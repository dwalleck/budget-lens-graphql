using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

public record AccountDeactivatedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public DateTime DeactivatedAt { get; init; }

    public AccountDeactivatedEvent(Guid accountId, DateTime deactivatedAt)
    {
        AccountId = accountId;
        DeactivatedAt = deactivatedAt;
    }

    public override Guid? UserId { get; init; }
}