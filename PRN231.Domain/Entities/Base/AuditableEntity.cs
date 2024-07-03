namespace PRN231.Domain.Entities.Base;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
