using BoatApp.Infrastructure.Services.Identity;
using MediatR;

namespace Boat.ApiService.Services;

public class ApiBoatServices(
    IMediator mediator, 
    IIdentityService identityService, 
    ILogger<ApiBoatServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public IIdentityService IdentityService { get; set; } = identityService;
    public ILogger<ApiBoatServices> Logger { get; set; } = logger;
}
