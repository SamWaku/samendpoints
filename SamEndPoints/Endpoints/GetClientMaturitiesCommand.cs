using System.Windows.Input;
using FastEndpoints;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.PatumbaCore.Configs;
using PatumbaPlatform.Core.PatumbaCore.Models.PatumbaLiveModels;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;

public class GetClientMaturitiesCommand: ICommand<IQueryable<Transaction>>
{
    public required string DashboardType { get; set; } 
    public string? Msisdn { get; set; }
    public string? AccountTypeId { get; set; }
}

public class GetClientMaturitiesCommandHandler(IServiceScopeFactory factory)
    : ICommandHandler<GetClientDepositsCommand, IQueryable<Transaction>>
{
    public Task<IQueryable<Transaction>> ExecuteAsync(GetClientDepositsCommand command, CancellationToken ct)
    {
        var scope = factory.CreateScope();

        if (command.DashboardType == CoreConfigs.AirtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<AirtelPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
            return Task.FromResult(query);
        }
        
        if (command.DashboardType == CoreConfigs.MtnDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<MtnPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
            return Task.FromResult(query);
        }
        
        if (command.DashboardType == CoreConfigs.ZamtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<ZamtelPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
            return Task.FromResult(query);
        }

        return Task.FromResult<IQueryable<Transaction>>(null);
    }
}