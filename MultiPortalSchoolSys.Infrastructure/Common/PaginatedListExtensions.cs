using Microsoft.EntityFrameworkCore;
using MultiPortalSchoolSys.Application.Common.Models;

namespace MultiPortalSchoolSys.Infrastructure.Common;

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var total = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, total, pageNumber, pageSize);
    }
}