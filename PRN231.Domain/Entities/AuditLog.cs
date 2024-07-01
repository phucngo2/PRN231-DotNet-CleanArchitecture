namespace PRN231.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public int EntityId { get; set; }
    public string Action { get; set; }
    public DateTime AuditDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }

    public User User { get; set; }
}
