using Microsoft.EntityFrameworkCore;
using PRN231.Domain.Entities.Base;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.Repositories;

public class AuditableEntityRepository<T>(DbFactory dbFactory) : Repository<T>(dbFactory), IAuditableEntityRepository<T> where T : AuditableEntity
{

    public void Delete(T model)
    {
        model.IsDeleted = true;
        Update(model);
    }

    public async Task<List<T>> ListSoftDeletedAsync()
    {
        var res = await DbSet
            .IgnoreQueryFilters()
            .Where(x => x.IsDeleted)
            .ToListAsync();

        return res;
    }
}
