using PRN231.Domain.Interfaces.UnitOfWork;

namespace PRN231.Infrastructure.DataAccess;

public abstract class BaseUnitOfWork(DbFactory dbFactory) : IDisposable, IBaseUnitOfWork
{
    private bool _disposed = false;
    protected readonly DbFactory _dbFactory = dbFactory;

    public async Task<int> CommitAsync()
    {
        return await _dbFactory.DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (_disposed || _dbFactory is null) return;
        _disposed = true;
        _dbFactory.Dispose();
        GC.SuppressFinalize(this);
    }
}
