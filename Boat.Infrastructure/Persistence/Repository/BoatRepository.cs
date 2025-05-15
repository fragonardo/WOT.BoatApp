using BoatApp.Domain.Models;
using BoatApp.Domain.Repository;
using BoatApp.Shared.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BoatApp.Infrastructure.Persistence.Repository;

public class BoatRepository : EfRepositoryBase<BoatApp.Domain.Models.Boat, Guid>, IBoatRepository
{
    public BoatRepository(BoatDbContext context, IMediator mediator) : base(context, mediator) { }

    public override async Task<Boat> FindAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.SingleOrDefaultAsync(b => b.Id == Id, cancellationToken);
    }

    public async Task<bool> HasBoatWithSerialNumber(string serialNumber, CancellationToken cancellationToken)
    {   
        return await _dbSet.AnyAsync(x => x.SerialNumber == serialNumber, cancellationToken);
    }
}