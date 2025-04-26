using FastEndpoints;

namespace SamEndPoints.SamEndPoints.Endpoints;

public class UsersRequest
{

}

public class UsersResponse
{

}

public class GetUsersEndpoint: Endpoint<UserRequest, UsersResponse>
{
    public override void Configure()
    {
        base.Configure();
    }
    
    public override Task<UsersResponse> ExecuteAsync(UserRequest req, CancellationToken ct)
    {
        return base.ExecuteAsync(req, ct);
    }
}