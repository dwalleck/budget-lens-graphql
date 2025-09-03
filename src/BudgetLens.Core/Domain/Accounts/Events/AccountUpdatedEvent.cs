using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

public record AccountUpdatedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime UpdatedAt { get; init; }

    public AccountUpdatedEvent(Guid accountId, string name, string description, DateTime updatedAt)
    {
        AccountId = accountId;
        Name = name;
        Description = description;
        UpdatedAt = updatedAt;
    }

    public override Guid? UserId { get; init; }
}