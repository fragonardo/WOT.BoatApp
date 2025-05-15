using BoatApp.Domain.Events;
using BoatApp.Domain.Exceptions;
using BoatApp.Shared.Domain.Model;
using System.Xml.Linq;

namespace BoatApp.Domain.Models;

public enum BoatType
{
    Catamaran = 1,
    CabinCruiser,
    Schooner,
    Cutter,
    Banana,
    Log,
    Yacht,
    Sloop,
    Deck,
    Trawler
}

public sealed class Boat  : Entity<Guid>
{
    public string SerialNumber { get; private set; }    

    public BoatType Type { get; set; }       
    
    public DateTime LaunchingDate { get; private set; }

    public string? Owner { get; private set; }

    public string? Name { get; private set; }

    public Boat() { }

    public Boat(
        Guid id,
        string serialNumber,
        BoatType type,
        DateTime launchingDate,
        string? owner,
        string? name,        
        int occuredBy)
    {
        ThrowIfNullOrEmpty(serialNumber, "The SerialNumber could not be null or empty.");

        Id = id;
        SerialNumber = serialNumber;
        LaunchingDate = launchingDate;        
        Type = type;       
        Owner = owner;
        Name = name;
        CreatedBy = occuredBy;
        CreatedAt = DateTime.Now;

        AddDomainEvent(new BoatCreatedEvent(serialNumber));
    }
        

    public Boat UpdateOwner(string newOwner, string name)
    {
        ThrowIfNullOrEmpty(newOwner, "The new Owner name couldn't be null or empty");
        ThrowIfNullOrEmpty(name, "The name couldn't be null or empty");

        Owner = newOwner;
        Name = name;

        AddDomainEvent(new BoatOwnerChangedEvent(SerialNumber, newOwner, name));

        return this;
    }
}
