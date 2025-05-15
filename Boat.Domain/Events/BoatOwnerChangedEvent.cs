using BoatApp.Shared.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatApp.Domain.Events;
public sealed class BoatOwnerChangedEvent : DomainEventBase
{
    public BoatOwnerChangedEvent(string serialNumber, string newOwner, string name)
    {
        SerialNumber = serialNumber;
        NewOwner = newOwner;
        Name = name;        
    }

    public string SerialNumber { get; private set; }
    public string NewOwner { get; private set; }
    public string Name { get; private set; }
}
