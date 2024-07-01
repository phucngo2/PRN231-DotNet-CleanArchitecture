using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetUserByEmailAsync(string email);
}
