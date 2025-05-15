using BoatApp.Domain.Models;

namespace BoatApp.Application.Queries.ViewModels;

public record BoatViewModel(Guid Id, string SerialNumber, BoatType Type, DateTime LaunchingDate, string Owner, string Name)
{
}
