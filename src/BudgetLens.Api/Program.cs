using BudgetLens.Infrastructure;
using BudgetLens.Api.Infrastructure;
using BudgetLens.Api.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add infrastructure services (database, event store)
builder.Services.AddInfrastructure(builder.Configuration);

// Add GraphQL server with HotChocolate
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddErrorFilter<GraphQLErrorFilter>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseRouting();

// Map GraphQL endpoint
app.MapGraphQL();

// Note: GraphQL Voyager temporarily disabled due to version compatibility

// Ensure database is ready
await app.Services.EnsureDatabaseAsync();

app.RunWithGraphQLCommands(args);
