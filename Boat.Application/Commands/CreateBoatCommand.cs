using BoatApp.Domain.Models;
using BoatApp.Shared.Application;
using MediatR;

namespace BoatApp.Application.Commands;
public record CreateBoatCommand(
    Guid Id,
    string SerialNumber,
    BoatType Type,
    DateTime LaunchingDate,
    string Owner,
    string Name) : IRequest<Guid>;

