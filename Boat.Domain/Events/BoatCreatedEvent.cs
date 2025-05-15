using BoatApp.Shared.Domain.Events;

namespace BoatApp.Domain.Events;
public sealed class BoatCreatedEvent : DomainEventBase
{
    public BoatCreatedEvent(string serialNumber)
    {
        SerialNumber = serialNumber;
    }

    public string SerialNumber { get; private set; }
}

