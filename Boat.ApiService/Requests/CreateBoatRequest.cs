using BoatApp.Domain.Models;

namespace BoatApp.ApiService.Requests;

public record CreateBoatRequest(
    string SerialNumber,
    int Type,
    DateTime LaunchingDate,
    string Owner,
    string Name
);

