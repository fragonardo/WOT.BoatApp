using BoatApp.Shared.Domain.Model;
using BoatApp.Shared.Domain.Events;
using MediatR;

namespace BoatApp.Shared.Domain.Model;

public abstract class Entity<TId> : Auditable
{
    int? _requestedHashCode;

    private TId? _Id;

    public virtual TId? Id
    {
        get
        {
            return _Id;
        }
        protected set
        {
            _Id = value;
        }
    }
        

    private List<DomainEventBase> _domainEvents = [];

    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
        

    public void AddDomainEvent(DomainEventBase eventItem)
    {
        _domainEvents = _domainEvents ?? new List<DomainEventBase>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(DomainEventBase eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient()
    {
        return EqualityComparer<TId>.Default.Equals(Id, default);
        //return object.Equals(Id, default(TId));
    }

    public override bool Equals(object obj)
    {
        if (obj == null || (obj is not Entity<TId>))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        Entity<TId> item = (Entity<TId>)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        else
            return EqualityComparer<TId>.Default.Equals(item.Id, Id);
        
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = (Id?.GetHashCode() ?? 0) ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator == (Entity<TId> left, Entity<TId> right)
    {
        if (Equals(left, null))
            return Equals(right, null);
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    protected static void ThrowIfNullOrEmpty(string? value, string message)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullException(message);
    }

    protected static void ThrowIfNull(object value, string message)
    {
        if (value is null)
            throw new ArgumentNullException(message);
    }
}
