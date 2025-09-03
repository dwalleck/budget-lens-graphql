using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserLoggedInEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public DateTime LoginAt { get; init; }

    public UserLoggedInEvent(Guid domainUserId, DateTime loginAt)
    {
        DomainUserId = domainUserId;
        LoginAt = loginAt;
    }

    public override Guid? UserId { get; init; }
}