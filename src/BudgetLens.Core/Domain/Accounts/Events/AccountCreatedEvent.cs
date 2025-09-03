using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

public record AccountCreatedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public AccountType AccountType { get; init; }
    public decimal InitialBalance { get; init; }
    public string Currency { get; init; }
    public Guid AccountUserId { get; init; }
    public DateTime CreatedAt { get; init; }

    public AccountCreatedEvent(Guid accountId, string name, string description, 
        AccountType accountType, decimal initialBalance, string currency, Guid userId, DateTime createdAt)
    {
        AccountId = accountId;
        Name = name;
        Description = description;
        AccountType = accountType;
        InitialBalance = initialBalance;
        Currency = currency;
        AccountUserId = userId;
        CreatedAt = createdAt;
    }

    public override Guid? UserId => AccountUserId;
}