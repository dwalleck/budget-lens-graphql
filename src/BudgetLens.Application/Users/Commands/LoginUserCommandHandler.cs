using MediatR;
using Microsoft.AspNetCore.Identity;
using BudgetLens.Core.Domain.Users;
using BudgetLens.Core.Domain.Interfaces;
using BudgetLens.Infrastructure.Identity;

namespace BudgetLens.Application.Users.Commands;

/// <summary>
/// Handler for user login command.
/// </summary>
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IEventStore _eventStore;

    public LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenService tokenService,
        IEventStore eventStore)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _eventStore = eventStore;
    }

    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Find user by email or username
            var user = await _userManager.FindByEmailAsync(request.EmailOrUsername) 
                      ?? await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (user == null || !user.IsActive)
            {
                return new LoginUserResult(
                    Success: false,
                    UserId: null,
                    AccessToken: null,
                    RefreshToken: null,
                    Errors: new[] { "Invalid email/username or password" }
                );
            }

            // Check password
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return new LoginUserResult(
                    Success: false,
                    UserId: null,
                    AccessToken: null,
                    RefreshToken: null,
                    Errors: new[] { "Invalid email/username or password" }
                );
            }

            // Update last login time
            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            // Record login in domain model if we have a domain user ID
            if (user.DomainUserId.HasValue)
            {
                var domainUserId = user.DomainUserId.Value;
                var events = await _eventStore.LoadEventsAsync(domainUserId);
                var domainUser = User.FromEvents(domainUserId, events);
                
                domainUser.RecordLogin();
                
                await _eventStore.SaveEventsAsync(domainUser.Id, nameof(User), domainUser.GetUncommittedEvents(), events.Count());
                domainUser.ClearUncommittedEvents();
            }

            // Generate tokens
            var domainUserIdForToken = user.DomainUserId ?? user.Id;
            var accessToken = _tokenService.GenerateAccessToken(domainUserIdForToken, user.Email!, user.UserName!);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new LoginUserResult(
                Success: true,
                UserId: domainUserIdForToken,
                AccessToken: accessToken,
                RefreshToken: refreshToken,
                Errors: Array.Empty<string>()
            );
        }
        catch (Exception ex)
        {
            return new LoginUserResult(
                Success: false,
                UserId: null,
                AccessToken: null,
                RefreshToken: null,
                Errors: new[] { ex.Message }
            );
        }
    }
}