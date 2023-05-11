namespace LTechnestHackhaton.Application.Common.Interfaces;

public interface IMailService
{
    Task SendMailAsync(string to, string cc, string subject, int body, bool isBodyHtml = true);
    Task SendMailAsync(string[] tos, string[] ccs, string subject, int[] body, bool isBodyHtml = true);
    public void SendPasswordResetMail(string to, string userId, string resetToken);
}
