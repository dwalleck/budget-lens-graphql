using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Interfaces;

/// <summary>
/// Repository interface for the event store, providing event sourcing capabilities.
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// Save events for an aggregate to the event store
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate</param>
    /// <param name="aggregateType">The type name of the aggregate</param>
    /// <param name="events">The events to save</param>
    /// <param name="expectedVersion">The expected current version (for optimistic concurrency)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task representing the async operation</returns>
    Task SaveEventsAsync(
        Guid aggregateId,
        string aggregateType,
        IEnumerable<DomainEvent> events,
        int expectedVersion,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Load all events for a specific aggregate
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All events for the aggregate in order</returns>
    Task<IEnumerable<DomainEvent>> LoadEventsAsync(
        Guid aggregateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Load events for a specific aggregate from a specific version
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate</param>
    /// <param name="fromVersion">Load events from this version onwards</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Events for the aggregate from the specified version onwards</returns>
    Task<IEnumerable<DomainEvent>> LoadEventsAsync(
        Guid aggregateId,
        int fromVersion,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the current version of an aggregate
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The current version, or 0 if the aggregate doesn't exist</returns>
    Task<int> GetAggregateVersionAsync(
        Guid aggregateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get events for a specific aggregate using the domain object method names.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate</param>
    /// <param name="aggregateType">The type name of the aggregate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All events for the aggregate in order</returns>
    Task<IEnumerable<DomainEvent>> GetEventsAsync(
        Guid aggregateId,
        string aggregateType,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get events for all aggregates of a specific type.
    /// </summary>
    /// <param name="aggregateType">The type name of the aggregate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All events for the aggregate type</returns>
    Task<IEnumerable<DomainEvent>> GetEventsByAggregateTypeAsync(
        string aggregateType,
        CancellationToken cancellationToken = default);
}