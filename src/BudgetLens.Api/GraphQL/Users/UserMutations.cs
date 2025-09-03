using MediatR;
using BudgetLens.Application.Users.Commands;

namespace BudgetLens.Api.GraphQL.Users;

/// <summary>
/// GraphQL mutations for user management.
/// </summary>
public class UserMutations
{
    /// <summary>
    /// Register a new user account.
    /// </summary>
    public async Task<AuthPayload> RegisterAsync(
        RegisterUserInput input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            input.Email,
            input.Username,
            input.Password,
            input.FirstName,
            input.LastName
        );

        var result = await mediator.Send(command, cancellationToken);

        return new AuthPayload(
            result.Success,
            result.UserId,
            result.AccessToken,
            result.RefreshToken,
            result.Errors.ToArray()
        );
    }

    /// <summary>
    /// Log in an existing user.
    /// </summary>
    public async Task<AuthPayload> LoginAsync(
        LoginUserInput input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(input.EmailOrUsername, input.Password);
        var result = await mediator.Send(command, cancellationToken);

        return new AuthPayload(
            result.Success,
            result.UserId,
            result.AccessToken,
            result.RefreshToken,
            result.Errors.ToArray()
        );
    }
}

/// <summary>
/// Input for user registration.
/// </summary>
public record RegisterUserInput(
    string Email,
    string Username,
    string Password,
    string? FirstName,
    string? LastName
);

/// <summary>
/// Input for user login.
/// </summary>
public record LoginUserInput(
    string EmailOrUsername,
    string Password
);

/// <summary>
/// Authentication result payload.
/// </summary>
public record AuthPayload(
    bool Success,
    Guid? UserId,
    string? AccessToken,
    string? RefreshToken,
    string[] Errors
);