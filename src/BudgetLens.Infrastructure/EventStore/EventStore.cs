using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BudgetLens.Core.Domain.Common;
using BudgetLens.Core.Domain.Interfaces;
using BudgetLens.Infrastructure.Data;

namespace BudgetLens.Infrastructure.EventStore;

/// <summary>
/// PostgreSQL-based implementation of the event store.
/// Provides event sourcing capabilities with optimistic concurrency control.
/// </summary>
public class EventStore : IEventStore
{
    private readonly BudgetLensDbContext _context;
    private readonly ILogger<EventStore> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public EventStore(BudgetLensDbContext context, ILogger<EventStore> logger)
    {
        _context = context;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task SaveEventsAsync(
        Guid aggregateId,
        string aggregateType,
        IEnumerable<DomainEvent> events,
        int expectedVersion,
        CancellationToken cancellationToken = default)
    {
        var eventList = events.ToList();
        if (!eventList.Any())
        {
            return;
        }

        _logger.LogDebug("Saving {EventCount} events for aggregate {AggregateId} with expected version {ExpectedVersion}",
            eventList.Count, aggregateId, expectedVersion);

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Check current version for optimistic concurrency
            var currentVersion = await GetAggregateVersionAsync(aggregateId, cancellationToken);
            
            if (currentVersion != expectedVersion)
            {
                throw new InvalidOperationException(
                    $"Concurrency conflict for aggregate {aggregateId}. Expected version {expectedVersion}, but current version is {currentVersion}");
            }

            // Convert domain events to stored events
            var storedEvents = eventList.Select((domainEvent, index) => new StoredEvent
            {
                EventId = domainEvent.EventId,
                AggregateId = aggregateId,
                AggregateType = aggregateType,
                EventType = domainEvent.EventType,
                EventData = JsonSerializer.Serialize(domainEvent, domainEvent.GetType(), _jsonOptions),
                Metadata = CreateMetadata(domainEvent),
                Version = expectedVersion + index + 1,
                OccurredAt = domainEvent.OccurredAt,
                UserId = domainEvent.UserId
            }).ToList();

            // Save to database
            await _context.Events.AddRangeAsync(storedEvents, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Successfully saved {EventCount} events for aggregate {AggregateId}. New version: {NewVersion}",
                eventList.Count, aggregateId, expectedVersion + eventList.Count);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Failed to save events for aggregate {AggregateId}", aggregateId);
            throw;
        }
    }

    public async Task<IEnumerable<DomainEvent>> LoadEventsAsync(
        Guid aggregateId,
        CancellationToken cancellationToken = default)
    {
        return await LoadEventsAsync(aggregateId, 0, cancellationToken);
    }

    public async Task<IEnumerable<DomainEvent>> LoadEventsAsync(
        Guid aggregateId,
        int fromVersion,
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Loading events for aggregate {AggregateId} from version {FromVersion}", 
            aggregateId, fromVersion);

        var storedEvents = await _context.Events
            .Where(e => e.AggregateId == aggregateId && e.Version > fromVersion)
            .OrderBy(e => e.Version)
            .ToListAsync(cancellationToken);

        var domainEvents = new List<DomainEvent>();

        foreach (var storedEvent in storedEvents)
        {
            try
            {
                // For now, we'll need a way to reconstruct the actual event types
                // This is a simplified implementation - in practice, you'd need a registry
                // of event types or use reflection/assembly scanning
                var domainEvent = DeserializeEvent(storedEvent);
                if (domainEvent != null)
                {
                    domainEvents.Add(domainEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to deserialize event {EventId} of type {EventType}", 
                    storedEvent.EventId, storedEvent.EventType);
            }
        }

        _logger.LogDebug("Loaded {EventCount} events for aggregate {AggregateId}", 
            domainEvents.Count, aggregateId);

        return domainEvents;
    }

    public async Task<int> GetAggregateVersionAsync(
        Guid aggregateId,
        CancellationToken cancellationToken = default)
    {
        var latestEvent = await _context.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderByDescending(e => e.Version)
            .FirstOrDefaultAsync(cancellationToken);

        return latestEvent?.Version ?? 0;
    }

    private string CreateMetadata(DomainEvent domainEvent)
    {
        var metadata = new
        {
            EventVersion = domainEvent.EventVersion,
            AssemblyQualifiedName = domainEvent.GetType().AssemblyQualifiedName,
            SavedAt = DateTime.UtcNow
        };

        return JsonSerializer.Serialize(metadata, _jsonOptions);
    }

    private DomainEvent? DeserializeEvent(StoredEvent storedEvent)
    {
        // This is a simplified implementation. In a real application, you would:
        // 1. Have an event type registry
        // 2. Use the metadata to find the correct type
        // 3. Handle event versioning and evolution
        
        // For now, we'll need to implement this when we have concrete event types
        // This method should be enhanced with proper event type resolution
        
        _logger.LogWarning("Event deserialization not fully implemented for event type {EventType}", 
            storedEvent.EventType);
        
        return null;
    }
}