using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IAnimeRepository : IRepository<Anime>
{
    Task<Anime> GetAnimeById(int id);
}
