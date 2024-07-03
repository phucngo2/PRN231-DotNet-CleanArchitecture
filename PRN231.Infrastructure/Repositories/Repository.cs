using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Domain.Models;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.Repositories;

public class Repository<T>(DbFactory dbFactory) : IRepository<T> where T : class
{
    protected DbSet<T> DbSet = dbFactory.DbContext.Set<T>();

    public async Task AddAsync(T model)
    {
        await DbSet.AddAsync(model);
    }

    public void Delete(T model)
    {
        DbSet.Remove(model);
    }

    public async Task<T> GetByIdAsync(params object[] keyValues)
    {
        return await DbSet.FindAsync(keyValues);
    }

    public async Task<List<T>> ListAsync()
    {
        return await DbSet.ToListAsync();
    }

    public void UpdateAsync(T model)
    {
        DbSet.Update(model);
    }

    protected static async Task<PaginationResponse<T>> ToPagedList(IQueryable<T> source, PaginationRequest pagination)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
        return new PaginationResponse<T>
        {
            CurrentPage = pagination.Page,
            PageSize = pagination.PageSize,
            TotalCount = count,
            TotalPages = (int) Math.Ceiling(count / (decimal) pagination.PageSize),
            Result = items
        };
    }

    public async Task<PaginationResponse<T>> PaginateAsync(PaginationRequest pagination)
    {
        return await ToPagedList(DbSet, pagination);
    }
}
