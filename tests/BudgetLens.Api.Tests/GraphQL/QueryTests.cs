using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using FluentAssertions;

namespace BudgetLens.Api.Tests.GraphQL;

/// <summary>
/// Integration tests for GraphQL queries.
/// </summary>
public class QueryTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly TestWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public QueryTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Hello_Query_Should_Return_Greeting()
    {
        // Arrange
        var query = """
            {
                hello
            }
            """;

        var request = new
        {
            query = query
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        
        responseContent.Should().Contain("Hello from Budget Lens GraphQL API!");
        responseContent.Should().Contain("Current time:");
    }

    [Fact]
    public async Task Version_Query_Should_Return_Api_Info()
    {
        // Arrange
        var query = """
            {
                version {
                    version
                    name
                    buildDate
                }
            }
            """;

        var request = new
        {
            query = query
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        
        responseContent.Should().Contain("1.0.0");
        responseContent.Should().Contain("Budget Lens MVP");
    }

    [Fact]
    public async Task GraphQL_Schema_Should_Be_Accessible()
    {
        // Arrange
        var introspectionQuery = """
            {
                __schema {
                    types {
                        name
                        kind
                    }
                }
            }
            """;

        var request = new
        {
            query = introspectionQuery
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        // Act
        var response = await _client.PostAsync("/graphql", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        
        responseContent.Should().Contain("Query");
        responseContent.Should().Contain("ApiVersion");
    }
}