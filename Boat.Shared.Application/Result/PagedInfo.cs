using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BoatApp.Shared.Application;

public class PagedInfo
{
    [JsonInclude]
    public long PageNumber { get; private set; }

    [JsonInclude]
    public long PageSize { get; private set; }

    [JsonInclude]
    public long TotalPages { get; private set; }

    [JsonInclude]
    public long TotalRecords { get; private set; }

    public PagedInfo(long pageNumber, long pageSize, long totalPages, long totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;        
    }

    public PagedInfo SetPageNumber(long pageNumber)
    {
        PageNumber = pageNumber;
        return this;
    }

    public PagedInfo SetPageSize(long pageSize)
    {
        PageSize = pageSize;
        return this;
    }

    public PagedInfo SetTotalPages(long totalPages)
    {
        TotalPages = totalPages;
        return this;
    }

    public PagedInfo SetTotalRecords(long totalRecords)
    {
        TotalRecords = totalRecords;
        return this;
    }
}
