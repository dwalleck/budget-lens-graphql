namespace BudgetLens.Core.Domain.Common;

/// <summary>
/// Base record for all domain events in the system.
/// Events are immutable records that represent something that has happened in the domain.
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// Unique identifier for this specific event occurrence
    /// </summary>
    public Guid EventId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// When this event occurred
    /// </summary>
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// The user who caused this event (if applicable)
    /// </summary>
    public virtual Guid? UserId { get; init; }

    /// <summary>
    /// The type name of this event (for serialization/deserialization)
    /// </summary>
    public string EventType => GetType().Name;

    /// <summary>
    /// Version of this event structure (for event evolution)
    /// </summary>
    public virtual int EventVersion => 1;
}