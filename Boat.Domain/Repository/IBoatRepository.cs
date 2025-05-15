using BoatApp.Shared.Domain.Repository;


namespace BoatApp.Domain.Repository;
public interface IBoatRepository : IRepository<BoatApp.Domain.Models.Boat, Guid>
{
    Task<bool> HasBoatWithSerialNumber(string serialNumber, CancellationToken cancellationToken);
}
