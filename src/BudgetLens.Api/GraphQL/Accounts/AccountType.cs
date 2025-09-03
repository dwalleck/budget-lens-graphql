using BudgetLens.Core.Domain.Accounts;

namespace BudgetLens.Api.GraphQL.Accounts;

public class AccountGraphQLType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static AccountGraphQLType FromDomain(Account account)
    {
        return new AccountGraphQLType
        {
            Id = account.Id,
            Name = account.Name,
            Description = account.Description,
            AccountType = account.AccountType,
            Balance = account.Balance,
            Currency = account.Currency,
            UserId = account.UserId,
            IsActive = account.IsActive,
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }
}