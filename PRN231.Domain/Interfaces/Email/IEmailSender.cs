using PRN231.Domain.Models;

namespace PRN231.Domain.Interfaces.Email;

public interface IEmailSender
{
    public Task<bool> SendMailAsync(MailData mailData);
}
