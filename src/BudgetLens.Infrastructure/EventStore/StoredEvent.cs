namespace BudgetLens.Infrastructure.EventStore;

/// <summary>
/// Entity representing a stored event in the database.
/// Uses snake_case naming convention for PostgreSQL compatibility.
/// </summary>
public class StoredEvent
{
    /// <summary>
    /// Unique identifier for this event
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// The aggregate this event belongs to
    /// </summary>
    public Guid AggregateId { get; set; }

    /// <summary>
    /// The type name of the aggregate
    /// </summary>
    public string AggregateType { get; set; } = string.Empty;

    /// <summary>
    /// The type name of the event
    /// </summary>
    public string EventType { get; set; } = string.Empty;

    /// <summary>
    /// The event data serialized as JSON
    /// </summary>
    public string EventData { get; set; } = string.Empty;

    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// The version of this event within the aggregate
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// When this event occurred
    /// </summary>
    public DateTime OccurredAt { get; set; }

    /// <summary>
    /// The user who caused this event (if applicable)
    /// </summary>
    public Guid? UserId { get; set; }
}