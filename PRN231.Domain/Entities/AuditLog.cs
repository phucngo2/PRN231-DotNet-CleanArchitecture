using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Entities;

public class AuditLog
{
    [Key]
    public int Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public int? EntityId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string QueryString { get; set; } = string.Empty;
    public DateTime AuditDate { get; set; } = DateTime.Now;
    public int? UserId { get; set; }
}
