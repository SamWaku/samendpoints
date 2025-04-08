using System.Linq.Expressions;
using SamEndPoints.Common;
using SamEndPoints.Models;
using SamEndPoints.SamEndPoints.Common;

namespace SamEndPoints.Extensions;

public static class CollectionExetensions
{
     public static PaginatedResponse<T> ToPagedResponse<T>(this IEnumerable<T> data, int total,
        PaginationFilter filter)
    {
        return new PaginatedResponse<T>
        {
            Page = filter.PageNumber,
            PageSize = filter.PageSize,
            Total = total,
            TotalPages = (int) Math.Ceiling((double) total / filter.PageSize),
            Data = data.ToList() 
        };
    }
    public static IQueryable<User> UsersOrderAndPaginate(
        this IQueryable<User> data, PaginationFilter filter)
    {
        return data
            .OrderByDescending(x => x.Id)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);
    }

     public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> queryable, bool condition,
        Expression<Func<T, bool>> predicate)
    {
        if (condition)
        {
            queryable = queryable.Where(predicate);
        }
    
        return queryable;
    }
}