// using FastEndpoints;
// using Microsoft.EntityFrameworkCore;


// namespace SamEndPoints.SamEndPoints.Endpoints;
// {
//     public class InvestmentTransactionsRequest : PaginationFilter
//     {
//         public String? TransactionType { get; set; }
        
//     }
    
//     public class GetIMobileDashInvestmentTransactionsEndpoints 
//         : Endpoint<InvestmentTransactionsRequest, PaginatedResponse<MobileInvestmentTransactionResponse>>

//     {
//         public override void Configure()
//         {
//             Get("patumba/dashboard/mobile-investment-transactions");
//             Summary(s =>
//             {
//                 s.Summary="Get all mobile investment transactions";
//             });
//             AllowAnonymous();
//         }

//         public override async Task<PaginatedResponse<MobileInvestmentTransactionResponse>> ExecuteAsync(
//             InvestmentTransactionsRequest filter, CancellationToken ct)
//         {
//             var query = await new GetMobileDashInvestmentTransactionsCommand
//             {
//                 TransactionType = filter.TransactionType
//             }.ExecuteAsync(ct: ct);

//             var investmentTransactions = await query
//                 .InvestmentTransactionsOrderAndPaginate(filter)
//                 .ToListAsync(cancellationToken: ct);
            
//             var totalItems = await query.CountAsync(cancellationToken: ct);

//             return investmentTransactions.Select(x => new MobileInvestmentTransactionResponse
//             {
//                 Account = x.Account,
//                 PaymentMethod = x.PaymentMethod,
//                 Status = x.Status,
//                 ExternalId = x.ExternalId,
//                 TransactionType = x.TransactionType,
//                 Units = x.Units,
//                 Amount = x.Amount,
//                 UnitPrice = x.UnitPrice,
//                 ProductType = x.ProductType,
//                 CreatedAt = x.CreatedAt,
//                 UpdatedAt = x.UpdatedAt
//             }).ToPagedResponse(totalItems, filter);
//         }
//     }
// }
