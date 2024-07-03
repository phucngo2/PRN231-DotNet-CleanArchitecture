using PRN231.Domain.Entities;
using PRN231.Domain.Interfaces.Repositories;
using PRN231.Infrastructure.Data;

namespace PRN231.Infrastructure.Repositories;

internal class AuditLogRepository(DbFactory dbFactory) : Repository<AuditLog>(dbFactory), IAuditLogRepository
{
}
