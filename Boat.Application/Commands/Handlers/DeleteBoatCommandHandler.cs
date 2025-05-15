using BoatApp.Application.Commands;
using BoatApp.Domain.Exceptions;
using BoatApp.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Handlers;

public class DeleteBoatCommandHandler : IRequestHandler<DeleteBoatCommand, bool>
{
    private readonly IBoatRepository _repository;
    private readonly ILogger<CreateBoatCommandHandler> _logger;
    private readonly IMediator _mediator;

    public DeleteBoatCommandHandler(IBoatRepository repository, ILogger<CreateBoatCommandHandler> logger, IMediator mediator)
    {
        _repository = repository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
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

        _repository.Delete(boat);

        await _repository.SaveChangesAsync();

        return true;        
    }
}
