using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

public record AccountActivatedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public DateTime ActivatedAt { get; init; }

    public AccountActivatedEvent(Guid accountId, DateTime activatedAt)
    {
        AccountId = accountId;
        ActivatedAt = activatedAt;
    }

    public override Guid? UserId { get; init; }
}