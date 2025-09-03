using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserActivatedEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public DateTime ActivatedAt { get; init; }

    public UserActivatedEvent(Guid domainUserId, DateTime activatedAt)
    {
        DomainUserId = domainUserId;
        ActivatedAt = activatedAt;
    }

    public override Guid? UserId { get; init; }
}