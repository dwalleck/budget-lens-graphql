using MediatR;
using Microsoft.AspNetCore.Identity;
using BudgetLens.Core.Domain.Users;
using BudgetLens.Core.Domain.Interfaces;
using BudgetLens.Infrastructure.Identity;

namespace BudgetLens.Application.Users.Commands;

/// <summary>
/// Handler for user registration command.
/// </summary>
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IEventStore _eventStore;

    public RegisterUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenService tokenService,
        IEventStore eventStore)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _eventStore = eventStore;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Create domain user aggregate
            var domainUserId = Guid.NewGuid();
            var domainUser = new User(domainUserId, request.Email, request.Username);
            
            if (!string.IsNullOrEmpty(request.FirstName) || !string.IsNullOrEmpty(request.LastName))
            {
                domainUser.UpdateProfile(request.FirstName, request.LastName);
            }

            // Create identity user
            var identityUser = new ApplicationUser(request.Email, request.Username)
            {
                DomainUserId = domainUserId,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            // Create identity user with password
            var result = await _userManager.CreateAsync(identityUser, request.Password);
            
            if (!result.Succeeded)
            {
                return new RegisterUserResult(
                    Success: false,
                    UserId: null,
                    AccessToken: null,
                    RefreshToken: null,
                    Errors: result.Errors.Select(e => e.Description)
                );
            }

            // Save domain events
            await _eventStore.SaveEventsAsync(domainUser.Id, nameof(User), domainUser.GetUncommittedEvents(), 0);
            domainUser.ClearUncommittedEvents();

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(domainUserId, request.Email, request.Username);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return new RegisterUserResult(
                Success: true,
                UserId: domainUserId,
                AccessToken: accessToken,
                RefreshToken: refreshToken,
                Errors: Array.Empty<string>()
            );
        }
        catch (Exception ex)
        {
            return new RegisterUserResult(
                Success: false,
                UserId: null,
                AccessToken: null,
                RefreshToken: null,
                Errors: new[] { ex.Message }
            );
        }
    }
}