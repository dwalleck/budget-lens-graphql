using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserDeactivatedEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public DateTime DeactivatedAt { get; init; }

    public UserDeactivatedEvent(Guid domainUserId, DateTime deactivatedAt)
    {
        DomainUserId = domainUserId;
        DeactivatedAt = deactivatedAt;
    }

    public override Guid? UserId { get; init; }
}