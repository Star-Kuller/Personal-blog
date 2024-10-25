using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core.Common;

namespace PersonalBlog.Core.Extensions;

public static class PagedListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> query, 
        PagedListQuery request,
        CancellationToken cancellationToken = default)
    {
        var rowsCount = await query.CountAsync(cancellationToken);
        return new PagedList<T>(
            request.Page,
            request.Size,
            totalPages: rowsCount / request.Size,
            rowsCount,
            await query.SkipItemsAsync(request, cancellationToken));
    }

    public static async Task<List<T>> SkipItemsAsync<T>(
        this IQueryable<T> query,
        PagedListQuery request,
        CancellationToken cancellationToken = default)
    {
        return await 
            query.Skip(request.Page * request.Size)
            .Take(request.Size).ToListAsync(cancellationToken);
    }
}