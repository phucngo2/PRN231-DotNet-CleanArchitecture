using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.Repositories;

public class UserTokenRepository(DbFactory dbFactory) : Repository<UserToken>(dbFactory), IUserTokenRepository
{
    public async Task<UserToken?> GetByUserIdAsync(int userId)
    {
        var res = await DbSet.FirstOrDefaultAsync(x => x.UserId == userId);
        return res;
    }

    public async Task<List<UserToken>> ListByUserIdAsync(int userId)
    {
        var res = await DbSet.Where(x => x.UserId == userId).ToListAsync();
        return res;
    }

    public async Task<UserToken?> GetNotExpiredByTokenAsync(string token)
    {
        var currentTime = DateTime.Now;
        var res = await DbSet
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token && currentTime < x.ExpiredAt);
        return res;
    }
}
