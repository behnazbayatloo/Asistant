

using Asistant_Domain_Core._commonEntities;
using Microsoft.EntityFrameworkCore;

namespace Asistant_Infra_Extensions
{
    public static class PaginationExtension
    {
        public static async Task<PagedResult<T>> ToPaginatedResult<T>(this IQueryable<T> query
            ,int page, int pageSize, CancellationToken ct)
        {
            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync(ct);
            return new PagedResult<T>
            { 
                Items = items,
                TotalCount = totalCount,
                PageNumber=page,
                PageSize=pageSize
            };
                
                
                }
    }
}
