# Budget Lens MVP Development Plan

## Personal Finance Platform - Lean MVP Implementation

**Version:** 1.0  
**Date:** September 2025  
**Status:** Active Development Plan

---

## Executive Summary

This document outlines the development plan for Budget Lens MVP, a lean implementation of the comprehensive personal finance platform defined in [prd.md](./prd.md) and [architecture.md](./architecture.md). The MVP maintains sophisticated architectural patterns while dramatically reducing feature scope to achieve faster time-to-market and rapid user feedback.

### Technical Constraints (Non-Negotiable)

- ✅ .NET 10 with preview features
- ✅ GraphQL API using HotChocolate 15.x
- ✅ Hexagonal Architecture with clear domain boundaries
- ✅ CQRS pattern with MediatR
- ✅ Domain events for auditing capabilities
- ✅ Multiple accounts per user support
- ✅ PostgreSQL with extensible schema design

### MVP Success Criteria

- **Time to Market:** 3-4 months from development start
- **User Validation:** 100 active users within first month post-launch
- **Feature Adoption:** 70% of users create budgets and categorize transactions
- **Technical Debt:** <20% additional effort for Phase 2 feature additions
- **Architecture Integrity:** Zero architectural rewrites required for full vision

---

## Feature Scope Comparison

### MVP Features (Phase 1) ✅

