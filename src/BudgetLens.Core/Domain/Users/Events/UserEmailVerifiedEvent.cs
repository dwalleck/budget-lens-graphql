using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserEmailVerifiedEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public string Email { get; init; }
    public DateTime VerifiedAt { get; init; }

    public UserEmailVerifiedEvent(Guid domainUserId, string email, DateTime verifiedAt)
    {
        DomainUserId = domainUserId;
        Email = email;
        VerifiedAt = verifiedAt;
    }

    public override Guid? UserId { get; init; }
}