using MediatR;
using BudgetLens.Core.Domain.Accounts;
using BudgetLens.Core.Domain.Interfaces;

namespace BudgetLens.Application.Accounts.Queries;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountResult>
{
    private readonly IEventStore _eventStore;

    public GetAccountQueryHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<GetAccountResult> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await _eventStore.LoadEventsAsync(request.AccountId, cancellationToken);
            
            if (!events.Any())
            {
                return new GetAccountResult(false, null, "Account not found");
            }

            var account = Account.FromEvents(request.AccountId, events);
            return new GetAccountResult(true, account, null);
        }
        catch (Exception ex)
        {
            return new GetAccountResult(false, null, ex.Message);
        }
    }
}