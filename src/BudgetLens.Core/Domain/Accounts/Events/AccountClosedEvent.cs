using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

/// <summary>
/// Domain event raised when an account is closed.
/// </summary>
public record AccountClosedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public decimal FinalBalance { get; init; }
    public string? ClosureReason { get; init; }
    public DateTime ClosedAt { get; init; }

    public AccountClosedEvent(
        Guid accountId,
        decimal finalBalance,
        string? closureReason,
        DateTime closedAt,
        Guid userId)
    {
        AccountId = accountId;
        FinalBalance = finalBalance;
        ClosureReason = closureReason;
        ClosedAt = closedAt;
        UserId = userId;
    }

    public override Guid? UserId { get; init; }
}