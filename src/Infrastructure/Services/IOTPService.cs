namespace TechnestHackhaton.Infrastructure.Services;

public interface IOTPService
{
    public int SendSms(string usernameOrEmail);
}
