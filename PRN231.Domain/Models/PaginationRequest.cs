using System.ComponentModel.DataAnnotations;

namespace PRN231.Domain.Models;

public class PaginationRequest
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Page must be a positive number")]
    public int Page { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "PageSize must be a positive number")]
    public int PageSize { get; set; }
}
