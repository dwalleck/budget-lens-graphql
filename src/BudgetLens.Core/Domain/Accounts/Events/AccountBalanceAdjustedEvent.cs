using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

/// <summary>
/// Domain event raised when an account balance is manually adjusted.
/// This is different from transaction-based balance changes.
/// </summary>
public record AccountBalanceAdjustedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public decimal PreviousBalance { get; init; }
    public decimal NewBalance { get; init; }
    public decimal AdjustmentAmount { get; init; }
    public string? Reason { get; init; }
    public DateTime AdjustedAt { get; init; }

    public AccountBalanceAdjustedEvent(
        Guid accountId,
        decimal previousBalance,
        decimal newBalance,
        decimal adjustmentAmount,
        string? reason,
        DateTime adjustedAt,
        Guid userId)
    {
        AccountId = accountId;
        PreviousBalance = previousBalance;
        NewBalance = newBalance;
        AdjustmentAmount = adjustmentAmount;
        Reason = reason;
        AdjustedAt = adjustedAt;
        UserId = userId;
    }

    public override Guid? UserId { get; init; }
}