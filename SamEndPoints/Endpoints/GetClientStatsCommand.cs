using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Common;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.PatumbaCore.Configs;
using PatumbaPlatform.Core.PatumbaCore.Models.PatumbaLiveModels;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;

public class GetClientStatsCommand : ICommand<List<Customer>>
{
    public required string DashboardType { get; set; } 
    public string? Msisdn { get; set; }
}

public class GetClientsStatsCommandHandler(AirtelPatumbaDatabase airtelDatabase, MtnPatumbaDatabase mtnDatabase, ZamtelPatumbaDatabase zamtelDatabase) : ICommandHandler<GetClientStatsCommand, List<Customer>>
{
    public async Task<List<Customer>> ExecuteAsync(GetClientStatsCommand command, CancellationToken ct)
    {

        switch (command.DashboardType)
        {
            case CoreConfigs.AirtelDashboard:
            {
                var query = await airtelDatabase.Customers
                    .OrderByDescending(x => x.Id)
                    .ToListAsync(ct);
                
                return query;
            }

            case CoreConfigs.MtnDashboard:
            {
                var query = await mtnDatabase.Customers
                    // .Include(x => x.Transaction)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync(ct);

                return query;
            }

            case CoreConfigs.ZamtelDashboard:
            {
                var query = await zamtelDatabase.Customers
                    .OrderByDescending(x => x.Id)
                    .ToListAsync(ct);
                
                return query;
            }
        }
        return null;
    }
}