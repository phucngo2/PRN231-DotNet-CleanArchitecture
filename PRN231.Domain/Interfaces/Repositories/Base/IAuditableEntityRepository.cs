using PRN231.Domain.Entities.Base;
using PRN231.Domain.Models;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IAuditableEntityRepository<T> : IRepository<T> where T : AuditableEntity
{
    public void Delete(T model);
    public Task<List<T>> ListSoftDeletedAsync();
    public Task<PaginationResponse<T>> PaginateSoftDeletedAsync(PaginationRequest pagination);
}
