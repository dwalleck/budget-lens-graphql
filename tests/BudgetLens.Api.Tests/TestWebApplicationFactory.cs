using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BudgetLens.Infrastructure.Data;

namespace BudgetLens.Api.Tests;

/// <summary>
/// Custom WebApplicationFactory for testing with in-memory database.
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the real DbContext registration
            services.RemoveAll(typeof(DbContextOptions<BudgetLensDbContext>));
            services.RemoveAll(typeof(BudgetLensDbContext));

            // Add in-memory database for testing
            services.AddDbContext<BudgetLensDbContext>(options =>
            {
                options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}");
            });

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BudgetLensDbContext>();
            
            // Ensure the database is created
            context.Database.EnsureCreated();
        });

        builder.UseEnvironment("Testing");
    }
}