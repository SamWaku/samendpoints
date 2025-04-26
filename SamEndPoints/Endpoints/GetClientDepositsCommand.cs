using System.Windows.Input;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.Modules.Wallet.Models;
using PatumbaPlatform.Core.PatumbaCore.Configs;
using Transaction = PatumbaPlatform.Core.PatumbaCore.Models.PatumbaLiveModels.Transaction;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;

public class GetClientDepositsCommand: ICommand<IQueryable<Transaction>>
{
    public required string DashboardType { get; set; } 
    public string? Msisdn { get; set; }
    public string? AccountTypeId { get; set; }
    
}

public class GetClientDepositsCommandHandler(IServiceScopeFactory factory)
    : ICommandHandler<GetClientDepositsCommand, IQueryable<Transaction>>
{
    public Task<IQueryable<Transaction>> ExecuteAsync(GetClientDepositsCommand command, CancellationToken ct)
    {
        var scope = factory.CreateScope();
        List<int> serviceIds = [1, 3, 5, 7]; //deposit types
        if (command.DashboardType == CoreConfigs.AirtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<AirtelPatumbaDatabase>();

            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null,
                    x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceIds.Contains(x.ServiceId))
                .Take(1000);

            return Task.FromResult(query);
        }

        if (command.DashboardType == CoreConfigs.MtnDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<MtnPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null, x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceIds.Contains(x.ServiceId))
                .Take(1000);
            
            return Task.FromResult(query);
        }

        if (command.DashboardType == CoreConfigs.ZamtelDashboard)
        {
           
            var database = scope.ServiceProvider.GetRequiredService<ZamtelPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null, x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceIds.Contains(x.ServiceId))
                .Take(1000);

            return Task.FromResult(query);
        }
        return Task.FromResult<IQueryable<Transaction>>(null);
    }
}