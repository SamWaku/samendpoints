using System.Windows.Input;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.PatumbaCore.Configs;
using PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Entities;
using PatumbaPlatform.Core.PatumbaCore.Models.PatumbaLiveModels;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.ClientManagement.Commands;

public class GetClientsCommand: ICommand<IQueryable<Customer>>
{
    public required string DashboardType { get; set; } 
    public string? Msisdn { get; set; }
}

public class GetClientsCommandHandler(IServiceScopeFactory factory)
    : ICommandHandler<GetClientsCommand, IQueryable<Customer>>
{
    public Task<IQueryable<Customer>> ExecuteAsync(GetClientsCommand command, CancellationToken ct)
    {
        var scope = factory.CreateScope();

        if (command.DashboardType == CoreConfigs.AirtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<AirtelPatumbaDatabase>();
        
            var query = database.Customers
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x =>
                    x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
        
            return Task.FromResult(query);
        }
        
        if (command.DashboardType == CoreConfigs.MtnDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<MtnPatumbaDatabase>();
        
            var query = database.Customers
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x =>
                    x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
        
            return Task.FromResult(query);
        }
        
        if (command.DashboardType == CoreConfigs.ZamtelDashboard)
        {
            var database = scope.ServiceProvider.GetRequiredService<ZamtelPatumbaDatabase>();
        
            var query = database.Customers
                .OrderByDescending(x => x.Id)
                .ConditionalWhere(command.Msisdn != null, x =>
                    x.Msisdn == long.Parse(command.Msisdn!))
                .Take(100);
        
            return Task.FromResult(query);
        }

        // if (command.DashboardType == CoreConfigs.MobileDashboard)
        // {
        //     var database = scope.ServiceProvider.GetRequiredService<PatumbaDatabase>();
        //
        //     var systemUsersQuery = database.SystemUsers
        //         .OrderByDescending(x => x.Id)
        //         .ConditionalWhere(command.Msisdn != null, x => x.PhoneNumber == command.Msisdn)
        //         .Select(x => new
        //         {
        //             x.Id,
        //             x.FirstName,
        //             x.LastName,
        //             x.PhoneNumber,
        //             x.DateOfBirth,
        //             x.IdNumber,
        //             x.Status,
        //             x.CreatedAt
        //         });
        //
        //     var userList = systemUsersQuery.Take(100).ToList(); 
        //
        //     var customers = userList.Select(x => new Customer
        //     {
        //         Id = x.Id,
        //         FirstName = x.FirstName ?? "",
        //         LastName = x.LastName ?? "",
        //         Msisdn = long.TryParse(x.PhoneNumber, out var msisdn) ? msisdn : 0,
        //         Status = (int)x.Status,
        //         Dob = x.DateOfBirth != null ? DateOnly.FromDateTime(x.DateOfBirth.Value.DateTime) : null,
        //         Nrc = x.IdNumber,
        //         DateCreated = x.CreatedAt?.DateTime ?? DateTime.Now,
        //     }).AsQueryable();
        //
        //     return Task.FromResult(customers);
        // }
        return Task.FromResult<IQueryable<Customer>> (null);
    }
}