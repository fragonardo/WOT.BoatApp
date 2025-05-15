using BoatApp.Application.Queries.ViewModels;
using MediatR;

namespace BoatApp.Application.Queries;

public record GetAllBoatsQuery : IRequest<IEnumerable<BoatViewModel>>
{
    public string? SerialNumberFilter { get; init; }
    public string? OwnerFilter { get; init; }
    public string? NameFilter { get; init; }
    public int? PageIndex { get; init; }
    public int? ItemPerPage { get; init; }
}
