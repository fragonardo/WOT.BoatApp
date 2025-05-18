using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoatApp.Application.Queries.Result;

public class ApiCollectionResult<T>(IEnumerable<T> data, int pageIndex, int pageSize, int totalCount)
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
    public IEnumerable<T> Data { get; private set; } = data;
}


