using FastEndpoints;
using SamEndPoints.Common;

public class UserRequest : PaginationFilter
{
    // public int Id { get; set;}
    // public string? FirstName { get; set;}
    [QueryParam]
    public string? LastName { get; set;}
    // public string? Password {get; set;}
}