using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.DataAccess;

namespace PRN231.Infrastructure.Repositories;

public class GenreRepository(DbFactory dbFactory) : Repository<Genre>(dbFactory), IGenreRepository
{
    public async Task<List<Genre>> ListGenresByIds(ICollection<int> ids)
    {
        var res = await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
        return res;
    }

    public async Task<Genre> GetGenreWithAnimesAsync(int id)
    {
        var res = await DbSet.Include(x => x.Animes).FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
}
