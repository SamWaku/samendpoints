namespace SamEndPoints.Common;

public interface IPaginatedResponse<T>
{
    int Page { get; set; }
    int PageSize { get; set; }
    int Total { get; set; }
    int TotalPages { get; set; }
    List<T> Data { get; set; }
}