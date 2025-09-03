using System.Reflection;
using System.Text.Json;
using BudgetLens.Core.Domain.Common;
using BudgetLens.Core.Domain.Users;
using BudgetLens.Core.Domain.Accounts.Events;

namespace BudgetLens.Infrastructure.EventStore;

/// <summary>
/// Registry for mapping event type names to .NET types for deserialization.
/// </summary>
public class EventTypeRegistry
{
    private readonly Dictionary<string, Type> _eventTypes;
    private readonly JsonSerializerOptions _jsonOptions;

    public EventTypeRegistry()
    {
        _eventTypes = new Dictionary<string, Type>();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        RegisterEventTypes();
    }

    /// <summary>
    /// Register all known event types.
    /// </summary>
    private void RegisterEventTypes()
    {
        // User events
        RegisterEvent<UserCreatedEvent>();
        RegisterEvent<UserProfileUpdatedEvent>();
        RegisterEvent<UserEmailVerifiedEvent>();
        RegisterEvent<UserLoggedInEvent>();
        RegisterEvent<UserDeactivatedEvent>();
        RegisterEvent<UserActivatedEvent>();

        // Account events
        RegisterEvent<AccountCreatedEvent>();
        RegisterEvent<AccountUpdatedEvent>();
        RegisterEvent<AccountBalanceAdjustedEvent>();
        RegisterEvent<AccountClosedEvent>();
        RegisterEvent<AccountReopenedEvent>();
    }

    /// <summary>
    /// Register a specific event type.
    /// </summary>
    private void RegisterEvent<T>() where T : DomainEvent
    {
        var eventType = typeof(T);
        var eventTypeName = eventType.Name;
        _eventTypes[eventTypeName] = eventType;
    }

    /// <summary>
    /// Deserialize an event from JSON data.
    /// </summary>
    public DomainEvent? DeserializeEvent(string eventType, string eventData)
    {
        if (!_eventTypes.TryGetValue(eventType, out var type))
        {
            return null;
        }

        try
        {
            var domainEvent = JsonSerializer.Deserialize(eventData, type, _jsonOptions) as DomainEvent;
            return domainEvent;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Check if an event type is registered.
    /// </summary>
    public bool IsEventTypeRegistered(string eventType)
    {
        return _eventTypes.ContainsKey(eventType);
    }

    /// <summary>
    /// Get all registered event types.
    /// </summary>
    public IEnumerable<string> GetRegisteredEventTypes()
    {
        return _eventTypes.Keys;
    }
}