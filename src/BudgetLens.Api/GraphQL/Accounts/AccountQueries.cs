using MediatR;
using HotChocolate.Authorization;
using BudgetLens.Application.Accounts.Queries;

namespace BudgetLens.Api.GraphQL.Accounts;

public class AccountQueries
{
    [Authorize]
    public async Task<AccountGraphQLType?> GetAccountAsync(
        Guid accountId,
        [Service] IMediator mediator)
    {
        var query = new GetAccountQuery(accountId);
        var result = await mediator.Send(query);
        
        if (result.Success && result.Account != null)
        {
            return AccountGraphQLType.FromDomain(result.Account);
        }

        return null;
    }
}