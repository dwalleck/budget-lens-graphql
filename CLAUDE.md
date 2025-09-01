# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Technology Stack
- .NET 10 (preview) with C# preview features enabled
- ASP.NET Core Web API with GraphQL (HotChocolate framework)
- .NET Aspire for service orchestration and distributed application hosting
- PostgreSQL database with Npgsql Entity Framework provider
- Clean Architecture pattern with separate layers

## Development Commands
- Build entire solution: `dotnet build`
- Run application with Aspire orchestration: `dotnet run --project src/AppHost`
- Run API standalone: `dotnet run --project src/BudgetLens.Api`
- Test: `dotnet test` (when tests are added to tests/ folder)
- Restore packages: `dotnet restore`

## Architecture Overview
This project follows Clean Architecture principles with clear separation of concerns:

- **AppHost**: .NET Aspire orchestration host that sets up PostgreSQL and API service
- **BudgetLens.Api**: Web API layer with GraphQL endpoint (HotChocolate), serves as presentation layer
- **BudgetLens.Application**: Application layer for business logic, commands, queries, and behaviors (CQRS pattern)
- **BudgetLens.Core**: Domain layer containing entities, value objects, events, and core services
- **BudgetLens.Infrastructure**: Infrastructure layer for data persistence, configuration, and external services

## Key Technologies and Patterns
- GraphQL API using HotChocolate 15.x with code-first approach
- .NET Aspire for local development orchestration with PostgreSQL container
- Clean Architecture with dependency inversion
- CQRS pattern preparation (Commands/Queries folders in Application layer)
- Entity Framework Core with PostgreSQL provider
- Nullable reference types enabled across all projects

## Important Notes
- The API layer runs on .NET 8.0 while other layers target .NET 10 preview
- GraphQL schema is defined using HotChocolate attributes and code-first approach
- Currently has basic Book/Author example types - likely to be replaced with budget-related domain models
- Infrastructure layer includes health checks for PostgreSQL
- Project uses preview language features and .NET 10 preview runtime