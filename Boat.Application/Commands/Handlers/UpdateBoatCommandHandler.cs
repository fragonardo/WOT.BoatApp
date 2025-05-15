using BoatApp.Application.Commands;
using BoatApp.Domain.Exceptions;
using BoatApp.Domain.Repository;
using BoatApp.Shared.Application;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Handlers;

public class UpdateBoatCommandHandler : IRequestHandler<UpdateBoatCommand, Guid>
{
    private readonly IBoatRepository _repository;
    private readonly ILogger<UpdateBoatCommandHandler> _logger;
    private readonly IMediator _mediator;

    public UpdateBoatCommandHandler(IBoatRepository repository, ILogger<UpdateBoatCommandHandler> logger, IMediator mediator)
    {
        _repository = repository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
    {
        // Vérifier les droits de l'utilisateur
        var userid = 1; // TODO Remplacer par l'identifiant de l'utilisateur lorsque le IIdentityService sera en place

        if (false)// TODO : Tester les droits d'accès
        {
            throw new UnauthorizedAccessException();
        }
        var boat = await _repository.FindAsync(request.Id, cancellationToken);

        if(boat is null)
        {
            throw new BoatNotFoundException($"No boat find for Id '{request.Id}'");
        }

        boat.UpdateOwner(request.Owner, request.Name);

        _repository.Update(boat);

        await _repository.SaveChangesAsync();

        return boat.Id;        
    }
}
