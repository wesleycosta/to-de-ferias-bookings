using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public IReadOnlyCollection<Event> DomainEvents =>
        _eventNotification.AsReadOnly();

    private List<Event> _eventNotification = new();

    public bool HasEvents() =>
        DomainEvents?.Count > 0;

    public void AddEvent(Event eventMessage)
    {
        _eventNotification ??= new List<Event>();
        _eventNotification.Add(eventMessage);
    }

    public void RemoveEvent(Event eventItem) =>
        _eventNotification?.Remove(eventItem);

    public void ClearEvents() =>
        _eventNotification?.Clear();

    public bool ContainsEvent(string eventType) =>
        _eventNotification.Any(p => p?.MessageType == eventType);

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo))
            return true;
        if (compareTo is null)
            return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) =>
        !(a == b);

    public override string ToString() =>
        GetType().Name + " [Id=" + Id + "]";

    public override int GetHashCode() =>
        (GetType().GetHashCode() * 907) + Id.GetHashCode();
}
