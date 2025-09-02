namespace BudgetLens.Api.GraphQL;

/// <summary>
/// Root GraphQL query type for Budget Lens API.
/// </summary>
public class Query
{
    /// <summary>
    /// Simple health check query to verify GraphQL is working.
    /// </summary>
    /// <returns>A simple greeting message with the current time.</returns>
    public string Hello() => $"Hello from Budget Lens GraphQL API! Current time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC";

    /// <summary>
    /// Get API version information.
    /// </summary>
    /// <returns>API version details.</returns>
    public ApiVersion Version() => new("1.0.0", "Budget Lens MVP", DateTime.UtcNow);
}

/// <summary>
/// API version information.
/// </summary>
/// <param name="Version">The API version string.</param>
/// <param name="Name">The API name.</param>
/// <param name="BuildDate">When this API instance was built.</param>
public record ApiVersion(string Version, string Name, DateTime BuildDate);