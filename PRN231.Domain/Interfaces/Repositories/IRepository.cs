using PRN231.Domain.Models;

namespace PRN231.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> ListAsync();
    Task<T> GetByIdAsync(params object[] keyValues);
    Task AddAsync(T model);
    void Delete(T model);
    void UpdateAsync(T model);
    Task<PaginationResponse<T>> PaginateAsync(PaginationRequest pagination);
}
