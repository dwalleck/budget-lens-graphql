using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

public record UserCreatedEvent : DomainEvent
{
    public Guid DomainUserId { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public DateTime CreatedAt { get; init; }

    public UserCreatedEvent(Guid domainUserId, string email, string username, DateTime createdAt)
    {
        DomainUserId = domainUserId;
        Email = email;
        Username = username;
        CreatedAt = createdAt;
    }

    public override Guid? UserId { get; init; }
}