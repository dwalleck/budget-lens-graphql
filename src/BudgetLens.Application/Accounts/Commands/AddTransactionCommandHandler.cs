using MediatR;
using BudgetLens.Core.Domain.Accounts;
using BudgetLens.Core.Domain.Interfaces;

namespace BudgetLens.Application.Accounts.Commands;

public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, AddTransactionResult>
{
    private readonly IEventStore _eventStore;

    public AddTransactionCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<AddTransactionResult> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await _eventStore.LoadEventsAsync(request.AccountId, cancellationToken);
            var account = Account.FromEvents(request.AccountId, events);

            if (!account.IsActive)
            {
                return new AddTransactionResult(false, "Account is not active");
            }

            account.AddTransaction(request.Amount, request.Description, request.Category);

            await _eventStore.SaveEventsAsync(
                account.Id,
                nameof(Account),
                account.GetUncommittedEvents(),
                events.Count(),
                cancellationToken
            );

            account.ClearUncommittedEvents();

            return new AddTransactionResult(true, null);
        }
        catch (Exception ex)
        {
            return new AddTransactionResult(false, ex.Message);
        }
    }
}