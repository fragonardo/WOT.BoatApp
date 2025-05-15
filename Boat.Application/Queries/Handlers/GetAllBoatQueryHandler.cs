using BoatApp.Application.Handlers;
using BoatApp.Application.Queries.ViewModels;
using BoatApp.Domain.Models;
using BoatApp.Domain.Repository;
using BoatApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Queries.Handlers;

public class GetAllBoatQueryHandler : IRequestHandler<GetAllBoatsQuery, IEnumerable<BoatViewModel>>
{
    private readonly ILogger<CreateBoatCommandHandler> _logger;
    private readonly  BoatDbContext _db;

    public GetAllBoatQueryHandler(BoatDbContext db, ILogger<CreateBoatCommandHandler> logger)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IEnumerable<BoatViewModel>> Handle(GetAllBoatsQuery request, CancellationToken cancellationToken)
    {   
        if(false)// TODO : Tester les droits d'accès
        {
            throw new UnauthorizedAccessException();
        }

        var query = _db.Boat.AsQueryable();

        if(!string.IsNullOrEmpty(request.SerialNumberFilter))
        {
            query = query.Where(x => x.SerialNumber.Contains(request.SerialNumberFilter));
        }

        if (!string.IsNullOrEmpty(request.OwnerFilter))
        {
            query = query.Where(x => !string.IsNullOrEmpty(x.Owner) &&  x.Owner.Contains(request.OwnerFilter));
        }

        if (!string.IsNullOrEmpty(request.OwnerFilter))
        {
            query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.NameFilter));
        }

        var itemPerPage = request.ItemPerPage ?? 9;
        var PageIndex = request.PageIndex ?? 0;

        var result = query
            .Skip(itemPerPage * PageIndex)
            .Take(itemPerPage).ToList()
            .Select(x => new BoatViewModel(x.Id, x.SerialNumber, x.Type, x.LaunchingDate, x.Owner, x.Name));

        return result;            
    }
}
