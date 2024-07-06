using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Email;
using PRN231.Domain.Models;

namespace PRN231.Infrastructure.Email;

public class EmailService(IOptions<MailSettings> options, IConfiguration configuration) : EmailSender(options), IEmailSerivce
{
    private readonly string _baseUrl = configuration["Application:BaseHost"];
    public async Task<bool> SendResetTokenMailAsync(User user, string token)
    {
        if (string.IsNullOrWhiteSpace(_baseUrl))
        {
            throw new Exception("Base host not found!");
        }

        string resetPasswordUrl = $"{_baseUrl}/reset-password?token={token}";
        string emailBody = $"<a target=\"_blank\" href=\"{resetPasswordUrl}\">Reset Password</a>";

        var mailData = new MailData
        {
            EmailToName = user.Name,
            EmailToId = user.Email,
            EmailSubject = "[PRN231] Reset Password",
            EmailBody = emailBody
        };

        var res = await SendMailAsync(mailData);

        return res;
    }
}
