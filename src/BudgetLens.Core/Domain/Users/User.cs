using BudgetLens.Core.Domain.Common;

namespace BudgetLens.Core.Domain.Users;

/// <summary>
/// User aggregate root representing a Budget Lens user account.
/// </summary>
public class User : AggregateRoot
{
    private User() { } // Required for EF Core

    public User(Guid id, string email, string username) : base(id)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required", nameof(username));

        Email = email;
        Username = username;
        CreatedAt = DateTime.UtcNow;
        IsEmailVerified = false;
        IsActive = true;

        AddDomainEvent(new UserCreatedEvent(Id, email, username, CreatedAt));
    }

    public string Email { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsActive { get; private set; }

    public void UpdateProfile(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        
        AddDomainEvent(new UserProfileUpdatedEvent(Id, firstName, lastName, DateTime.UtcNow));
    }

    public void VerifyEmail()
    {
        if (!IsEmailVerified)
        {
            IsEmailVerified = true;
            AddDomainEvent(new UserEmailVerifiedEvent(Id, Email, DateTime.UtcNow));
        }
    }

    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        AddDomainEvent(new UserLoggedInEvent(Id, LastLoginAt.Value));
    }

    public void Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            AddDomainEvent(new UserDeactivatedEvent(Id, DateTime.UtcNow));
        }
    }

    public void Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            AddDomainEvent(new UserActivatedEvent(Id, DateTime.UtcNow));
        }
    }

    /// <summary>
    /// Reconstruct User aggregate from domain events.
    /// </summary>
    public static User FromEvents(Guid id, IEnumerable<DomainEvent> events)
    {
        var user = new User { Id = id };
        
        foreach (var @event in events)
        {
            user.ApplyEvent(@event);
        }
        
        return user;
    }

    protected override void ApplyEvent(DomainEvent @event)
    {
        switch (@event)
        {
            case UserCreatedEvent created:
                Email = created.Email;
                Username = created.Username;
                CreatedAt = created.CreatedAt;
                IsEmailVerified = false;
                IsActive = true;
                break;
                
            case UserProfileUpdatedEvent profileUpdated:
                FirstName = profileUpdated.FirstName;
                LastName = profileUpdated.LastName;
                break;
                
            case UserEmailVerifiedEvent emailVerified:
                IsEmailVerified = true;
                break;
                
            case UserLoggedInEvent loggedIn:
                LastLoginAt = loggedIn.LoginAt;
                break;
                
            case UserDeactivatedEvent deactivated:
                IsActive = false;
                break;
                
            case UserActivatedEvent activated:
                IsActive = true;
                break;
        }
    }
}