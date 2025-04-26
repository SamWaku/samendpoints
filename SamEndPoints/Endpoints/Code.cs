using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Common.Pagination;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;
using PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Entities;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Endpoints;

public class ClientWithdrawRequest : PaginationFilter
{
    public required string DashboardType { get; set; }
    public  string? AccountTypeId { get; set; }
    public string? Msisdn { get; set; }
}

public class GetClientWithdrawEndpoint : Endpoint<ClientWithdrawRequest, PaginatedResponse<ClientWithdrawResponse>>
{
    public override void Configure()
    {
       Get("patumba/client-management/client-withdraws");
       Summary(s =>
       {
           s.Summary = "Get client withdraws";
       });
       AllowAnonymous();
    }

    public override async Task<PaginatedResponse<ClientWithdrawResponse>> ExecuteAsync(ClientWithdrawRequest filter,
        CancellationToken ct)
    {
        var query = await new GetClientWithdrawCommand
        {
            DashboardType = filter.DashboardType,
            AccountTypeId = filter.AccountTypeId,
            Msisdn = filter.Msisdn
        }.ExecuteAsync(ct: ct);

        var clientWithdraws = await query
            .TransactionOrderAndPaginate(filter)
            .ToListAsync(cancellationToken: ct);

        var totalItems = await query.CountAsync(ct);

        return clientWithdraws.Select(x => new ClientWithdrawResponse
        {
            Id = x.Id,
            Msisdn = x.Msisdn,
            Amount = x.Amount,
            Units = x.Units,
            UnitPrice = x.UnitPrice,
            StatusCode = x.StatusCode,
            ServiceId = x.ServiceId,
            AccountTypeId = x.AccountTypeId,
            MaturityDate = x.MaturityDate,
            DateCreated = x.DateCreated
        }).ToPagedResponse(totalItems, filter);

    }
}