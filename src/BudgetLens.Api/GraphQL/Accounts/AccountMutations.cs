using MediatR;
using HotChocolate.Authorization;
using System.Security.Claims;
using BudgetLens.Application.Accounts.Commands;

namespace BudgetLens.Api.GraphQL.Accounts;

public class AccountMutations
{
    [Authorize]
    public async Task<AccountPayload> CreateAccountAsync(
        CreateAccountInput input,
        [Service] IMediator mediator,
        ClaimsPrincipal claimsPrincipal)
    {
        var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return new AccountPayload(false, null, "User not authenticated");
        }

        var command = new CreateAccountCommand(
            input.Name,
            input.Description,
            input.AccountType,
            input.InitialBalance,
            input.Currency,
            userId
        );

        var result = await mediator.Send(command);
        
        if (!result.Success)
        {
            return new AccountPayload(false, null, result.ErrorMessage);
        }

        // Get the created account for the response
        var getAccountQuery = new BudgetLens.Application.Accounts.Queries.GetAccountQuery(result.AccountId!.Value);
        var accountResult = await mediator.Send(getAccountQuery);
        
        if (accountResult.Success && accountResult.Account != null)
        {
            var accountType = AccountGraphQLType.FromDomain(accountResult.Account);
            return new AccountPayload(true, accountType);
        }

        return new AccountPayload(true, null, "Account created but could not retrieve details");
    }

    [Authorize]
    public async Task<TransactionPayload> AddTransactionAsync(
        AddTransactionInput input,
        [Service] IMediator mediator)
    {
        var command = new AddTransactionCommand(
            input.AccountId,
            input.Amount,
            input.Description,
            input.Category
        );

        var result = await mediator.Send(command);
        
        return new TransactionPayload(result.Success, result.ErrorMessage);
    }
}