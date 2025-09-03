using BudgetLens.Core.Domain.Accounts;

namespace BudgetLens.Api.GraphQL.Accounts;

public record CreateAccountInput(
    string Name,
    string Description,
    AccountType AccountType,
    decimal InitialBalance,
    string Currency
);

public record AddTransactionInput(
    Guid AccountId,
    decimal Amount,
    string Description,
    string Category
);

public class AccountPayload
{
    public bool Success { get; set; }
    public AccountGraphQLType? Account { get; set; }
    public string? ErrorMessage { get; set; }

    public AccountPayload(bool success, AccountGraphQLType? account = null, string? errorMessage = null)
    {
        Success = success;
        Account = account;
        ErrorMessage = errorMessage;
    }
}

public class TransactionPayload
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public TransactionPayload(bool success, string? errorMessage = null)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }
}