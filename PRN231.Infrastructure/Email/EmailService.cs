using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PRN231.Domain.Constants;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Models;

namespace PRN231.Infrastructure.Email;

public class EmailService(IOptions<MailSettings> options, IConfiguration configuration) : EmailSender(options), IEmailSerivce
{
    private readonly string _baseUrl = configuration["Application:BaseHost"] ?? string.Empty;
    private readonly string _resetPasswordPath = configuration["Application:ResetPasswordPath"] ?? string.Empty;
    public bool SendResetTokenEmail(User user, string token)
    {
        if (string.IsNullOrWhiteSpace(_baseUrl))
        {
            throw new Exception("Base host not found!");
        }

        string emailSubject = EmailConstants.EMAIL_RESET_TOKEN_SUBJECT;
        string resetPasswordUrl = string.Format($"{_baseUrl}/{_resetPasswordPath}", token);
        string emailBody = EmailConstants.GenerateResetTokenEmailBody(resetPasswordUrl, "Reset Password");

        var mailData = new MailData
        {
            EmailToName = user.Name,
            EmailToId = user.Email,
            EmailSubject = emailSubject,
            EmailBody = emailBody
        };

        BackgroundJob.Enqueue(() => SendMailAsync(mailData));

        return true;
    }
}
