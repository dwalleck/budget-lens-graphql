using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BudgetLens.Infrastructure.Data;

/// <summary>
/// Design-time factory for BudgetLensDbContext, used by Entity Framework tools.
/// </summary>
public class BudgetLensDbContextFactory : IDesignTimeDbContextFactory<BudgetLensDbContext>
{
    public BudgetLensDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BudgetLensDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Database=budget_lens;Username=postgres;Password=postgres;Port=5432";

        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null);
        });

        // Use snake_case naming convention
        optionsBuilder.UseSnakeCaseNamingConvention();

        return new BudgetLensDbContext(optionsBuilder.Options);
    }
}