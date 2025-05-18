using Boat.Shared.Kernel.Extensions;
using BoatApp.Application.Handlers;
using BoatApp.Application.Queries.Result;
using BoatApp.Application.Queries.ViewModels;
using BoatApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Queries.Handlers;

public class GetAllBoatQueryHandler : IRequestHandler<GetAllBoatsQuery, ApiCollectionResult<BoatViewModel>>
{
    private readonly ILogger<CreateBoatCommandHandler> _logger;
    private readonly  BoatDbContext _db;

    public GetAllBoatQueryHandler(BoatDbContext db, ILogger<CreateBoatCommandHandler> logger)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<ApiCollectionResult<BoatViewModel>> Handle(GetAllBoatsQuery request, CancellationToken cancellationToken)
    {   
        if(false)// TODO : Tester les droits d'accès
        {
            throw new UnauthorizedAccessException();
        }

        var query = _db.Boat.AsQueryable();
        
        if (!string.IsNullOrEmpty(request.Filter))
        {
            query = query.Where(x => 
                (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(request.Filter))
                ||(x.SerialNumber.Contains(request.Filter))
                ||(!string.IsNullOrEmpty(x.Owner) && x.Owner.Contains(request.Filter))                
            );
        }

        var paginatedResult = await query.ToPaginatedListAsync<Domain.Models.Boat>(request.PageIndex, request.ItemPerPage, cancellationToken);

        var result = new ApiCollectionResult<BoatViewModel>(
            paginatedResult.Data.Select(x => new BoatViewModel(x.Id, x.SerialNumber, x.Type, x.LaunchingDate, x.Owner, x.Name)),
            paginatedResult.PageIndex,
            paginatedResult.PageSize,
            paginatedResult.TotalCount);
        
        return result;            
    }
}
