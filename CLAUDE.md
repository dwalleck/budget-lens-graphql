# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with the Budget Lens personal finance management platform.

## Product Overview

Budget Lens is a comprehensive personal finance management platform that empowers individuals and families to take control of their financial lives through:

- **7 flexible budgeting strategies** (Traditional, Envelope, Percentage-based, Priority-based, Rolling, Value-based, Hybrid)
- **ML-powered automation** for transaction categorization and anomaly detection
- **Complete audit trail** through event sourcing architecture
- **Family collaboration** with granular permissions and household management
- **Comprehensive debt management** with multiple payoff strategies
- **Predictive insights** and cash flow forecasting

## Technology Stack

- **Backend:** .NET 10 (preview) with C# preview features enabled
- **API:** GraphQL using HotChocolate 15.x framework
- **Database:** PostgreSQL with Event Sourcing pattern
- **Caching:** Redis for performance optimization
- **ML Framework:** ML.NET for categorization and predictions
- **Architecture:** Hexagonal Architecture with CQRS/Event Sourcing
- **Messaging:** MediatR for in-process commands/queries
- **Orchestration:** .NET Aspire for service management and PostgreSQL container

## Development Commands

- Build entire solution: `dotnet build`
- Run application with Aspire orchestration: `dotnet run --project src/AppHost`
- Run API standalone: `dotnet run --project src/BudgetLens.Api`
- Test: `dotnet test` (when tests are added to tests/ folder)
- Restore packages: `dotnet restore`

## Architecture Overview

This project follows Hexagonal Architecture with Clean Architecture principles:

- **AppHost**: .NET Aspire orchestration host managing PostgreSQL and API services
- **BudgetLens.Api**: Presentation layer with GraphQL API endpoints
- **BudgetLens.Application**: Application layer implementing CQRS pattern with commands, queries, and handlers
- **BudgetLens.Core**: Domain layer containing aggregates, entities, value objects, domain events, and services
- **BudgetLens.Infrastructure**: Infrastructure layer for data persistence, event store, external services, and ML models

## Domain Model Guidelines

The core domain includes these key aggregates and concepts:

### Financial Aggregates
- **Account**: Bank accounts, credit cards, investments, loans with transaction history
- **Transaction**: Financial transactions with categorization, splits, and audit trail
- **Budget**: Multiple budget strategy implementations (envelope, percentage, priority, etc.)
- **Household**: Family financial management with member roles and shared resources

### Financial Concepts
- **Categories**: Hierarchical categorization system with ML-powered auto-assignment
- **Bills**: Recurring payment tracking with smart reminders
- **Debts**: Debt portfolio with payoff strategy calculations
- **Insights**: ML-generated financial recommendations and anomaly detection

## Event Sourcing Implementation

All financial data changes are captured as domain events in an immutable event store:

```csharp
public abstract record DomainEvent(
    Guid EventId,
    DateTime OccurredAt,
    Guid UserId,
    string EventType,
    int Version
);

// Example events
public record TransactionCreatedEvent : DomainEvent;
public record TransactionCategorizedEvent : DomainEvent;
public record BudgetExceededEvent : DomainEvent;
```

## Key Technical Patterns

- **Event Sourcing**: Complete audit trail with temporal queries and state reconstruction
- **CQRS**: Separate command/query models for optimal read/write performance
- **Hexagonal Architecture**: Domain isolation with ports/adapters pattern
- **GraphQL Subscriptions**: Real-time updates for transaction processing
- **ML Integration**: ML.NET models for auto-categorization and anomaly detection
- **Multi-tenancy**: Household-based data isolation and sharing

## Performance Requirements

- **API Response Time**: <200ms for 95th percentile queries
- **ML Prediction Time**: <100ms for transaction categorization
- **Event Store Queries**: Sub-second historical data retrieval
- **Real-time Updates**: GraphQL subscriptions for live transaction processing
- **Throughput**: Support for 10,000+ concurrent users

## Security Considerations

- **Authentication**: OAuth 2.0/OpenID Connect integration
- **Authorization**: Role-based access control with household permissions
- **Data Encryption**: AES-256 at rest, TLS 1.3 in transit
- **Audit Logging**: All financial data access tracked in event store
- **PCI Compliance**: Secure handling of financial data without storing card numbers

## Development Phases

### Phase 1 - MVP (Current Focus)
- Core account and transaction management
- Basic budgeting and categorization
- User authentication and household setup
- Simple reporting and GraphQL API

### Future Phases
- Advanced budgeting strategies (7 types)
- ML-powered automation and insights
- Debt management and payoff strategies
- Public API and integration ecosystem

## Important Notes

- All financial calculations use `decimal` type for precision
- Event store provides complete transaction history and audit trail
- GraphQL schema follows domain-driven design principles
- ML models require training data from user transaction patterns
- Household permissions enable family financial collaboration
- Performance monitoring critical for financial application reliability
