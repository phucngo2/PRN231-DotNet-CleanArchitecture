namespace PRN231.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}
