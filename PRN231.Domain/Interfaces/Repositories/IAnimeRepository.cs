using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IAnimeRepository : IAuditableEntityRepository<Anime>
{
    Task<Anime?> GetAnimeById(int id);
}
