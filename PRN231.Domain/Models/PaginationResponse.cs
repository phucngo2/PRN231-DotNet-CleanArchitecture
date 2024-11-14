namespace PRN231.Domain.Models;

public class PaginationResponse<T> where T : class
{
    public List<T> Result { get; set; } = [];
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
