using BoatApp.Application.Handlers;
using BoatApp.Application.Queries.ViewModels;
using BoatApp.Domain.Exceptions;
using BoatApp.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Queries.Handlers;

public class GetBoatByIdQueryHandler : IRequestHandler<GetBoatByIdQuery, BoatViewModel?>
{
    private readonly IBoatRepository _repository;
    private readonly ILogger<CreateBoatCommandHandler> _logger;    

    public GetBoatByIdQueryHandler(IBoatRepository repository, ILogger<CreateBoatCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<BoatViewModel?> Handle(GetBoatByIdQuery request, CancellationToken cancellationToken)
    {   
        if(false)// TODO : Tester les droits d'accès
        {
            throw new UnauthorizedAccessException();
        }

        BoatViewModel? result = null;
        var boat = await _repository.FindAsync(request.Id, cancellationToken);

        if(boat is not null)
        {
            result = new(boat.Id, boat.SerialNumber, boat.Type, boat.LaunchingDate, boat.Owner, boat.Name);
        }

        return result;
    }
}
