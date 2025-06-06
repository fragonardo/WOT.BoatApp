﻿using MediatR;

namespace BoatApp.Shared.Domain.Events;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
