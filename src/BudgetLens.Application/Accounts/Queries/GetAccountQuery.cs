using MediatR;
using BudgetLens.Core.Domain.Accounts;

namespace BudgetLens.Application.Accounts.Queries;

public record GetAccountQuery(Guid AccountId) : IRequest<GetAccountResult>;

public record GetAccountResult(
    bool Success,
    Account? Account,
    string? ErrorMessage
);