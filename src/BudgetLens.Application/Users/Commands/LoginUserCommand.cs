using MediatR;

namespace BudgetLens.Application.Users.Commands;

/// <summary>
/// Command to log in a user.
/// </summary>
public record LoginUserCommand(
    string EmailOrUsername,
    string Password
) : IRequest<LoginUserResult>;

/// <summary>
/// Result of user login.
/// </summary>
public record LoginUserResult(
    bool Success,
    Guid? UserId,
    string? AccessToken,
    string? RefreshToken,
    IEnumerable<string> Errors
);