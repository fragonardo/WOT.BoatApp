using Boat.Shared.Kernel.Extensions;
using BoatApp.Application.Queries.Result;
using BoatApp.Application.Queries.ViewModels;
using MediatR;

namespace BoatApp.Application.Queries;

public record GetAllBoatsQuery : IRequest<ApiCollectionResult<BoatViewModel>>
{
    public string? Filter { get; init; }
    public int PageIndex { get; init; }
    public int ItemPerPage { get; init; }
}
