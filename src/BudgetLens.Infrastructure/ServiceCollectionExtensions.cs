using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BudgetLens.Core.Domain.Interfaces;
using BudgetLens.Infrastructure.Data;
using BudgetLens.Infrastructure.EventStore;
using BudgetLens.Infrastructure.Identity;

namespace BudgetLens.Infrastructure;

/// <summary>
/// Extension methods for configuring infrastructure services in dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add all infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configuration">Application configuration</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add database context
        services.AddDbContext<BudgetLensDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured");

            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
                
                // Use snake_case naming convention
                npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            // Configure snake_case naming globally
            options.UseSnakeCaseNamingConvention();
        });

        // Add Identity
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            
            // User settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false; // For MVP, skip email confirmation
        })
        .AddEntityFrameworkStores<BudgetLensDbContext>()
        .AddDefaultTokenProviders();

        // Add JWT token service
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Add event store
        services.AddScoped<IEventStore, EventStore.EventStore>();

        // Add health checks for database
        services.AddHealthChecks()
            .AddDbContextCheck<BudgetLensDbContext>("database");

        return services;
    }

    /// <summary>
    /// Ensure the database is created and up-to-date with migrations.
    /// Call this during application startup.
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    /// <returns>Task representing the async operation</returns>
    public static async Task EnsureDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BudgetLensDbContext>();
        
        await context.Database.MigrateAsync();
    }
}