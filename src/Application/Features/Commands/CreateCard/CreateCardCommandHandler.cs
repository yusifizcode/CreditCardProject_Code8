using MediatR;
using TechnestHackhaton.Application.Common.Interfaces;
using TechnestHackhaton.Application.DTOs.Card;

namespace TechnestHackhaton.Application.Features.Commands.CreateCard;

public class CreateCardCommandHandler : IRequestHandler<CreateCardCommandRequest, CreateCardCommandResponse>
{
    private readonly ICardOperationChecker _cardOperationChecker;

    public CreateCardCommandHandler(ICardOperationChecker cardOperationChecker)
    {
        _cardOperationChecker = cardOperationChecker;
    }

    public async Task<CreateCardCommandResponse> Handle(CreateCardCommandRequest request, CancellationToken cancellationToken)
    {
        CardInfoRequestObjectDto postData = new()
        {
            Salary = request.Salary,
            CreditAmount = request.CreditAmount,
            EmploymentTime = request.EmploymentTime,
            HomeOwnership = request.HomeOwnership,
            LoanPurposes = request.LoanPurposes,
        };
        CardCreatorData cardCreatorData = new()
        {
            Age = request.Age,
            CreditHistoryLength = request.CreditHistoryLength,
            CreditStatus = request.CreditStatus,
            LoanPercentage = request.LoanPercentage,
            LoanRate = request.LoanRate,
            PaymentHistory = request.PaymentHistory
        };
        var result = await _cardOperationChecker.CheckCart(postData, cardCreatorData);

        if (result == 1)
        {
            return new()
            {
                Result = "Success"
            };
        }
        else
        {
            return new()
            {
                Result = "Unsuccess"
            };
        }
    }
}
