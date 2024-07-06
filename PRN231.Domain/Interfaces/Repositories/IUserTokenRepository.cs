using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IUserTokenRepository : IRepository<UserToken>
{
    public Task<UserToken> GetByUserIdAsync(int userId);
    public Task<List<UserToken>> ListByUserIdAsync(int userId);
    public Task<UserToken> GetNotExpiredByTokenAsync(string token);
}
