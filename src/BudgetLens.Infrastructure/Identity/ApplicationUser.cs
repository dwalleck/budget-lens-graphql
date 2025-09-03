using Microsoft.AspNetCore.Identity;
using BudgetLens.Core.Domain.Users;

namespace BudgetLens.Infrastructure.Identity;

/// <summary>
/// Application user for ASP.NET Core Identity integration.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser() : base() 
    {
        Id = Guid.NewGuid();
    }

    public ApplicationUser(string email, string userName) : this()
    {
        Email = email;
        UserName = userName;
        EmailConfirmed = false;
    }

    /// <summary>
    /// Domain user aggregate ID.
    /// </summary>
    public Guid? DomainUserId { get; set; }
    
    /// <summary>
    /// First name from user profile.
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Last name from user profile.
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Date when user account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date of last successful login.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
    
    /// <summary>
    /// Whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}