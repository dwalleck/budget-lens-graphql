using MediatR;
using BudgetLens.Core.Domain.Accounts;
using BudgetLens.Core.Domain.Interfaces;

namespace BudgetLens.Application.Accounts.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly IEventStore _eventStore;

    public CreateAccountCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountId = Guid.NewGuid();
            var account = new Account(
                accountId,
                request.Name,
                request.Description,
                request.AccountType,
                request.InitialBalance,
                request.Currency,
                request.UserId
            );

            await _eventStore.SaveEventsAsync(
                account.Id,
                nameof(Account),
                account.GetUncommittedEvents(),
                0,
                cancellationToken
            );

            account.ClearUncommittedEvents();

            return new CreateAccountResult(true, accountId, null);
        }
        catch (Exception ex)
        {
            return new CreateAccountResult(false, null, ex.Message);
        }
    }
}