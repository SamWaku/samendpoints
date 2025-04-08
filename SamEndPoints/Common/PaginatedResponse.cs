using SamEndPoints.Common;

namespace SamEndPoints.SamEndPoints.Common;

public class PaginatedResponse<T> : IPaginatedResponse<T>
{
 public required int Page { get; set; }
    public required int PageSize { get; set; }
    public required int Total { get; set; }
    public required int TotalPages { get; set; }
    public required List<T> Data { get; set; } = [];
}