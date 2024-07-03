using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IUserRepository : IAuditableEntityRepository<User>
{
    public Task<User> GetUserByEmailAsync(string email);
}
