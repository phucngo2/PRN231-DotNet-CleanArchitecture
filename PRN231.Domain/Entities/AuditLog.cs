namespace PRN231.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public int? EntityId { get; set; }
    public string Action { get; set; }
    public string Method { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public DateTime AuditDate { get; set; } = DateTime.Now;
    public int? UserId { get; set; }
}
