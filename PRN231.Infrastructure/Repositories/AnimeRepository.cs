using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.Repositories;

public class AnimeRepository(DbFactory dbFactory) : AuditableEntityRepository<Anime>(dbFactory), IAnimeRepository
{
    public async Task<Anime> GetAnimeById(int id)
    {
        var res = await DbSet
            .Include(x => x.Genres)
            .FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
}
