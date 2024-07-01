using PRN231.Domain.Entities;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<List<Genre>> ListGenresByIds(ICollection<int> ids);
    Task<Genre> GetGenreWithAnimesAsync(int id);
}
