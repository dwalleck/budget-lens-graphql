using BudgetLens.Core.Domain.Common;
using BudgetLens.Core.Domain.Accounts.Events;

namespace BudgetLens.Core.Domain.Accounts;

public class Account : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public AccountType AccountType { get; private set; }
    public decimal Balance { get; private set; }
    public string Currency { get; private set; } = "USD";
    public Guid UserId { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Account() { }

    public Account(Guid id, string name, string description, AccountType accountType, 
        decimal initialBalance, string currency, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name cannot be empty", nameof(name));
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty", nameof(userId));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        var createdEvent = new AccountCreatedEvent(
            id, name, description, accountType, initialBalance, currency, userId, DateTime.UtcNow);
        
        ApplyEvent(createdEvent);
        AddDomainEvent(createdEvent);
    }

    public void UpdateAccount(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name cannot be empty", nameof(name));

        var updatedEvent = new AccountUpdatedEvent(
            Id, name, description, DateTime.UtcNow);
        
        ApplyEvent(updatedEvent);
        AddDomainEvent(updatedEvent);
    }

    public void AddTransaction(decimal amount, string description, string category)
    {
        var transactionEvent = new TransactionAddedEvent(
            Id, amount, description, category, DateTime.UtcNow);
        
        ApplyEvent(transactionEvent);
        AddDomainEvent(transactionEvent);
    }

    public void DeactivateAccount()
    {
        if (!IsActive)
            return;

        var deactivatedEvent = new AccountDeactivatedEvent(Id, DateTime.UtcNow);
        
        ApplyEvent(deactivatedEvent);
        AddDomainEvent(deactivatedEvent);
    }

    public void ActivateAccount()
    {
        if (IsActive)
            return;

        var activatedEvent = new AccountActivatedEvent(Id, DateTime.UtcNow);
        
        ApplyEvent(activatedEvent);
        AddDomainEvent(activatedEvent);
    }

    public static Account FromEvents(Guid id, IEnumerable<DomainEvent> events)
    {
        var account = new Account { Id = id };
        foreach (var @event in events)
        {
            account.ApplyEvent(@event);
        }
        return account;
    }

    protected override void ApplyEvent(DomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case AccountCreatedEvent created:
                Id = created.AccountId;
                Name = created.Name;
                Description = created.Description;
                AccountType = created.AccountType;
                Balance = created.InitialBalance;
                Currency = created.Currency;
                UserId = created.AccountUserId;
                IsActive = true;
                CreatedAt = created.CreatedAt;
                break;
                
            case AccountUpdatedEvent updated:
                Name = updated.Name;
                Description = updated.Description;
                UpdatedAt = updated.UpdatedAt;
                break;
                
            case TransactionAddedEvent transaction:
                Balance += transaction.Amount;
                UpdatedAt = transaction.TransactionDate;
                break;
                
            case AccountDeactivatedEvent deactivated:
                IsActive = false;
                UpdatedAt = deactivated.DeactivatedAt;
                break;
                
            case AccountActivatedEvent activated:
                IsActive = true;
                UpdatedAt = activated.ActivatedAt;
                break;
        }
    }
}