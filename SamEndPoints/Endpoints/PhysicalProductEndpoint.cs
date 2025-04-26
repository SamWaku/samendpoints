using FastEndpoints;
using SamEndPoints.Common;
using SamEndPoints.Extensions;
using SamEndPoints.SamEndPoints.Common;
using SamEndPoints.SamEndPoints.Database;
using SamEndPoints.SamEndPoints.Responses;

namespace SamEndpoints.SamEndPoints.Endpoints
{
    public class ProductRequest: PaginationFilter
    {
        public string? Title { get; set; }
    }
    public class PhysicalProductEndpoint(ApplicationDBContext database) : Endpoint<ProductRequest, PaginatedResponse<ProductResponse>>
    {
        public override void Configure()
        {
            Get("/products/physical-products");
            Summary(s => {
                s.Summary = "View all products";
            });
            AllowAnonymous();
        }
    }

    // public override async Task<PaginatedResponse<ProductResponse>> ExecuteAsync(ProductRequest filter, CancellationToken ct)
    // {
    //     var query = database.ProductModels
    //     .OrderByDescending(x => x.Id)
    //     .ConditionalWhere(filter.Title != null, x => x.Title == filter.Title)
    //     .Take(100);

    //     // var products = await query
    //     // .UsersOrderAndPaginate
    // }
}