using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

/// <summary>
/// Domain event raised when a closed account is reopened.
/// </summary>
public record AccountReopenedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public DateTime ReopenedAt { get; init; }
    public string? ReopenReason { get; init; }

    public AccountReopenedEvent(
        Guid accountId,
        DateTime reopenedAt,
        string? reopenReason,
        Guid userId)
    {
        AccountId = accountId;
        ReopenedAt = reopenedAt;
        ReopenReason = reopenReason;
        UserId = userId;
    }

    public override Guid? UserId { get; init; }
}