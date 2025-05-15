using BoatApp.Shared.Application;
using MediatR;

namespace BoatApp.Application.Commands;

public record UpdateBoatOwnerCommand(Guid Id, string NewOwner) : IRequest<Result<Guid>>
{
}


