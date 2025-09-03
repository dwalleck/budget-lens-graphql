using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Accounts.Events;

public record TransactionAddedEvent : DomainEvent
{
    public Guid AccountId { get; init; }
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string Category { get; init; }
    public DateTime TransactionDate { get; init; }

    public TransactionAddedEvent(Guid accountId, decimal amount, string description, 
        string category, DateTime transactionDate)
    {
        AccountId = accountId;
        Amount = amount;
        Description = description;
        Category = category;
        TransactionDate = transactionDate;
    }

    public override Guid? UserId { get; init; }
}