using MediatR;

namespace BudgetLens.Application.Accounts.Commands;

public record AddTransactionCommand(
    Guid AccountId,
    decimal Amount,
    string Description,
    string Category
) : IRequest<AddTransactionResult>;

public record AddTransactionResult(
    bool Success,
    string? ErrorMessage
);