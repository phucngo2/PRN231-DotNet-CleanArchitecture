namespace PRN231.Domain.Interfaces.UnitOfWork;

public interface IBaseUnitOfWork
{
    Task<int> CommitAsync();
}
