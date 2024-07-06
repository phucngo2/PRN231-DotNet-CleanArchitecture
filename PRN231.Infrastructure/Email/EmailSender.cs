using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Models;

namespace PRN231.Infrastructure.Email;

public abstract class EmailSender(IOptions<MailSettings> options) : IEmailSender
{
    private readonly MailSettings _emailSettings = options.Value;

    public async Task<bool> SendMailAsync(MailData mailData)
    {
        var emailMessage = new MimeMessage();

        var emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
        emailMessage.From.Add(emailFrom);

        var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
        emailMessage.To.Add(emailTo);

        emailMessage.Subject = mailData.EmailSubject;

        var emailBodyBuilder = new BodyBuilder
        {
            HtmlBody = mailData.EmailBody
        };
        emailMessage.Body = emailBodyBuilder.ToMessageBody();

        var mailClient = new SmtpClient();

        await mailClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
        await mailClient.AuthenticateAsync(_emailSettings.EmailId, _emailSettings.Password);
        await mailClient.SendAsync(emailMessage);

        await mailClient.DisconnectAsync(true);
        mailClient.Dispose();

        return true;
    }
}
