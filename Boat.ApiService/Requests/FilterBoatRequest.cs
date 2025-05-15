namespace Boat.ApiService.Requests;

public class FilterBoatRequest(string serialNumberFilter, string ownerFilter, string nameFilter, int pageIndex, int itemPerPage)
{
    public string? SerialNumberFilter { get; set; } = serialNumberFilter;
    public string? OwnerFilter { get; set; } = ownerFilter;
    public string? NameFilter { get; set; } = nameFilter;
    public int? PageIndex { get; set; } = pageIndex;
    public int? ItemPerPage { get; set; } = itemPerPage;
}

