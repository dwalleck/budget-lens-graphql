using BudgetLens.Api.GraphQL.Users;
using BudgetLens.Api.GraphQL.Accounts;

namespace BudgetLens.Api.GraphQL;

/// <summary>
/// Root GraphQL mutation type for Budget Lens API.
/// </summary>
public class Mutation
{
    /// <summary>
    /// User-related mutations.
    /// </summary>
    public UserMutations Users => new();

    /// <summary>
    /// Account-related mutations.
    /// </summary>
    public AccountMutations Accounts => new();
}