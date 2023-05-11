using Microsoft.AspNetCore.Identity;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TechnestHackhaton.Infrastructure.Services;

public class OTPService : IOTPService
{
    private readonly ITwilioRestClient _client;
    readonly UserManager<Domain.Entity.Identity.AppUser> _userManager;
    public OTPService(ITwilioRestClient client, UserManager<Domain.Entity.Identity.AppUser> userManager)
    {
        _client = client;
        _userManager = userManager;
    }

    public int SendSms(string usernameOrEmail)
    {
        var user = _userManager.Users.FirstOrDefault(x => x.UserName == usernameOrEmail || x.Email == usernameOrEmail);
        var generateNumber = new Random();
        var newNum = generateNumber.Next(1000, 9999);
        var message = MessageResource.Create(to: new PhoneNumber(user.PhoneNumber), from: new PhoneNumber("+12707478228"), body: newNum.ToString(), client: _client);

        return newNum;
    }
}
