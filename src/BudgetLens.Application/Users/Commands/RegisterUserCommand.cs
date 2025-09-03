using MediatR;
using BudgetLens.Core.Domain.Users;

namespace BudgetLens.Application.Users.Commands;

/// <summary>
/// Command to register a new user.
/// </summary>
public record RegisterUserCommand(
    string Email,
    string Username,
    string Password,
    string? FirstName = null,
    string? LastName = null
) : IRequest<RegisterUserResult>;

/// <summary>
/// Result of user registration.
/// </summary>
public record RegisterUserResult(
    bool Success,
    Guid? UserId,
    string? AccessToken,
    string? RefreshToken,
    IEnumerable<string> Errors
);