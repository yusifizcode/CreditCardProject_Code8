using LTechnestHackhaton.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TechnestHackhaton.Persistence.Context;
namespace TechnestHackhaton.Infrastructure.Services;

public class MailService : IMailService
{
    readonly IConfiguration _configuration;
    readonly TechnestHackhatonDbContext _context;

    public MailService(IConfiguration configuration, TechnestHackhatonDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    public async Task SendMailAsync(string to, string cc, string subject, int body, bool isBodyHtml = true)
    {
        await SendMailAsync(new[] { to }, new[] { cc }, subject, new[] { body }, isBodyHtml);
    }

    public async Task SendMailAsync(string[] tos, string[] ccs, string subject, int[] body, bool isBodyHtml = true)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(subject, _configuration["Mail:Username"]));
        foreach (var to in tos)
            message.To.Add(new MailboxAddress(to, to));
        foreach (var cc in ccs)
            if (cc != "")
                message.Cc.Add(new MailboxAddress(cc, cc));

        message.Subject = subject;
        var bodyBuilder = new BodyBuilder();
        message.Body = bodyBuilder.ToMessageBody();

        // send the email using SMTP
        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect(_configuration["Mail:Host"], 465, true);
            client.Authenticate(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public void SendPasswordResetMail(string to, string userId, string resetToken)
    {
        throw new NotImplementedException();
    }
}
