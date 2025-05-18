using Microsoft.EntityFrameworkCore;

namespace Boat.Shared.Kernel.Extensions;

public class PaginatedList<T>(List<T> data,
                              int pageIndex,
                              int pageSize,
                              int totalCount)
{
    /// <summary>
    /// Index of the current page (Zero-based)
    /// </summary>
    public int PageIndex { get; private set; } = pageIndex;

    /// <summary>
    /// Number of items contained in each page
    /// </summary>
    public int PageSize { get; private set; } = pageSize;

    /// <summary>
    /// Total pages count
    /// </summary>
    public int TotalPageCount { get; private set; } = (int)Math.Ceiling(totalCount / (double)pageSize);

    /// <summary>
    /// Total items count
    /// </summary>
    public int TotalCount { get; private set; } = totalCount;

    /// <summary>
    /// The data result
    /// </summary>
    public List<T> Data { get; private set; } = data;

   

    /// <summary>
    /// Return true if thecurrent page has a previous page
    /// </summary>
    public bool HasPreviousPage
    {
        get
        {
            return PageIndex > 0;
        }
    }

    /// <summary>
    /// Return true if the current page has a next page
    /// </summary>
    public bool HasNextPage
    {
        get
        {
            return PageIndex + 1 < TotalCount;
        }
    }
}

public static class PaginatedListExtensionForIQueryable
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source,
                                                                        int pageIndex,
                                                                        int pageSize,
                                                                        CancellationToken cancellationToken)
    {
        var count = await source.CountAsync(cancellationToken);
        source = source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var data = await source.ToListAsync<T>(cancellationToken);
        
        return new PaginatedList<T>(data, pageIndex, pageSize, count);
    }
}
