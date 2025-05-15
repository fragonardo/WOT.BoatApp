namespace Boat.ApiService.Requests;

public record UpdateBoatRequest(
    //Guid Id,
    string SerialNumber,
    int Type,
    DateTime LaunchingDate,
    string Owner,
    string Name
);
