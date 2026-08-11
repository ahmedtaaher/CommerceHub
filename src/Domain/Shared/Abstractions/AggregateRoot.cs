using Domain.Shared.Events;

namespace Domain.Shared.Abstractions
{
  public abstract class AggregateRoot<TId> : AuditableEntity<TId>
  {
    private readonly List<IDomainEvent> _domainEvents = [];
   
    protected AggregateRoot()
    {
      
    }
    protected AggregateRoot(TId id) : base(id)
    {
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected void RaiseDomainEvents(IDomainEvent domainEvent)
    {
      ArgumentNullException.ThrowIfNull(domainEvent);
      _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
      _domainEvents.Clear();
    }
  }
}