using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Email;

public interface IEmailSerivce : IEmailSender
{
    public Task<bool> SendResetTokenMailAsync(User user, string token);
}
