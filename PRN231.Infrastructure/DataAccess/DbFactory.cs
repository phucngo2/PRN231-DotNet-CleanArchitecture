using Microsoft.EntityFrameworkCore;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.DataAccess;

public class DbFactory(Func<AppDbContext> dbContextFactory) : IDisposable
{
    private bool _disposed;
    private readonly Func<AppDbContext> _instanceFunc = dbContextFactory;
    private AppDbContext _dbContext;
    public DbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

    public void Dispose()
    {
        if (_disposed || _dbContext is null) return;
        _disposed = true;
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
