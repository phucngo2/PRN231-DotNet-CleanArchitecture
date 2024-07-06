using PRN231.Domain.Interfaces.Repositories;
using PRN231.Domain.Interfaces.UnitOfWork;

namespace PRN231.Infrastructure.Data;

public class UnitOfWork(
    DbFactory dbFactory,
    IAnimeRepository animeRepository,
    IAuditLogRepository auditLogRepository,
    IGenreRepository genreRepository,
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository
) : BaseUnitOfWork(dbFactory), IUnitOfWork
{
    public IAnimeRepository AnimeRepository { get; } = animeRepository;
    public IAuditLogRepository AuditLogRepository { get; } = auditLogRepository;
    public IGenreRepository GenreRepository { get; } = genreRepository;
    public IUserRepository UserRepository { get; } = userRepository;
    public IUserTokenRepository UserTokenRepository { get; } = userTokenRepository;
}
