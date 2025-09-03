namespace BudgetLens.Core.Domain.Common;

/// <summary>
/// Base class for aggregate roots in the domain.
/// Manages domain events and provides identity.
/// </summary>
public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _uncommittedEvents = new();

    /// <summary>
    /// Unique identifier for this aggregate.
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Protected constructor for Entity Framework and derived classes.
    /// </summary>
    protected AggregateRoot()
    {
    }

    /// <summary>
    /// Constructor with ID for creating new aggregates.
    /// </summary>
    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Add a domain event to be published.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _uncommittedEvents.Add(domainEvent);
    }

    /// <summary>
    /// Get all uncommitted events.
    /// </summary>
    /// <returns>Collection of uncommitted domain events.</returns>
    public IReadOnlyCollection<DomainEvent> GetUncommittedEvents()
    {
        return _uncommittedEvents.AsReadOnly();
    }

    /// <summary>
    /// Clear all uncommitted events (called after events are persisted).
    /// </summary>
    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    /// <summary>
    /// Apply a domain event to update the aggregate state.
    /// </summary>
    /// <param name="domainEvent">The domain event to apply.</param>
    protected abstract void ApplyEvent(DomainEvent domainEvent);

    /// <summary>
    /// Override equality based on ID.
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (obj is not AggregateRoot other || GetType() != obj.GetType())
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Id != Guid.Empty && Id == other.Id;
    }

    /// <summary>
    /// Override hash code based on ID.
    /// </summary>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Override ToString for debugging.
    /// </summary>
    public override string ToString()
    {
        return $"{GetType().Name} [Id: {Id}]";
    }
}