using BoatApp.Application.Queries.ViewModels;
using MediatR;

namespace BoatApp.Application.Queries;

public record GetBoatByIdQuery(Guid Id) : IRequest<BoatViewModel>;

