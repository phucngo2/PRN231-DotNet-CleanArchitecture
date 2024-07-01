using PRN231.Domain.Interfaces.Repositories;
using PRN231.Domain.Interfaces.UnitOfWork;

namespace PRN231.Infrastructure.DataAccess;

public class UnitOfWork(
    DbFactory dbFactory,
    IAnimeRepository animeRepository,
    IGenreRepository genreRepository,
    IUserRepository userRepository
) : BaseUnitOfWork(dbFactory), IUnitOfWork
{
    public IAnimeRepository AnimeRepository { get; } = animeRepository;
    public IGenreRepository GenreRepository { get; } = genreRepository;
    public IUserRepository UserRepository { get; } = userRepository;
}
