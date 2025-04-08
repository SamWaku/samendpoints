using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SamEndPoints.Common;
using SamEndPoints.Extensions;
using SamEndPoints.SamEndPoints.Common;
using SamEndPoints.SamEndPoints.Database;
using SamEndPoints.SamEndPoints.Responses;
namespace SamEndPoints.SamEndPoints.Endpoints;

 public class UserRequest : PaginationFilter
{
    public string? LastName { get; set;}
}
public class UserEndpoint(ApplicationDBContext database): Endpoint<UserRequest, PaginatedResponse<UserResponse>>
{
   

    public override void Configure()
    {
        Get("/api/users/get-users");
        Summary(s => {
            s.Summary = "get users";
        });
        AllowAnonymous();
    }

    public override async Task<PaginatedResponse<UserResponse>> ExecuteAsync(UserRequest filter, CancellationToken ct)
    {
        var query = database.Users
             .OrderByDescending(x => x.Id)
             .ConditionalWhere(filter.LastName !=null, x => x.LastName == filter.LastName)
             .Take(100);


        var users = await query
            .UsersOrderAndPaginate(filter)
            .ToListAsync(cancellationToken: ct);

        var totalItems = await query.CountAsync(cancellationToken: ct);

        return users.Select(x => new UserResponse
        {
            UserId = x.Id.ToString(),
            FullName = x.FirstName
        }).ToPagedResponse(totalItems, filter);
    }
}