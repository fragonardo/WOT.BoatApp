﻿using Microsoft.AspNetCore.Http;

namespace BoatApp.Infrastructure.Services.Identity;

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string GetUserIdentity()
        => context.HttpContext?.User.FindFirst("sub")?.Value ?? string.Empty;

    public string GetUserName()
        => context.HttpContext?.User.Identity?.Name ?? string.Empty;
}
