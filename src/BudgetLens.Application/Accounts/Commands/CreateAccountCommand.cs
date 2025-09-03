using MediatR;
using BudgetLens.Core.Domain.Accounts;

namespace BudgetLens.Application.Accounts.Commands;

public record CreateAccountCommand(
    string Name,
    string Description,
    AccountType AccountType,
    decimal InitialBalance,
    string Currency,
    Guid UserId
) : IRequest<CreateAccountResult>;

public record CreateAccountResult(
    bool Success,
    Guid? AccountId,
    string? ErrorMessage
);