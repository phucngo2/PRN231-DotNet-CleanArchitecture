using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.DataAccess;

namespace PRN231.Infrastructure.Repositories;

public class UserRepository(DbFactory dbFactory) : Repository<User>(dbFactory), IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var res = await DbSet.FirstOrDefaultAsync(x => x.Email == email);
        return res;
    }
}
