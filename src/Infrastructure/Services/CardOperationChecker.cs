using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TechnestHackhaton.Application.Common.Interfaces;
using TechnestHackhaton.Application.DTOs.Card;
using TechnestHackhaton.Application.Repositories.CheckoutHistory;
using TechnestHackhaton.Domain.Entities;
using TechnestHackhaton.Persistence.Context;

namespace TechnestHackhaton.Infrastructure.Services;

public class CardOperationChecker : ICardOperationChecker
{
    private readonly TechnestHackhatonDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICheckoutHistoryWriteRepository _checkoutHistoryWriteRepository;
    public CardOperationChecker(TechnestHackhatonDbContext context, IHttpContextAccessor httpContextAccessor, ICheckoutHistoryWriteRepository checkoutHistoryWriteRepository)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _checkoutHistoryWriteRepository = checkoutHistoryWriteRepository;
    }

    public async Task<int> CheckCart(CardInfoRequestObjectDto cardInfoRequestObjectDto, CardCreatorData cardCreatorData)
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var json = JsonSerializer.Serialize(cardInfoRequestObjectDto);
        var dictionary = new Dictionary<string, string>()
        {
            {"data", json}
        };
        var requestApi = await client.PostAsJsonAsync("http://91.102.161.166:5000/predict?data=" + dictionary["data"], "");
        var resultApi = await requestApi.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CheckCreditResponseResultDto>(resultApi.ToString());
        if (result.result == 1)
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
            CheckoutHistory checkoutHistory = new()
            {
                Age = cardCreatorData.Age,
                CreditAmount = cardInfoRequestObjectDto.CreditAmount,
                CreditHistoryLength = cardCreatorData.CreditHistoryLength,
                CreditStatus = cardCreatorData.CreditStatus,
                Email = user.Email,
                EmploymentTime = cardInfoRequestObjectDto.EmploymentTime,
                HomeOwnership = cardInfoRequestObjectDto.HomeOwnership,
                LoanPercentage = cardCreatorData.LoanPercentage,
                LoanPurposes = cardInfoRequestObjectDto.LoanPurposes,
                LoanRate = cardCreatorData.LoanRate,
                PaymentHistory = cardCreatorData.PaymentHistory,
                Salary = cardInfoRequestObjectDto.Salary,
                UserName = user.UserName
            };
            await _checkoutHistoryWriteRepository.AddAsync(checkoutHistory);
            await _checkoutHistoryWriteRepository.SaveAsync();
        }
        return result.result;
    }
}
