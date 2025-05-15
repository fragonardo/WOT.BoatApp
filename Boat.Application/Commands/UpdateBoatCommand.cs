using MediatR;

namespace BoatApp.Application.Commands;

public record UpdateBoatCommand(
    Guid Id,
    //string SerialNumber,
    //BoatType Type,
    //DateTime LaunchingDate,
    string Owner,
    string Name) : IRequest<Guid>;

