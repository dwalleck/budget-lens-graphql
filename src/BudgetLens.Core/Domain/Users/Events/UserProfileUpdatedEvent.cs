using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserProfileUpdatedEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public DateTime UpdatedAt { get; init; }

    public UserProfileUpdatedEvent(Guid domainUserId, string? firstName, string? lastName, DateTime updatedAt)
    {
        DomainUserId = domainUserId;
        FirstName = firstName;
        LastName = lastName;
        UpdatedAt = updatedAt;
    }

    public override Guid? UserId { get; init; }
}