| **Feature** | **MVP Scope** | **Full Vision Reference** |
|-------------|---------------|---------------------------|
| **User Management** | Email/password authentication, single user profiles | [PRD Section 3.1.1](./prd.md#user-management) - OAuth/SSO deferred |
| **Account Management** | Multiple accounts (checking, savings, credit), manual balance tracking | [Architecture Section 2.3](./architecture.md#primary-financial-entities) - Full account types supported |
| **Transaction Management** | Manual entry, basic CRUD, categorization, audit trail | [PRD Section 3.1.1](./prd.md#account-management) - Bank integration deferred |
| **Category System** | Flat hierarchy, predefined + custom categories | [Architecture Section 2.2](./architecture.md#core-domain-concepts) - Advanced hierarchy deferred |
| **Traditional Budgeting** | Monthly category-based budgets, overspending alerts | [PRD Section 3.1.2](./prd.md#basic-budgeting) - 1 of 7 budget strategies |
| **Basic Reporting** | Account summaries, budget vs actual, spending by category | [PRD Section 3.1.4](./prd.md#basic-reporting) - Advanced analytics deferred |
| **Event Auditing** | Domain events for critical operations, change tracking | [Architecture Section 4.6](./architecture.md#event-sourcing-integration) - Simplified event store |

### Deferred Features (Future Phases) ⏸️

| **Deferred Feature** | **PRD Reference** | **Target Phase** |
|---------------------|------------------|------------------|
| 6 Advanced Budget Strategies | [PRD Section 3.2.2](./prd.md#flexible-budgeting-strategies) | Phase 2-3 |
| ML Auto-Categorization | [PRD Section 3.3.1](./prd.md#ml-powered-auto-categorization) | Phase 3 |
| Household Management | [PRD Section 3.2.1](./prd.md#family-household-management) | Phase 2 |
| Bank Integration | [PRD Dependencies](./prd.md#external-dependencies) | Phase 2-3 |
| Debt Management Suite | [PRD Section 3.4](./prd.md#debt-management) | Phase 4 |
| Advanced Analytics/Insights | [PRD Section 3.3.2](./prd.md#spending-insights--anomaly-detection) | Phase 3-4 |
| Mobile Applications | [PRD Section 5.3](./prd.md#platform-support) | Phase 4+ |

---

## Technical Architecture Decisions

### Core Architectural Patterns (From [architecture.md](./architecture.md))

**Hexagonal Architecture Implementation:**

```
┌─────────────────────────────────────────────────────────────────────┐
│                     GRAPHQL API LAYER (Primary Adapter)            │
│          HotChocolate Resolvers, Mutations, Subscriptions          │
└─────────────────┬───────────────────────────────────────────────────┘
                  │
┌─────────────────▼─────────────────────────────────────────────────────┐
│                    APPLICATION LAYER (CQRS)                          │
│   Commands: CreateAccount, AddTransaction, CreateBudget              │
│   Queries: GetAccount, GetTransactions, GetBudgetPerformance         │
│   Event Handlers: TransactionAdded, BudgetExceeded                   │
└─────────────────┬─────────────────────────────────────────────────────┘
                  │
┌─────────────────▼─────────────────────────────────────────────────────┐
│                      DOMAIN LAYER (CORE)                             │
│   Aggregates: User, Account, Transaction, Category, Budget           │
│   Domain Events: AccountCreated, TransactionCategorized              │
│   Domain Services: BudgetCalculationService                          │
└─────────────────┬─────────────────────────────────────────────────────┘
                  │
┌─────────────────▼─────────────────────────────────────────────────────┐
│                  INFRASTRUCTURE LAYER (Secondary Adapters)           │
│   PostgreSQL Repositories, Event Store, External Service Adapters    │
└───────────────────────────────────────────────────────────────────────┘
```

### MVP-Specific Simplifications

**Domain Model Reduction:**

- **5 Core Aggregates:** User, Account, Transaction, Category, Budget
- **Essential Value Objects:** Money, DateRange, BudgetPeriod
- **Critical Domain Events:** Account/Transaction/Budget lifecycle events
- **Single Budget Strategy:** Traditional category-based budgeting

**GraphQL Schema Design:**

- Interface-based polymorphic types for future budget strategies
- Connection-based pagination for scalability
- Extensible filter and sort inputs
- Subscription support for real-time updates

### DevOps and Deployment Strategy

To mitigate risks associated with a last-minute "big bang" deployment, this plan incorporates an iterative DevOps strategy from the beginning.

- **Infrastructure as Code (IaC):** All cloud infrastructure (e.g., databases, app services, networking) will be defined using a declarative IaC tool (e.g., Terraform, Bicep). This ensures environments are repeatable, auditable, and easy to manage.
- **Database Migration Strategy:** Automated schema migrations integrated into CI/CD pipeline with rollback capability:
  - **Development:** Entity Framework migrations applied automatically on application startup
  - **Staging:** Migrations executed as part of deployment pipeline before application deployment
  - **Production:** Migration validation step with manual approval gate, automated rollback procedures, and zero-downtime deployment patterns
  - **Migration Testing:** All migrations tested in staging environment with production-like data volumes
- **CI/CD Pipeline:** A continuous integration and continuous deployment pipeline will be established in Week 1 using GitHub Actions (or a similar tool).
  - **Continuous Integration:** On every pull request, the pipeline will automatically:
        1. Build the .NET solution.
        2. Run all unit and integration tests.
        3. Execute database migration validation against test database.
        4. Report status back to the PR.
  - **Continuous Deployment:** On every merge to the `main` branch, the pipeline will automatically deploy the application to the `Staging` environment. Deployment to `Production` will be a manually triggered step after successful staging validation.
- **Environments:** Three distinct environments will be maintained:
  - **Development:** Local developer machines with Docker Compose PostgreSQL.
  - **Staging:** A production-like environment for automated deployments and user acceptance testing.
  - **Production:** The live environment for end-users with high availability configuration.
- **Monitoring and Logging:** A structured logging, monitoring, and alerting solution (e.g., OpenTelemetry with Azure Monitor/Datadog) will be configured from the start. This ensures immediate visibility into application health in all environments.

### Testing and Quality Assurance Strategy

A multi-layered testing strategy is critical for maintaining quality and development velocity within a Hexagonal Architecture. All tests will be integrated into the CI/CD pipeline.

- **Level 1: Unit Tests (xUnit)**
  - **Scope:** Focused on the **Domain Layer** and **Application Layer**.
  - **Domain:** Test aggregate business logic, domain events, and value object invariants in complete isolation. No external dependencies (no database, no network).
  - **Application:** Test CQRS command/query handlers by mocking repository and service interfaces (`IAccountRepository`, `IUnitOfWork`, etc.).
  - **Goal:** Achieve >90% code coverage on core business logic. These tests should be extremely fast.

- **Level 2: Integration Tests**
  - **Scope:** Focused on the **Infrastructure Layer** and validating application logic against real dependencies.
  - **Infrastructure:** Test repository implementations against a real, containerized PostgreSQL database to verify data access logic and EF Core mappings.
  - **Application:** Test the full MediatR pipeline for key commands/queries, ensuring they integrate correctly with the database and other infrastructure components.
  - **Goal:** Ensure adapters work as expected and validate logic against real-world dependencies.

- **Level 3: End-to-End (E2E) Tests**
  - **Scope:** Focused on the **API Layer**.
  - **Method:** Tests will be written from the perspective of a client application. They will send real GraphQL queries and mutations to an in-memory test server (`HotChocolate.AspNetCore.Tests`) running the full application stack against a test database.
  - **Goal:** Verify that critical user flows (e.g., creating a user, adding a transaction, and seeing a budget update) work correctly through the entire system.

- **Level 4: Performance Testing (k6)**
  - **Scope:** Load and performance validation of GraphQL API endpoints under realistic usage patterns.
  - **Tool:** k6 for JavaScript-based performance testing with GraphQL support.
  - **Test Scenarios:**
    - **Baseline Load:** 50 concurrent users performing typical operations (login, view accounts, add transactions)
    - **Peak Load:** 200 concurrent users simulating high-traffic periods
    - **Stress Test:** 500+ concurrent users to identify breaking points
    - **Endurance Test:** 100 concurrent users over 30 minutes to detect memory leaks
  - **Performance Targets:**
    - **API Response Time:** <200ms for 95th percentile under baseline load
    - **GraphQL Query Performance:** <100ms for simple queries, <500ms for complex aggregations
    - **Database Performance:** <50ms average query execution time
    - **Throughput:** >1000 requests per minute sustained
  - **k6 Test Structure:**

        ```javascript
        // Example k6 test for Budget Lens GraphQL API
        import http from 'k6/http';
        import { check } from 'k6';
        import { Rate } from 'k6/metrics';

        export let errorRate = new Rate('errors');
        export let options = {
          scenarios: {
            baseline: {
              executor: 'constant-vus',
              vus: 50,
              duration: '5m',
            },
            peak: {
              executor: 'ramping-vus',
              startVUs: 0,
              stages: [
                { duration: '2m', target: 200 },
                { duration: '5m', target: 200 },
                { duration: '2m', target: 0 },
              ],
            },
          },
        };
        ```

  - **GraphQL Performance Scenarios:**
    - User authentication and profile queries
    - Account balance and transaction history retrieval
    - Budget performance calculations
    - Real-time subscription load testing
    - Complex filtering and pagination queries

- **Level 5: Security Testing**
  - **Scope:** Comprehensive security validation for financial application requirements.
  - **OWASP Dependency Scanning:**
    - **Tool:** OWASP Dependency-Check integrated into CI pipeline
    - **Scope:** Scan all .NET packages and JavaScript dependencies for known vulnerabilities
    - **Frequency:** Every build and weekly scheduled scans
    - **Action:** Block deployment if high/critical vulnerabilities found
  - **GraphQL-Specific Security Testing:**
    - **Query Depth Limiting:** Validate protection against deeply nested queries (max depth: 10)
    - **Query Complexity Analysis:** Test protection against expensive queries (max complexity: 1000)
    - **Rate Limiting:** Validate API rate limiting (100 requests/minute per user)
    - **Authorization Testing:** Verify users can only access their own financial data
    - **Input Validation:** Test GraphQL input sanitization and validation
  - **Authentication & Authorization Testing:**
    - **JWT Token Security:** Validate token expiration, rotation, and blacklisting
    - **Password Security:** Test password strength requirements and hashing (bcrypt)
    - **Session Management:** Verify secure session handling and timeout policies
    - **Multi-tenancy Security:** Test data isolation between users
  - **Financial Data Protection:**
    - **Data Encryption:** Verify sensitive data encryption at rest (AES-256)
    - **PCI DSS Compliance:** Ensure no storage of full credit card numbers
    - **Audit Trail Security:** Validate event store tamper protection
    - **Data Export Security:** Test CSV/PDF export authorization and data masking
  - **Infrastructure Security:**
    - **TLS Configuration:** Verify TLS 1.3 encryption for all API communications
    - **Database Security:** Test PostgreSQL connection security and access controls
    - **Environment Secrets:** Validate proper secret management (Azure Key Vault/AWS Secrets Manager)
    - **Container Security:** Scan Docker images for vulnerabilities

---

## Future-Proofing Strategy

### Interface-Based Design for Extension

**Budget Strategy Polymorphism:**

```csharp
// Designed for MVP but extensible to 7 strategies from PRD Section 3.2.2
public interface IBudget
{
    BudgetType Type { get; }
    BudgetPeriod Period { get; }
    Task<BudgetPerformance> CalculatePerformanceAsync(IEnumerable<Transaction> transactions);
    bool CanAllocateTransaction(Transaction transaction);
}

// MVP Implementation
public class TraditionalBudget : IBudget
{
    public BudgetType Type => BudgetType.TRADITIONAL;
    // ... implementation
}

// Future Phase 2 Implementations (from PRD)
public class EnvelopeBudget : IBudget { }
public class PercentageBudget : IBudget { }
// ... 5 additional strategies
```

**Authentication Provider Abstraction:**

```csharp
// MVP: Simple email/password
// Future: OAuth, SSO, Multi-factor (PRD Section 4.3)
public interface IAuthenticationProvider
{
    Task<AuthResult> AuthenticateAsync(AuthRequest request);
    Task<User> RegisterAsync(RegistrationRequest request);
}
```

**Transaction Processing Pipeline:**

```csharp
// MVP: Manual entry
// Future: Bank integration, ML categorization (PRD Section 3.3.1)
public interface ITransactionProcessor
{
    Task<Transaction> ProcessTransactionAsync(RawTransactionData data);
    Task<CategoryPrediction> PredictCategoryAsync(Transaction transaction);
}
```

### Database Schema Evolution Strategy

**Extensible Core Tables:**

```sql
-- Users table designed for household expansion (PRD Section 3.2.1)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    currency VARCHAR(3) DEFAULT 'USD',
    preferences JSONB DEFAULT '{}',          -- Extensible user settings
    household_id UUID,                       -- Future household support
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    updated_at TIMESTAMP NOT NULL DEFAULT now()
);

-- Accounts with sharing capability built-in
CREATE TABLE accounts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    owner_id UUID NOT NULL REFERENCES users(id),
    household_id UUID,                       -- Future shared accounts
    name VARCHAR(255) NOT NULL,
    account_type VARCHAR(50) NOT NULL,
    balance DECIMAL(15,2) NOT NULL DEFAULT 0.00,
    institution VARCHAR(255),
    metadata JSONB DEFAULT '{}',             -- Bank integration data
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    updated_at TIMESTAMP NOT NULL DEFAULT now()
);

-- Transactions with ML prediction support
CREATE TABLE transactions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(id),
    category_id UUID REFERENCES categories(id),
    amount DECIMAL(15,2) NOT NULL,
    transaction_date DATE NOT NULL,
    description TEXT NOT NULL,
    transaction_type VARCHAR(20) NOT NULL,
    status VARCHAR(20) DEFAULT 'CLEARED',
    
    -- Future ML integration (PRD Section 3.3.1)
    predicted_category_id UUID REFERENCES categories(id),
    ml_confidence_score DECIMAL(5,4),
    auto_categorized BOOLEAN DEFAULT false,
    
    -- Future bank integration
    external_id VARCHAR(255),
    external_metadata JSONB DEFAULT '{}',
    
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    updated_at TIMESTAMP NOT NULL DEFAULT now()
);

-- Budgets designed for polymorphic strategies
CREATE TABLE budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id),
    name VARCHAR(255) NOT NULL,
    budget_type VARCHAR(50) NOT NULL,        -- TRADITIONAL, ENVELOPE, PERCENTAGE, etc.
    period_type VARCHAR(20) DEFAULT 'MONTHLY',
    settings JSONB DEFAULT '{}',             -- Strategy-specific configuration
    is_active BOOLEAN DEFAULT true,
    start_date DATE NOT NULL,
    end_date DATE,
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    updated_at TIMESTAMP NOT NULL DEFAULT now()
);

-- Categories with hierarchy support (deferred but planned)
CREATE TABLE categories (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id),
    name VARCHAR(255) NOT NULL,
    parent_id UUID REFERENCES categories(id), -- Future hierarchy support
    category_type VARCHAR(50) DEFAULT 'EXPENSE',
    color VARCHAR(7),                         -- Hex color code
    icon VARCHAR(50),
    is_system_category BOOLEAN DEFAULT false,
    created_at TIMESTAMP NOT NULL DEFAULT now()
);

-- Event Store for auditing (simplified initially, expandable)
CREATE TABLE domain_events (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    aggregate_id UUID NOT NULL,
    aggregate_type VARCHAR(100) NOT NULL,
    event_type VARCHAR(100) NOT NULL,
    event_version INTEGER NOT NULL DEFAULT 1,
    event_data JSONB NOT NULL,
    occurred_at TIMESTAMP NOT NULL DEFAULT now(),
    user_id UUID REFERENCES users(id),
    
    -- Future full event sourcing support
    stream_version INTEGER,
    correlation_id UUID,
    causation_id UUID
);

-- Indexes for performance
CREATE INDEX idx_transactions_account_date ON transactions(account_id, transaction_date DESC);
CREATE INDEX idx_transactions_category ON transactions(category_id) WHERE category_id IS NOT NULL;
CREATE INDEX idx_domain_events_aggregate ON domain_events(aggregate_id, occurred_at DESC);
CREATE INDEX idx_domain_events_type ON domain_events(event_type, occurred_at DESC);
```

### Versioned Domain Events

**Event Evolution Strategy:**

```csharp
// Event versioning for future compatibility (Architecture Section 4.6)
public abstract record DomainEvent(
    Guid EventId,
    DateTime OccurredAt,
    Guid UserId,
    string EventType,
    int EventVersion = 1  // Always include version for schema evolution
);

// Example: Transaction events with extensible data
public record TransactionCreatedEvent_V1(
    Guid EventId, DateTime OccurredAt, Guid UserId, string EventType, int EventVersion,
    Guid TransactionId, Guid AccountId, decimal Amount, string Description,
    Dictionary<string, object> ExtensionData = null  // Future field storage
) : DomainEvent(EventId, OccurredAt, UserId, EventType, EventVersion);

// Future version with ML prediction data
public record TransactionCreatedEvent_V2(
    Guid EventId, DateTime OccurredAt, Guid UserId, string EventType, int EventVersion,
    Guid TransactionId, Guid AccountId, decimal Amount, string Description,
    Guid? PredictedCategoryId, float? MLConfidenceScore,
    Dictionary<string, object> ExtensionData = null
) : DomainEvent(EventId, OccurredAt, UserId, EventType, EventVersion);
```

---

## Development Timeline & Milestones

### Phase 1: Foundation & Core Features (Weeks 1-6)

**Week 1-2: Project Setup & Architecture Foundation**

*Deliverables:*

- [ ] .NET 10 solution structure with Hexagonal Architecture
- [ ] PostgreSQL database with extensible schema (defined above)
- [ ] HotChocolate GraphQL server setup
- [ ] MediatR CQRS pipeline configuration
- [ ] **DevOps:** Initial CI/CD pipeline established (build, unit/integration test execution)
- [ ] **DevOps:** Staging environment provisioned using Infrastructure as Code (IaC)
- [ ] **DevOps:** Basic logging and monitoring configured and integrated
- [ ] **Testing:** Testing frameworks setup (TUnit for Unit/Integration)
- [ ] Basic authentication system (email/password)
- [ ] Domain event infrastructure

*Architecture Validation:*

- [ ] CI pipeline passes on every commit
- [ ] Database migrations are automatically applied in the staging environment
- [ ] GraphQL schema validation and error handling
- [ ] Command/Query separation working through MediatR

**Week 3-4: User & Account Management**

*Deliverables:*

- [ ] User registration and authentication
- [ ] Account aggregate implementation (multiple account types)
- [ ] Account CRUD operations via GraphQL
- [ ] Account balance management with events
- [ ] Basic account reconciliation features
- [ ] Account security (user can only access their accounts)

*Success Criteria:*

- [ ] Users can register and login
- [ ] Users can create/edit/delete multiple accounts
- [ ] Account balances update correctly with transactions
- [ ] All account operations generate proper domain events

**Week 5-6: Transaction Management**

*Deliverables:*

- [ ] Transaction aggregate with full CRUD
- [ ] Manual transaction entry via GraphQL
- [ ] Transaction categorization system
- [ ] Basic transaction filtering and search
- [ ] Transaction audit trail through events
- [ ] Account balance synchronization

*Success Criteria:*

- [ ] Users can add/edit/delete transactions manually
- [ ] Transactions properly update account balances
- [ ] Transaction history is fully auditable
- [ ] Transaction queries perform efficiently with pagination

### Phase 2: Budgeting & Categories (Weeks 7-10)

**Week 7-8: Category System**

*Deliverables:*

- [ ] Category aggregate (flat hierarchy for MVP)
- [ ] Predefined category set (20+ common categories)
- [ ] Custom category creation
- [ ] Category assignment to transactions
- [ ] Bulk transaction categorization
- [ ] Category-based transaction filtering

*Success Criteria:*

- [ ] Users can create and manage custom categories
- [ ] Transaction categorization is intuitive and fast
- [ ] Category filtering works across all transaction views
- [ ] System categories cannot be deleted

**Week 9-10: Traditional Budget Implementation**

*Deliverables:*

- [ ] Budget aggregate with Traditional budget strategy
- [ ] Monthly budget creation and management
- [ ] Category budget limits and tracking
- [ ] Budget performance calculations
- [ ] Overspending alerts and notifications
- [ ] Budget vs actual reporting

*Success Criteria:*

- [ ] Users can create monthly budgets with category limits
- [ ] Budget performance updates in real-time with transactions
- [ ] Overspending alerts trigger correctly
- [ ] Budget interface designed for future strategy expansion

### Phase 3: Dashboard & Polish (Weeks 11-14)

**Week 11-12: Reporting Dashboard**

*Deliverables:*

- [ ] Account summary dashboard
- [ ] Budget performance dashboard
- [ ] Spending by category charts
- [ ] Monthly spending trends
- [ ] Transaction search and filtering UI
- [ ] Export functionality (CSV/PDF)

*Success Criteria:*

- [ ] Dashboard loads quickly (<2 seconds)
- [ ] Charts and graphs are responsive and interactive
- [ ] Data export works correctly for all date ranges
- [ ] Mobile-responsive design

**Week 13-14: Hardening, Testing & Deployment**

*Deliverables:*

- [ ] **E2E Testing:** Complete test suite covering all critical user flows (registration, account management, transaction processing, budgeting)
- [ ] **Unit/Integration Testing:** Achieve >90% code coverage for domain logic and >80% for application layer
- [ ] **Performance Testing (k6):** Execute comprehensive load testing scenarios:
  - [ ] Baseline load testing (50 concurrent users for 5 minutes)
  - [ ] Peak load testing (200 concurrent users with ramp-up/ramp-down)
  - [ ] Stress testing (500+ concurrent users to identify breaking points)
  - [ ] Endurance testing (100 concurrent users for 30 minutes)
  - [ ] GraphQL-specific performance validation (query complexity, subscription load)
- [ ] **Security Testing:** Complete security validation:
  - [ ] OWASP dependency scanning with zero high/critical vulnerabilities
  - [ ] GraphQL security testing (depth limits, complexity analysis, rate limiting)
  - [ ] Authentication/authorization testing (JWT security, multi-tenancy isolation)
  - [ ] Financial data protection validation (encryption, PCI compliance, audit security)
  - [ ] Infrastructure security verification (TLS, database security, secret management)
- [ ] **DevOps:** Harden production deployment pipeline with automated rollback procedures
- [ ] **Monitoring:** Finalize production monitoring dashboards, alerting rules, and log analysis queries
- [ ] **Database:** Validate production migration procedures with staging data
- [ ] **UAT:** Complete User Acceptance Testing in staging environment

*Success Criteria:*

- [ ] All critical user flows pass E2E automation (100% success rate)
- [ ] Performance targets achieved: <200ms API responses at 95th percentile under baseline load
- [ ] k6 performance tests demonstrate system can handle 200 concurrent users with <5% error rate
- [ ] Zero critical or high security vulnerabilities in security scan results
- [ ] GraphQL API demonstrates proper protection against malicious queries
- [ ] Production deployment completed successfully with monitoring fully operational
- [ ] Database migrations execute without data loss or extended downtime
- [ ] UAT completion with stakeholder sign-off on core functionality

---

## GraphQL Schema Evolution Strategy

### MVP GraphQL Schema

**Core Types (Implementing polymorphic design from Architecture Section 3.2):**

```graphql
# Scalars for financial precision
scalar Date
scalar DateTime
scalar Decimal
scalar UUID

# Core enums designed for expansion
enum AccountType {
  CHECKING
  SAVINGS
  CREDIT_CARD
  INVESTMENT    # Future support
  LOAN         # Future support  
}

enum BudgetType {
  TRADITIONAL  # MVP implementation
  ENVELOPE     # Phase 2
  PERCENTAGE   # Phase 2
  PRIORITY     # Phase 2
  ROLLING      # Phase 3
  VALUE_BASED  # Phase 3
  HYBRID       # Phase 3
}

enum TransactionType {
  DEBIT
  CREDIT
}

# MVP Types with future expansion hooks
type User {
  id: ID!
  email: String!
  name: String!
  currency: String!
  
  # Relationships
  accounts: [Account!]!
  categories: [Category!]!
  budgets(active: Boolean = true): [Budget!]!
  
  # Future household support (PRD Section 3.2.1)
  household: Household  # Returns null in MVP
}

type Account {
  id: ID!
  name: String!
  accountType: AccountType!
  balance: Decimal!
  institution: String
  isActive: Boolean!
  
  # Future sharing support
  isShared: Boolean!
  
  # Transaction relationships
  transactions(
    filter: TransactionFilter
    first: Int = 50
    after: String
  ): TransactionConnection!
  
  # Analytics (basic for MVP)
  monthlyAverage(months: Int = 6): Decimal!
  balanceHistory(days: Int = 30): [BalancePoint!]!
}

type Transaction {
  id: ID!
  amount: Decimal!
  transactionDate: Date!
  description: String!
  transactionType: TransactionType!
  
  # Relationships
  account: Account!
  category: Category
  
  # Future ML support (schema ready, not implemented in MVP)
  predictedCategory: Category
  mlConfidenceScore: Float
  autoCategorized: Boolean!
  
  createdAt: DateTime!
  updatedAt: DateTime!
}

# Polymorphic budget interface for future strategies
interface Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  period: String!
  isActive: Boolean!
  
  # Performance analytics
  currentPerformance: BudgetPerformance!
}

# MVP implementation
type TraditionalBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  period: String!
  isActive: Boolean!
  
  # Traditional budget specific
  totalBudgeted: Decimal!
  categoryBudgets: [CategoryBudget!]!
  
  # Performance
  currentPerformance: BudgetPerformance!
}

type Category {
  id: ID!
  name: String!
  color: String
  icon: String
  
  # Future hierarchy support (ready but not used in MVP)
  parent: Category
  children: [Category!]!
  
  # Transaction relationships
  transactions(first: Int = 50): TransactionConnection!
  transactionCount: Int!
  totalSpent(from: Date, to: Date): Decimal!
}

# Query Root
type Query {
  me: User
  
  # Core entities
  account(id: ID!): Account
  accounts: [Account!]!
  transaction(id: ID!): Transaction
  transactions(
    filter: TransactionFilter
    first: Int = 50
    after: String
  ): TransactionConnection!
  
  # Budgeting
  budget(id: ID!): Budget
  activeBudget: Budget  # Current month's budget
  
  # Categories
  categories: [Category!]!
  systemCategories: [Category!]!  # Predefined categories
}

# Mutation Root
type Mutation {
  # Account management
  createAccount(input: CreateAccountInput!): AccountPayload!
  updateAccount(id: ID!, input: UpdateAccountInput!): AccountPayload!
  deleteAccount(id: ID!): DeletePayload!
  
  # Transaction management  
  createTransaction(input: CreateTransactionInput!): TransactionPayload!
  updateTransaction(id: ID!, input: UpdateTransactionInput!): TransactionPayload!
  categorizeTransaction(id: ID!, categoryId: ID!): TransactionPayload!
  deleteTransaction(id: ID!): DeletePayload!
  
  # Bulk operations
  bulkCategorizeTransactions(
    transactionIds: [ID!]!
    categoryId: ID!
  ): BulkTransactionPayload!
  
  # Budget management
  createTraditionalBudget(input: CreateTraditionalBudgetInput!): BudgetPayload!
  updateBudget(id: ID!, input: UpdateBudgetInput!): BudgetPayload!
  
  # Category management
  createCategory(input: CreateCategoryInput!): CategoryPayload!
  updateCategory(id: ID!, input: UpdateCategoryInput!): CategoryPayload!
}

# Subscription Root (real-time updates)
type Subscription {
  # Transaction updates
  transactionAdded(accountId: ID): Transaction!
  transactionUpdated(accountId: ID): Transaction!
  
  # Budget alerts
  budgetExceeded(userId: ID!): BudgetAlert!
  
  # Balance updates
  balanceUpdated(accountId: ID!): Account!
}
```

### Schema Extension Strategy

**Phase 2 Additions (Household & Advanced Budgets):**

```graphql
# New types for household management (PRD Section 3.2.1)
type Household {
  id: ID!
  name: String!
  members: [HouseholdMember!]!
  sharedAccounts: [Account!]!
  sharedBudgets: [Budget!]!
}

# New budget implementations (PRD Section 3.2.2)
type EnvelopeBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  # ... envelope-specific fields
  envelopes: [Envelope!]!
}

type PercentageBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  # ... percentage-specific fields
  percentageRules: [PercentageRule!]!
}
```

**Phase 3 Additions (ML & Analytics):**

```graphql
# ML-powered features (PRD Section 3.3.1)
type CategoryPrediction {
  category: Category!
  confidence: Float!
  reasoning: String
}

type SpendingInsight {
  id: ID!
  type: InsightType!
  title: String!
  description: String!
  actionable: Boolean!
  priority: InsightPriority!
}

# Extended queries for ML features
extend type Query {
  predictCategory(
    description: String!
    amount: Decimal!
    payee: String
  ): CategoryPrediction!
  
  spendingInsights(
    priority: InsightPriority
    limit: Int = 10
  ): [SpendingInsight!]!
}
```

---

## Risk Assessment & Mitigation

### Technical Risks & Solutions

| **Risk** | **Impact** | **Probability** | **Mitigation Strategy** | **Reference** |
|----------|------------|-----------------|-------------------------|---------------|
| **Complex architecture slows initial development** | High | Medium | Smart interface design, delayed optimization | Architecture patterns from [architecture.md](./architecture.md#hexagonal-architecture-implementation) |
| **Deployment or environment issues delay launch** | High | Medium | Establish CI/CD pipeline and multiple environments from Week 1. Use Infrastructure as Code for repeatability. | DevOps and Deployment Strategy |
| **Performance issues under load** | High | Medium | Comprehensive k6 load testing with GraphQL-specific scenarios, database optimization | Level 4: Performance Testing section |
| **Security vulnerabilities in financial app** | Critical | Medium | Multi-layer security testing including OWASP scanning, GraphQL security, and financial data protection | Level 5: Security Testing section |
| **Database migration failures in production** | High | Low | Automated migration testing in staging, rollback procedures, zero-downtime patterns | Database Migration Strategy |
| **Bugs and regressions slow down development** | Medium | High | Implement a multi-layered testing strategy (Unit, Integration, E2E) integrated into the CI pipeline to catch issues early. | Testing and Quality Assurance Strategy |
| **GraphQL schema breaking changes** | Medium | Low | Interface-based polymorphic types, schema versioning | Schema evolution strategy above |
| **Database performance with events** | Medium | Medium | Proper indexing, materialized views for analytics | Event store optimization from [architecture.md](./architecture.md#database-performance-considerations) |
| **Authentication/authorization complexity** | High | Medium | Simple email/password initially, OAuth interfaces ready | Security requirements from [PRD Section 4.3](./prd.md#security-requirements) |

### Business Risks & Solutions

| **Risk** | **Impact** | **Probability** | **Mitigation Strategy** |
|----------|------------|-----------------|-------------------------|
| **Low user adoption** | High | Medium | Focus on core value (budgeting), rapid iteration based on feedback |
| **Feature scope creep** | Medium | High | Strict MVP boundaries, deferred feature tracking |
| **Competitor feature parity** | Medium | Low | Unique 7-budget strategy differentiator maintained in roadmap |

### Success Metrics & Validation

**MVP Validation Criteria (3-month post-launch):**

- [ ] **User Acquisition:** 100+ active users
- [ ] **Engagement:** 70%+ users create budgets
- [ ] **Retention:** 60%+ monthly retention rate
- [ ] **Feature Usage:** 80%+ users categorize transactions regularly
- [ ] **Performance:** <200ms API response times
- [ ] **Technical Debt:** <20% overhead for Phase 2 features

**Architecture Quality Gates:**

- [ ] All domain operations go through proper aggregates
- [ ] No direct database queries in GraphQL resolvers (repository pattern)
- [ ] Domain events generated for all critical operations
- [ ] GraphQL schema supports polymorphic extension without breaking changes
- [ ] Database schema handles future requirements without major migrations

---

## Post-MVP Roadmap

### Immediate Phase 2 (Months 4-6)

**Priority Features from [PRD](./prd.md):**

1. **Envelope Budget Strategy** - Validate polymorphic budget design
2. **Basic Household Sharing** - 2-user households with simple permission model
3. **OAuth Integration** - Social login for improved user acquisition
4. **Mobile-responsive UI** - Improve user engagement

### Phase 3 (Months 7-9)

**Intelligence Features from [PRD Section 3.3](./prd.md#intelligence-features-phase-3):**

1. **ML Auto-categorization** - Basic ML.NET implementation
2. **Spending Insights** - Simple trend analysis and anomaly detection
3. **Cash Flow Forecasting** - 30-day predictions based on historical data
4. **Advanced Analytics** - Enhanced reporting dashboard

### Phase 4 (Months 10-12)

**Full Platform Features from [PRD](./prd.md):**

1. **All 7 Budget Strategies** - Complete portfolio of budgeting methodologies
2. **Debt Management Suite** - Payoff strategies and tracking
3. **Bank Integration** - Automated transaction imports
4. **Mobile Applications** - Native iOS/Android apps

### Service Extraction Timeline

**Based on [Architecture Section 5.4](./architecture.md#migration-triggers-and-timeline):**

- **Month 6:** Intelligence service extraction (ML workloads)
- **Month 9:** Analytics service extraction (reporting performance)
- **Month 12:** Account service extraction (security isolation)

---

## Conclusion

This MVP development plan provides a strategic approach to building Budget Lens that:

1. **Maintains Architectural Integrity** - Uses sophisticated patterns (.NET 10, GraphQL, Hexagonal Architecture, CQRS) while keeping implementation scope manageable

2. **Enables Rapid Iteration** - 14-week timeline to market with core value proposition validated

3. **Prevents Technical Debt** - Interface-based design and extensible database schema enable smooth evolution to full vision

4. **References Full Vision** - Every architectural decision maps to capabilities defined in [PRD](./prd.md) and [architecture.md](./architecture.md)

5. **Balances Risk & Reward** - Reduces feature complexity by 70% while preserving technical sophistication for future scaling

The result is a production-ready MVP that serves as a solid foundation for the comprehensive personal finance platform envisioned in the complete product requirements, with clear evolution paths for each advanced feature.

**Next Steps:**

1. Review and approve this development plan
2. Set up development environment and team structure  
3. Begin Phase 1 development with Week 1-2 foundation work
4. Establish user feedback loops for rapid post-launch iteration

---

**Document Control:**

- **Created:** September 2025
- **Author:** Development Team  
- **Approved By:** Product & Engineering Leadership
- **Next Review:** End of Phase 1 (Week 6)
- **Related Documents:** [prd.md](./prd.md), [architecture.md](./architecture.md)
