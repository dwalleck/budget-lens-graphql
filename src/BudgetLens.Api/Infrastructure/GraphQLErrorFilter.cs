using HotChocolate;

namespace BudgetLens.Api.Infrastructure;

/// <summary>
/// Error filter for GraphQL to handle and format errors appropriately.
/// </summary>
public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        // Log the error (you could inject ILogger here)
        
        // For now, return the error as-is, but we can add custom error handling here
        // For example, you might want to:
        // - Sanitize error messages in production
        // - Convert domain exceptions to GraphQL error codes
        // - Add correlation IDs for tracking
        
        return error;
    }
}