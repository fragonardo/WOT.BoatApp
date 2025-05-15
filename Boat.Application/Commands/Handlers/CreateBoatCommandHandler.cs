using BoatApp.Application.Commands;
using BoatApp.Domain.Exceptions;
using BoatApp.Domain.Repository;
using BoatApp.Shared.Application;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace BoatApp.Application.Handlers;

public class CreateBoatCommandHandler : IRequestHandler<CreateBoatCommand, Guid>
{
    private readonly IBoatRepository _repository;
    private readonly ILogger<CreateBoatCommandHandler> _logger;
    private readonly IMediator _mediator;

    public CreateBoatCommandHandler(IBoatRepository repository, ILogger<CreateBoatCommandHandler> logger, IMediator mediator)
    {
        _repository = repository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateBoatCommand request, CancellationToken cancellationToken)
    {
        // Vérifier les droits de l'utilisateur
        var userid = 1; // TODO Remplacer par l'identifiant de l'utilisateur lorsque le IIdentityService sera en place

        if (false)// TODO : Tester les droits d'accès
        {
            throw new UnauthorizedAccessException();
        }

        if (await _repository.HasBoatWithSerialNumber(request.SerialNumber, cancellationToken))
        {
            throw new BoatConflictException($"A boat with the same serial number '{request.SerialNumber}' aleready exists. Please select another serial number");
        }

        BoatApp.Domain.Models.Boat boat = new(
            Guid.NewGuid(),
            serialNumber: request.SerialNumber,
            type: request.Type,
            launchingDate: request.LaunchingDate,
            owner: request.Owner,
            name: request.Name,
            occuredBy: userid);

        var newBoat = await _repository.AddAsync(boat, cancellationToken);

        await _repository.SaveChangesAsync();

        return newBoat.Id;        
    }
}
