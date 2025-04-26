using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.PatumbaCore.Configs;
using PatumbaPlatform.Core.PatumbaCore.Models.PatumbaLiveModels;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;

public class GetClientWithdrawCommand : ICommand<IQueryable<Transaction>>
{
    public required string DashboardType { get; set; } 
    public string? Msisdn { get; set; }
    public string? AccountTypeId { get; set; }
}

public class GetClientWithdrawCommandHandler(IServiceScopeFactory factory)
    : ICommandHandler<GetClientWithdrawCommand, IQueryable<Transaction>>
{
    public async Task<IQueryable<Transaction>> ExecuteAsync(GetClientWithdrawCommand command, CancellationToken ct)
    {
        var scope = factory.CreateScope();
        
        List<int> serviceInts = [2, 4, 6, 8];
        
        if (command.DashboardType == CoreConfigs.AirtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<AirtelPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null, x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceInts.Contains(x.ServiceId))
                .Take(100);
            
            return query;
        }
        
        if (command.DashboardType == CoreConfigs.MtnDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<MtnPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null, x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceInts.Contains(x.ServiceId))
                .Take(100);
            
            return query;
        }
        
        if (command.DashboardType == CoreConfigs.ZamtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<ZamtelPatumbaDatabase>();
            var query = database.Transactions
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x => x.Msisdn == long.Parse(command.Msisdn!))
                .ConditionalWhere(command.AccountTypeId != null, x => x.AccountTypeId == int.Parse(command.AccountTypeId!))
                .Where(x => serviceInts.Contains(x.ServiceId))
                .Take(100);
            
            return query;
        }

        return null;
    }
}