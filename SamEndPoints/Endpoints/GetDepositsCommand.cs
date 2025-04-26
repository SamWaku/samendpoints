using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using PatumbaPlatform.Core.Modules.Database;
using PatumbaPlatform.Core.Modules.Extensions;
using PatumbaPlatform.Core.Modules.Wallet.Models;

namespace PatumbaPlatform.Core.PatumbaCore.ManagementPortal.Summary.Commands;

public class GetDepositsCommand: ICommand<IQueryable<Transaction>>
{
    public string? TransactionType { get; set; }
}

public class GetDepositsCommandHandler(PatumbaDatabase database,AirtelPatumbaDatabase airteldatabase)
    : ICommandHandler<GetDepositsCommand, IQueryable<Transaction>>
{
    public async Task<IQueryable<Transaction>> ExecuteAsync(GetDepositsCommand command, CancellationToken ct)
    {
        var query = database.InvestmentTransactions
            .OrderByDescending(x => x.Id)
            .ConditionalWhere(command.TransactionType != null, x => x.TransactionType == TransactionType.Deposit)
            .Take(100);
        return query;
    }
}