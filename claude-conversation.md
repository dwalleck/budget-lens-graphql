# GraphQL Application Use Cases

**human** (Aug 29, 2025, 09:24 PM)

What kind of applications lend themselves to using graphQL?

**assistant** (Aug 29, 2025, 09:24 PM)

 GraphQL is particularly well-suited for several types of applications:

## Mobile Applications

Mobile apps benefit significantly from GraphQL because they often operate on limited bandwidth and need precise data fetching. GraphQL allows mobile clients to request exactly the data they need in a single request, reducing over-fetching and minimizing data transfer. This is especially valuable when dealing with varying network conditions.

## Complex, Interconnected Data Models

Applications with deeply nested or interconnected data relationships work well with GraphQL. Social networks, content management systems, and e-commerce platforms often have complex relationships between entities (users, posts, comments, products, reviews, etc.). GraphQL's ability to traverse these relationships in a single query makes it ideal for such scenarios.

## Microservices Architectures

GraphQL can serve as an excellent API gateway layer that aggregates data from multiple microservices. Instead of clients making requests to numerous services, GraphQL can provide a unified interface that fetches data from various backend services and presents it cohesively.

## Applications with Diverse Client Needs

When you have multiple clients with different data requirements - such as a web app, mobile app, smartwatch app, and third-party integrations - GraphQL shines. Each client can request precisely what it needs without requiring separate endpoints or API versions.

## Real-time Applications

GraphQL subscriptions make it well-suited for applications requiring real-time updates, such as chat applications, live dashboards, collaborative editing tools, or financial trading platforms.

## Rapid Product Development

Teams that need to iterate quickly benefit from GraphQL's flexibility. Frontend developers can modify their data requirements without waiting for backend changes, and the self-documenting nature of GraphQL schemas helps teams work more independently.

## Public APIs

Organizations providing public APIs can benefit from GraphQL's type system and introspection capabilities, which make APIs self-documenting and easier for external developers to explore and understand.

That said, GraphQL isn't always the best choice. Simple CRUD applications, file upload/download services, or applications with straightforward data models might find REST APIs simpler and more appropriate. The added complexity of GraphQL is most justified when its flexibility and efficiency benefits outweigh the implementation overhead.

**human** (Aug 29, 2025, 09:25 PM)

Could graphql be a reasonable choice for something like a budget/finance tracking application?

**assistant** (Aug 29, 2025, 09:25 PM)

 Yes, GraphQL would be a very reasonable choice for a budget/finance tracking application. Here's why it fits well:

## Natural Fit for Financial Data Relationships

Financial data is inherently interconnected - accounts link to transactions, transactions tie to categories and budgets, budgets relate to goals, and everything connects back to users. GraphQL excels at querying these related entities efficiently. For example, you could fetch a user's accounts, their recent transactions, category spending, and budget progress all in one request.

## Flexible Dashboard and Reporting Needs

Finance apps typically have dashboards that users want to customize - some users care about investment performance, others focus on debt paydown, and others want detailed category breakdowns. GraphQL lets each view request exactly the data it needs without creating numerous specialized endpoints. A monthly spending view might query different fields than a net worth tracker, and GraphQL handles this elegantly.

## Multiple Client Support

A finance app often needs to serve different clients - a full web dashboard with rich visualizations, a mobile app for quick transaction entry, widgets for checking account balances, and maybe even smartwatch complications. Each has vastly different data needs, and GraphQL lets you serve them all from one API.

## Real-time Updates

GraphQL subscriptions work well for features like real-time balance updates when transactions clear, alerts for budget thresholds, or collaborative features in family/business accounts where multiple users might be making changes.

## Performance Benefits

Mobile users checking their finances on the go benefit from GraphQL's efficient data fetching. Instead of making multiple API calls to load account balances, recent transactions, and budget status, everything comes in one optimized request.

## Progressive Feature Development

As you add features like investment tracking, bill reminders, or tax planning, GraphQL's schema evolution capabilities let you extend the API without breaking existing clients.

The main consideration would be whether your app's complexity justifies GraphQL's overhead. A simple expense tracker might not need it, but most modern finance applications with multiple views, reporting features, and cross-platform clients would benefit from GraphQL's flexibility and efficiency.

**human** (Aug 29, 2025, 09:26 PM)

Lets go into more detail. What are some of entities I would need in a budgeting and finance tracking application?

**assistant** (Aug 29, 2025, 09:27 PM)

 Here are the core entities you'd typically need for a comprehensive budgeting and finance tracking application:

## Core Financial Entities

**User** - The account holder with authentication details, preferences, currency settings, and notification preferences.

**Account** - Represents financial accounts (checking, savings, credit cards, investment accounts, loans). Each would have a type, balance, institution name, currency, and interest rate where applicable.

**Transaction** - The heart of any finance app. Contains amount, date, description, type (debit/credit), status (pending/cleared), merchant information, and links to accounts and categories. You might also track location, receipts, or notes.

**Category** - For organizing transactions (groceries, rent, entertainment, etc.). Often hierarchical, so you might have "Transportation" with subcategories like "Gas" and "Public Transit."

**Budget** - Defines spending limits for categories over specific periods. Includes the budgeted amount, period type (monthly/weekly/annual), rollover settings, and links to relevant categories.

## Extended Tracking Entities

**Transfer** - Special type of transaction that moves money between accounts, requiring links to both source and destination accounts.

**ScheduledTransaction** - Recurring transactions like rent, subscriptions, or loan payments. Includes frequency, next occurrence date, and end date.

**Goal** - Savings goals with target amounts, deadlines, and linked accounts. Could be for emergencies, vacations, or major purchases.

**Bill** - Upcoming or recurring bills with due dates, minimum payments, and autopay settings.

**Payee/Merchant** - Stores information about where money is spent, enabling better transaction categorization and spending analysis.

## Investment & Debt Entities

**Investment** - For tracking stocks, bonds, mutual funds with purchase price, current value, and quantity.

**Portfolio** - Groups investments and tracks overall performance.

**Loan** - Tracks debts with principal, interest rate, payment schedule, and remaining balance.

**NetWorthSnapshot** - Periodic calculations of total assets minus liabilities for tracking long-term progress.

## Organizational Entities

**Tag** - Flexible labeling system for transactions beyond categories (e.g., "tax-deductible," "reimbursable," "vacation-2024").

**Attachment** - Receipts, statements, or documents linked to transactions or accounts.

**Budget Period** - Represents a specific time instance of a budget (e.g., "January 2024" for a monthly budget).

**Alert/Notification** - Configured warnings for low balances, exceeded budgets, bill due dates, or goal milestones.

**Report** - Saved report configurations or generated financial summaries.

## Relationships to Consider

The power of GraphQL really shows when you consider how these entities interconnect:

- Transactions belong to Accounts and Categories
- Budgets monitor spending across one or more Categories
- Goals can be linked to specific Accounts
- Scheduled Transactions create regular Transactions
- Users can share certain entities (for family/joint accounts)
- Categories can have parent-child relationships
- Transfers link two Accounts
- Budget Periods track actual vs. planned spending

This interconnected structure is where GraphQL shines - you could query a user's accounts with their recent transactions, each transaction's category, and how those categories are performing against budgets, all in a single request. The flexibility to traverse these relationships efficiently makes GraphQL particularly well-suited for financial applications.

**human** (Aug 29, 2025, 09:28 PM)

Can you show me what those relationships would look like using mermaid charts?

**assistant** (Aug 29, 2025, 09:28 PM)

 I'll create a comprehensive entity relationship diagram showing how these financial entities connect to each other.

#### create Finance Application Entity Relationship Diagram

```
erDiagram
    User ||--o{ Account : owns
    User ||--o{ Category : creates
    User ||--o{ Budget : manages
    User ||--o{ Goal : sets
    User ||--o{ Tag : defines
    User ||--o{ Alert : configures
    User ||--o{ Report : generates
    User ||--o{ ScheduledTransaction : schedules
    
    Account ||--o{ Transaction : contains
    Account ||--o{ Transfer : source
    Account ||--o{ Transfer : destination
    Account ||--o{ Goal : "funds towards"
    Account ||--o{ NetWorthSnapshot : "included in"
    Account ||--o{ Investment : holds
    Account ||--o{ Loan : represents
    
    Transaction }o--|| Category : "belongs to"
    Transaction }o--|| Payee : "paid to"
    Transaction }o--o{ Tag : "tagged with"
    Transaction ||--o{ Attachment : "has receipts"
    Transaction }o--|| ScheduledTransaction : "created by"
    
    Category ||--o{ Category : "parent of"
    Category ||--o{ Budget : "monitored by"
    
    Budget ||--o{ BudgetPeriod : "has periods"
    BudgetPeriod }o--o{ Transaction : tracks
    
    Transfer }o--|| Transaction : "is type of"
    
    ScheduledTransaction }o--|| Payee : "pays to"
    ScheduledTransaction }o--|| Category : "categorized as"
    
    Bill ||--|| ScheduledTransaction : "is type of"
    Bill }o--|| Payee : "owed to"
    
    Investment }o--|| Portfolio : "grouped in"
    Portfolio }o--|| User : "owned by"
    
    Loan }o--|| Payee : "owed to"
    
    NetWorthSnapshot }o--|| User : "calculated for"
    
    Alert }o--|| Account : "monitors"
    Alert }o--|| Budget : "watches"
    Alert }o--|| Bill : "reminds about"
    Alert }o--|| Goal : "tracks progress"
    
    Report }o--o{ Account : analyzes
    Report }o--o{ Category : summarizes
    Report }o--o{ Budget : compares

    User {
        string id PK
        string email
        string name
        string currency
        json preferences
        datetime created_at
    }
    
    Account {
        string id PK
        string user_id FK
        string name
        string type
        decimal balance
        string institution
        string currency
        decimal interest_rate
        boolean is_active
    }
    
    Transaction {
        string id PK
        string account_id FK
        string category_id FK
        string payee_id FK
        decimal amount
        date transaction_date
        string description
        string type
        string status
        json metadata
    }
    
    Category {
        string id PK
        string user_id FK
        string parent_id FK
        string name
        string icon
        string color
        boolean is_income
    }
    
    Budget {
        string id PK
        string user_id FK
        string name
        decimal amount
        string period_type
        boolean rollover_enabled
    }
    
    Goal {
        string id PK
        string user_id FK
        string account_id FK
        string name
        decimal target_amount
        decimal current_amount
        date target_date
    }
    
    Transfer {
        string id PK
        string from_account_id FK
        string to_account_id FK
        string transaction_id FK
        decimal amount
        date transfer_date
    }
```

I've created an entity relationship diagram showing the key relationships in a finance tracking application. Here are some important aspects of this design:

## Key Relationships Explained

**User-Centric Design**: The User entity is at the center, owning most other entities. This supports multi-tenancy where each user has their own isolated financial data.

**Transaction Relationships**: Transactions are the core data points, connecting to:

- Accounts (where the money flows)
- Categories (how it's classified)
- Payees (who received/sent the money)
- Tags (flexible labeling)
- Attachments (receipts and documentation)

**Account Versatility**: Accounts can represent various financial instruments (checking, credit cards, investments, loans) and serve as the containers for transactions and the funding source for goals.

**Hierarchical Categories**: Categories can have parent-child relationships, allowing for detailed organization like "Transportation > Gas" or "Food > Restaurants > Fast Food."

**Budget Periods**: The BudgetPeriod entity acts as an intersection between Budgets and Transactions, tracking actual spending against planned amounts for specific time periods.

**Transfer Handling**: Transfers are modeled as a special entity linking two accounts with an associated transaction, maintaining proper double-entry bookkeeping.

**Scheduled Transactions**: These create regular transactions automatically and can represent bills, subscriptions, or recurring income.

**Monitoring & Alerts**: The Alert entity can watch various aspects of the financial system - account balances, budget overruns, bill due dates, or goal progress.

This structure provides the flexibility needed for complex queries in GraphQL. For example, you could easily query a user's accounts with their recent transactions, each transaction's category and its parent category, current budget period performance, and progress toward associated goals - all in a single GraphQL query.

**human** (Aug 29, 2025, 09:30 PM)

This is becoming very interesting. What kinds of databases lend themselves to the types of queries graphql can make?

**assistant** (Aug 29, 2025, 09:31 PM)

 This is a great question because GraphQL's ability to traverse relationships and fetch nested data efficiently puts interesting demands on your database layer. Let me break down which databases work well and why:

## Graph Databases

**Neo4j, Amazon Neptune, ArangoDB**

These are naturally aligned with GraphQL's graph-like query patterns. When your GraphQL query asks for deeply nested relationships (like "all transactions in categories within a budget for a specific user's accounts"), graph databases traverse these relationships natively. They excel at queries like "show me all expenses that are related to my vacation goal through shared tags and categories." The downside is they're often overkill for simpler queries and require specialized knowledge.

## PostgreSQL (Relational)

**The Pragmatic Choice**

PostgreSQL remains one of the most popular choices for GraphQL backends because:

- Its JSON/JSONB support handles flexible, nested data structures well
- Common table expressions (CTEs) and window functions help with complex aggregations
- Strong support for indexes on foreign keys makes relationship traversal performant
- Tools like Hasura or PostGraphile can automatically generate GraphQL schemas from PostgreSQL schemas
- Row-level security policies align well with GraphQL's field-level authorization needs

For a finance app, PostgreSQL's ACID compliance and strong consistency guarantees are particularly valuable when dealing with monetary transactions.

## Document Databases

**MongoDB, DynamoDB, FaunaDB**

Document stores can work well with GraphQL, especially if you denormalize your data to match common query patterns. For a finance app, you might store transactions with embedded category information to avoid joins. MongoDB's aggregation pipeline can handle complex GraphQL queries efficiently. FaunaDB is particularly interesting as it combines document storage with ACID transactions and built-in GraphQL support.

The challenge comes with relationships - while you can use references between documents, joining across collections isn't as natural as in relational databases.

## Multi-Model Databases

**ArangoDB, CosmosDB, FaunaDB**

These databases support multiple data models (document, graph, key-value) in a single system. They're excellent for GraphQL because you can use the graph model for relationship-heavy queries while using document storage for simpler entities. For a finance app, you might use graph traversal for budget-category-transaction relationships while storing user preferences as documents.

## The N+1 Query Problem

Regardless of database choice, GraphQL's nested nature can lead to the N+1 query problem. If you fetch 10 accounts and then transactions for each, naive implementations make 11 database queries. Solutions include:

- **DataLoader pattern**: Batches and caches database requests
- **Join Monster**: Automatically generates efficient SQL joins from GraphQL queries
- **Eager loading**: Anticipating and preloading related data
- **Database views**: Pre-computed joins for common query patterns

## Caching Layers

**Redis, Memcached**

Almost essential for production GraphQL APIs. You can cache:

- Resolved GraphQL queries
- Database query results  
- Computed aggregations (monthly spending, category totals)

Redis is particularly powerful because it can store complex data structures, making it excellent for caching partial graph results.

## For a Finance Application Specifically

I'd recommend starting with **PostgreSQL** for these reasons:

1. **Financial Integrity**: ACID transactions are crucial for monetary data
2. **Complex Queries**: Window functions handle running balances, month-over-month comparisons
3. **Maturity**: Battle-tested with extensive tooling
4. **Flexibility**: JSONB columns can store flexible metadata while maintaining relational structure
5. **Aggregations**: Native support for the kinds of SUM, AVG, and GROUP BY operations common in financial reporting

You could enhance this with:

- **Redis** for caching frequently accessed data like account balances
- **TimescaleDB** (PostgreSQL extension) if you need time-series analysis for investment tracking
- **Elasticsearch** for powerful transaction search capabilities

The key is that GraphQL is database-agnostic - it's a query language for your API, not your database. Your GraphQL resolvers translate GraphQL queries into whatever database queries make sense for your chosen storage solution. This flexibility means you can start with PostgreSQL and add specialized databases for specific features as your application grows.

**human** (Aug 29, 2025, 09:32 PM)

So if I used GraphQL with Postgres, what would the relationship between entities look like?

**assistant** (Aug 29, 2025, 09:37 PM)

 When using GraphQL with PostgreSQL, you're essentially mapping between three layers: your GraphQL schema (what clients see), your SQL schema (how data is stored), and the resolver layer that connects them. Let me show you how this works for a finance app.

#### create GraphQL with PostgreSQL - Finance App Implementation

```sql
-- PostgreSQL Schema for Finance Application
-- This shows how the relational database structure maps to GraphQL entities

-- Users table (root entity)
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    currency VARCHAR(3) DEFAULT 'USD',
    preferences JSONB DEFAULT '{}',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Accounts table with foreign key to users
CREATE TABLE accounts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    account_type VARCHAR(50) NOT NULL, -- checking, savings, credit_card, investment, loan
    balance DECIMAL(15,2) NOT NULL DEFAULT 0,
    institution VARCHAR(255),
    currency VARCHAR(3) DEFAULT 'USD',
    interest_rate DECIMAL(5,2),
    credit_limit DECIMAL(15,2),
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_accounts_user_id (user_id),
    INDEX idx_accounts_type (account_type)
);

-- Categories with self-referential relationship for hierarchy
CREATE TABLE categories (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    parent_id UUID REFERENCES categories(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    icon VARCHAR(50),
    color VARCHAR(7), -- hex color
    is_income BOOLEAN DEFAULT false,
    is_system BOOLEAN DEFAULT false, -- for default categories
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_categories_user_id (user_id),
    INDEX idx_categories_parent_id (parent_id),
    UNIQUE(user_id, name, parent_id)
);

-- Payees/Merchants
CREATE TABLE payees (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    default_category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    notes TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_payees_user_id (user_id),
    UNIQUE(user_id, name)
);

-- Transactions - the core entity
CREATE TABLE transactions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
    category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    payee_id UUID REFERENCES payees(id) ON DELETE SET NULL,
    amount DECIMAL(15,2) NOT NULL,
    transaction_date DATE NOT NULL,
    description TEXT,
    transaction_type VARCHAR(20) NOT NULL, -- debit, credit
    status VARCHAR(20) DEFAULT 'cleared', -- pending, cleared, reconciled
    notes TEXT,
    metadata JSONB DEFAULT '{}', -- flexible field for additional data
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_transactions_account_id (account_id),
    INDEX idx_transactions_category_id (category_id),
    INDEX idx_transactions_date (transaction_date DESC),
    INDEX idx_transactions_amount (amount),
    INDEX idx_transactions_account_date (account_id, transaction_date DESC)
);

-- Transfers link two accounts
CREATE TABLE transfers (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    from_account_id UUID NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
    to_account_id UUID NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
    from_transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    to_transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    amount DECIMAL(15,2) NOT NULL,
    transfer_date DATE NOT NULL,
    notes TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_transfers_from_account (from_account_id),
    INDEX idx_transfers_to_account (to_account_id)
);

-- Budgets
CREATE TABLE budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    period_type VARCHAR(20) NOT NULL, -- monthly, weekly, yearly
    rollover_enabled BOOLEAN DEFAULT false,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_budgets_user_id (user_id)
);

-- Budget-Category junction table (many-to-many)
CREATE TABLE budget_categories (
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    category_id UUID NOT NULL REFERENCES categories(id) ON DELETE CASCADE,
    amount DECIMAL(15,2) NOT NULL,
    
    PRIMARY KEY (budget_id, category_id)
);

-- Budget periods track actual vs planned
CREATE TABLE budget_periods (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    total_budgeted DECIMAL(15,2) NOT NULL,
    total_spent DECIMAL(15,2) DEFAULT 0,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_budget_periods_budget_id (budget_id),
    INDEX idx_budget_periods_dates (start_date, end_date),
    UNIQUE(budget_id, start_date, end_date)
);

-- Scheduled/Recurring transactions
CREATE TABLE scheduled_transactions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    account_id UUID NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
    category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    payee_id UUID REFERENCES payees(id) ON DELETE SET NULL,
    amount DECIMAL(15,2) NOT NULL,
    description TEXT,
    frequency VARCHAR(20) NOT NULL, -- daily, weekly, monthly, yearly
    day_of_month INTEGER,
    next_date DATE NOT NULL,
    end_date DATE,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_scheduled_user_id (user_id),
    INDEX idx_scheduled_next_date (next_date)
);

-- Tags for flexible categorization
CREATE TABLE tags (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(50) NOT NULL,
    color VARCHAR(7),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_tags_user_id (user_id),
    UNIQUE(user_id, name)
);

-- Transaction-Tag junction table (many-to-many)
CREATE TABLE transaction_tags (
    transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    tag_id UUID NOT NULL REFERENCES tags(id) ON DELETE CASCADE,
    
    PRIMARY KEY (transaction_id, tag_id)
);

-- Goals
CREATE TABLE goals (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    account_id UUID REFERENCES accounts(id) ON DELETE SET NULL,
    name VARCHAR(255) NOT NULL,
    target_amount DECIMAL(15,2) NOT NULL,
    current_amount DECIMAL(15,2) DEFAULT 0,
    target_date DATE,
    is_achieved BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_goals_user_id (user_id)
);

-- Attachments for receipts
CREATE TABLE attachments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    file_name VARCHAR(255) NOT NULL,
    file_url TEXT NOT NULL,
    file_size INTEGER,
    mime_type VARCHAR(100),
    uploaded_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_attachments_transaction_id (transaction_id)
);

-- ============================================
-- VIEWS for common GraphQL queries
-- ============================================

-- Account balance with recent transactions
CREATE VIEW account_summary AS
SELECT 
    a.*,
    COUNT(t.id) as transaction_count,
    MAX(t.transaction_date) as last_transaction_date
FROM accounts a
LEFT JOIN transactions t ON a.id = t.account_id
GROUP BY a.id;

-- Category spending by period
CREATE VIEW category_spending AS
SELECT 
    c.id as category_id,
    c.name as category_name,
    DATE_TRUNC('month', t.transaction_date) as period,
    SUM(CASE WHEN t.transaction_type = 'debit' THEN t.amount ELSE 0 END) as spent,
    SUM(CASE WHEN t.transaction_type = 'credit' THEN t.amount ELSE 0 END) as earned,
    COUNT(t.id) as transaction_count
FROM categories c
LEFT JOIN transactions t ON c.id = t.category_id
GROUP BY c.id, c.name, DATE_TRUNC('month', t.transaction_date);

-- Net worth calculation
CREATE VIEW net_worth AS
SELECT 
    u.id as user_id,
    SUM(CASE 
        WHEN a.account_type IN ('checking', 'savings', 'investment') THEN a.balance 
        ELSE 0 
    END) as assets,
    SUM(CASE 
        WHEN a.account_type IN ('credit_card', 'loan') THEN a.balance 
        ELSE 0 
    END) as liabilities,
    SUM(CASE 
        WHEN a.account_type IN ('checking', 'savings', 'investment') THEN a.balance 
        WHEN a.account_type IN ('credit_card', 'loan') THEN -a.balance 
        ELSE 0 
    END) as net_worth
FROM users u
LEFT JOIN accounts a ON u.id = a.user_id
GROUP BY u.id;

-- ============================================
-- FUNCTIONS for complex GraphQL resolvers
-- ============================================

-- Get transactions with running balance
CREATE OR REPLACE FUNCTION get_transactions_with_balance(
    p_account_id UUID,
    p_limit INTEGER DEFAULT 50
)
RETURNS TABLE (
    id UUID,
    amount DECIMAL,
    transaction_date DATE,
    description TEXT,
    running_balance DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        t.id,
        t.amount,
        t.transaction_date,
        t.description,
        SUM(CASE 
            WHEN t2.transaction_type = 'credit' THEN t2.amount 
            ELSE -t2.amount 
        END) OVER (ORDER BY t2.transaction_date DESC, t2.created_at DESC) as running_balance
    FROM transactions t
    JOIN transactions t2 ON t2.account_id = p_account_id 
        AND (t2.transaction_date < t.transaction_date 
            OR (t2.transaction_date = t.transaction_date AND t2.created_at <= t.created_at))
    WHERE t.account_id = p_account_id
    ORDER BY t.transaction_date DESC, t.created_at DESC
    LIMIT p_limit;
END;
$$ LANGUAGE plpgsql;

-- Calculate budget performance
CREATE OR REPLACE FUNCTION calculate_budget_performance(
    p_budget_id UUID,
    p_start_date DATE,
    p_end_date DATE
)
RETURNS TABLE (
    category_id UUID,
    category_name VARCHAR,
    budgeted DECIMAL,
    spent DECIMAL,
    remaining DECIMAL,
    percentage_used DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        c.id,
        c.name,
        bc.amount as budgeted,
        COALESCE(SUM(t.amount), 0) as spent,
        bc.amount - COALESCE(SUM(t.amount), 0) as remaining,
        CASE 
            WHEN bc.amount > 0 THEN (COALESCE(SUM(t.amount), 0) / bc.amount * 100)
            ELSE 0
        END as percentage_used
    FROM budget_categories bc
    JOIN categories c ON bc.category_id = c.id
    LEFT JOIN transactions t ON c.id = t.category_id 
        AND t.transaction_date BETWEEN p_start_date AND p_end_date
        AND t.transaction_type = 'debit'
    WHERE bc.budget_id = p_budget_id
    GROUP BY c.id, c.name, bc.amount;
END;
$$ LANGUAGE plpgsql;

-- ============================================
-- INDEXES for GraphQL query optimization
-- ============================================

-- Composite indexes for common GraphQL queries
CREATE INDEX idx_transactions_user_date ON transactions 
    USING btree (account_id, transaction_date DESC, created_at DESC);

CREATE INDEX idx_transactions_category_date ON transactions 
    USING btree (category_id, transaction_date DESC) 
    WHERE category_id IS NOT NULL;

CREATE INDEX idx_transactions_search ON transactions 
    USING gin (to_tsvector('english', description || ' ' || COALESCE(notes, '')));

-- Partial indexes for filtered queries
CREATE INDEX idx_active_accounts ON accounts (user_id) 
    WHERE is_active = true;

CREATE INDEX idx_pending_transactions ON transactions (account_id, transaction_date) 
    WHERE status = 'pending';

-- ============================================
-- ROW LEVEL SECURITY (RLS) for multi-tenancy
-- ============================================

ALTER TABLE users ENABLE ROW LEVEL SECURITY;
ALTER TABLE accounts ENABLE ROW LEVEL SECURITY;
ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;
ALTER TABLE categories ENABLE ROW LEVEL SECURITY;
ALTER TABLE budgets ENABLE ROW LEVEL SECURITY;

-- Users can only see their own data
CREATE POLICY users_isolation ON users
    FOR ALL USING (id = current_setting('app.current_user_id')::UUID);

CREATE POLICY accounts_isolation ON accounts
    FOR ALL USING (user_id = current_setting('app.current_user_id')::UUID);

CREATE POLICY transactions_isolation ON transactions
    FOR ALL USING (
        account_id IN (
            SELECT id FROM accounts 
            WHERE user_id = current_setting('app.current_user_id')::UUID
        )
    );

-- Add similar policies for other tables...
```

Now let me show you how this PostgreSQL schema maps to GraphQL:

#### create GraphQL Schema with PostgreSQL Resolver Examples

```graphql
# GraphQL Schema for Finance Application
# This maps to the PostgreSQL tables shown previously

type User {
  id: ID!
  email: String!
  name: String!
  currency: String!
  preferences: JSON
  
  # Relationships resolved via SQL JOINs or DataLoader
  accounts: [Account!]! 
  categories: [Category!]!
  budgets: [Budget!]!
  goals: [Goal!]!
  transactions(
    limit: Int = 50
    offset: Int = 0
    startDate: Date
    endDate: Date
    categoryId: ID
  ): [Transaction!]!
  
  # Computed fields using SQL aggregations
  netWorth: NetWorth!
  monthlySpending(month: String!): MonthlySpending!
}

type Account {
  id: ID!
  name: String!
  accountType: AccountType!
  balance: Float!
  institution: String
  currency: String!
  interestRate: Float
  creditLimit: Float
  isActive: Boolean!
  
  # Resolved via JOIN: SELECT * FROM transactions WHERE account_id = $1
  transactions(
    limit: Int = 50
    offset: Int = 0
    startDate: Date
    endDate: Date
  ): [Transaction!]!
  
  # Resolved via custom SQL function
  transactionsWithBalance(limit: Int = 50): [TransactionWithBalance!]!
  
  # Computed via SQL aggregation
  monthlyAverage: Float!
  pendingTransactions: [Transaction!]!
}

type Transaction {
  id: ID!
  amount: Float!
  transactionDate: Date!
  description: String
  transactionType: TransactionType!
  status: TransactionStatus!
  notes: String
  metadata: JSON
  
  # Resolved via JOIN: SELECT * FROM accounts WHERE id = $1
  account: Account!
  
  # Resolved via JOIN: SELECT * FROM categories WHERE id = $1
  category: Category
  
  # Resolved via JOIN: SELECT * FROM payees WHERE id = $1
  payee: Payee
  
  # Many-to-many via junction table
  # SELECT t.* FROM tags t 
  # JOIN transaction_tags tt ON t.id = tt.tag_id 
  # WHERE tt.transaction_id = $1
  tags: [Tag!]!
  
  # One-to-many relationship
  attachments: [Attachment!]!
}

type Category {
  id: ID!
  name: String!
  icon: String
  color: String
  isIncome: Boolean!
  
  # Self-referential relationship
  parent: Category
  children: [Category!]!
  
  # Resolved via JOIN with aggregation
  currentMonthSpending: Float!
  averageMonthlySpending: Float!
  
  # Complex query with date filtering
  transactions(
    startDate: Date
    endDate: Date
    limit: Int = 50
  ): [Transaction!]!
  
  # Via junction table
  budgets: [Budget!]!
}

type Budget {
  id: ID!
  name: String!
  periodType: PeriodType!
  rolloverEnabled: Boolean!
  isActive: Boolean!
  
  # Many-to-many via budget_categories
  categories: [BudgetCategory!]!
  
  # One-to-many relationship
  periods: [BudgetPeriod!]!
  
  # Current period calculated via SQL
  currentPeriod: BudgetPeriod
  
  # Uses stored function: calculate_budget_performance
  performance(startDate: Date!, endDate: Date!): BudgetPerformance!
}

type BudgetCategory {
  category: Category!
  amount: Float!
  
  # Computed field using window functions
  percentageUsed: Float!
  remaining: Float!
  spent: Float!
}

type BudgetPeriod {
  id: ID!
  startDate: Date!
  endDate: Date!
  totalBudgeted: Float!
  totalSpent: Float!
  
  # Computed field
  percentageUsed: Float!
  daysRemaining: Int!
  
  # Complex aggregation query
  categoryBreakdown: [CategorySpending!]!
}

type Transfer {
  id: ID!
  amount: Float!
  transferDate: Date!
  notes: String
  
  # JOINs to accounts table twice
  fromAccount: Account!
  toAccount: Account!
  
  # JOINs to transactions table twice  
  fromTransaction: Transaction!
  toTransaction: Transaction!
}

type Goal {
  id: ID!
  name: String!
  targetAmount: Float!
  currentAmount: Float!
  targetDate: Date
  isAchieved: Boolean!
  
  # Calculated field
  percentageComplete: Float!
  estimatedCompletionDate: Date
  
  # Optional relationship
  account: Account
  
  # Computed from transaction history
  monthlyProgress: [MonthlyProgress!]!
}

# Computed Types (often from VIEWs or functions)
type NetWorth {
  assets: Float!
  liabilities: Float!
  total: Float!
  
  # Historical data via time-series query
  history(months: Int = 12): [NetWorthSnapshot!]!
}

type TransactionWithBalance {
  transaction: Transaction!
  runningBalance: Float!
}

type MonthlySpending {
  total: Float!
  byCategory: [CategorySpending!]!
  comparison: SpendingComparison!
}

# Query Root
type Query {
  # Simple SELECT with WHERE clause
  user(id: ID!): User
  
  # SELECT with JOIN and WHERE
  account(id: ID!): Account
  
  # Complex search query using PostgreSQL full-text search
  searchTransactions(
    query: String!
    accountIds: [ID!]
    startDate: Date
    endDate: Date
    limit: Int = 50
  ): [Transaction!]!
  
  # Aggregation query across multiple tables
  spendingAnalysis(
    userId: ID!
    startDate: Date!
    endDate: Date!
    groupBy: GroupByPeriod!
  ): SpendingAnalysis!
  
  # Uses PostgreSQL window functions
  topSpendingCategories(
    userId: ID!
    limit: Int = 10
    period: TimePeriod
  ): [CategoryRanking!]!
}

# Mutation Root
type Mutation {
  # INSERT with RETURNING clause
  createAccount(input: CreateAccountInput!): Account!
  
  # Transaction with multiple INSERT statements
  createTransfer(input: CreateTransferInput!): Transfer!
  
  # UPDATE with optimistic locking check
  updateTransaction(
    id: ID!
    input: UpdateTransactionInput!
    expectedVersion: Int
  ): Transaction!
  
  # Complex transaction updating multiple tables
  reconcileAccount(
    accountId: ID!
    endingBalance: Float!
    transactionIds: [ID!]!
  ): ReconciliationResult!
  
  # Bulk operation with CTEs
  categorizeTransactions(
    transactionIds: [ID!]!
    categoryId: ID!
  ): [Transaction!]!
}

# Subscription Root (using PostgreSQL LISTEN/NOTIFY)
type Subscription {
  # Triggered by PostgreSQL trigger on INSERT
  transactionAdded(accountId: ID!): Transaction!
  
  # Triggered when balance changes significantly
  accountBalanceChanged(threshold: Float!): AccountBalanceUpdate!
  
  # Real-time budget alerts
  budgetAlert(budgetId: ID!): BudgetAlert!
}

# Enums map to PostgreSQL CHECK constraints or ENUM types
enum AccountType {
  CHECKING
  SAVINGS
  CREDIT_CARD
  INVESTMENT
  LOAN
}

enum TransactionType {
  DEBIT
  CREDIT
}

enum TransactionStatus {
  PENDING
  CLEARED
  RECONCILED
}

enum PeriodType {
  DAILY
  WEEKLY
  MONTHLY
  YEARLY
}

# Input Types for mutations
input CreateAccountInput {
  name: String!
  accountType: AccountType!
  balance: Float!
  institution: String
  currency: String
}

input CreateTransferInput {
  fromAccountId: ID!
  toAccountId: ID!
  amount: Float!
  date: Date!
  description: String
}

input UpdateTransactionInput {
  amount: Float
  description: String
  categoryId: ID
  payeeId: ID
  notes: String
  tags: [ID!]
}
```

Now let me show you how the resolver layer connects GraphQL to PostgreSQL:

#### create GraphQL Resolvers with PostgreSQL Queries

```javascript
// GraphQL Resolvers showing how GraphQL queries map to PostgreSQL
// Using Node.js with a PostgreSQL client and DataLoader for optimization

import DataLoader from 'dataloader';
import { Pool } from 'pg';

const pool = new Pool({
  connectionString: process.env.DATABASE_URL
});

// ============================================
// DATA LOADERS - Solve N+1 Query Problem
// ============================================

// Batch load accounts to avoid N+1 queries
const accountLoader = new DataLoader(async (accountIds) => {
  const query = `
    SELECT * FROM accounts 
    WHERE id = ANY($1::uuid[])
  `;
  const result = await pool.query(query, [accountIds]);
  
  // Map results back in the same order as requested
  const accountMap = {};
  result.rows.forEach(row => {
    accountMap[row.id] = row;
  });
  
  return accountIds.map(id => accountMap[id]);
});

// Batch load categories
const categoryLoader = new DataLoader(async (categoryIds) => {
  const query = `
    SELECT * FROM categories 
    WHERE id = ANY($1::uuid[])
  `;
  const result = await pool.query(query, [categoryIds]);
  
  const categoryMap = {};
  result.rows.forEach(row => {
    categoryMap[row.id] = row;
  });
  
  return categoryIds.map(id => categoryMap[id]);
});

// Batch load transactions for multiple accounts
const accountTransactionsLoader = new DataLoader(async (accountIds) => {
  const query = `
    SELECT * FROM transactions 
    WHERE account_id = ANY($1::uuid[])
    ORDER BY transaction_date DESC, created_at DESC
  `;
  const result = await pool.query(query, [accountIds]);
  
  // Group transactions by account_id
  const transactionsByAccount = {};
  result.rows.forEach(row => {
    if (!transactionsByAccount[row.account_id]) {
      transactionsByAccount[row.account_id] = [];
    }
    transactionsByAccount[row.account_id].push(row);
  });
  
  return accountIds.map(id => transactionsByAccount[id] || []);
});

// ============================================
// RESOLVERS
// ============================================

const resolvers = {
  Query: {
    // Simple query with direct SQL
    user: async (parent, { id }, context) => {
      const query = `
        SELECT * FROM users 
        WHERE id = $1
      `;
      const result = await pool.query(query, [id]);
      return result.rows[0];
    },

    // Complex search using PostgreSQL full-text search
    searchTransactions: async (parent, args, context) => {
      const { query, accountIds, startDate, endDate, limit } = args;
      
      let sql = `
        SELECT t.*, 
               ts_rank(to_tsvector('english', t.description || ' ' || COALESCE(t.notes, '')), 
                      plainto_tsquery('english', $1)) as rank
        FROM transactions t
        WHERE to_tsvector('english', t.description || ' ' || COALESCE(t.notes, '')) 
              @@ plainto_tsquery('english', $1)
      `;
      
      const params = [query];
      let paramCount = 1;
      
      if (accountIds && accountIds.length > 0) {
        paramCount++;
        sql += ` AND t.account_id = ANY($${paramCount}::uuid[])`;
        params.push(accountIds);
      }
      
      if (startDate) {
        paramCount++;
        sql += ` AND t.transaction_date >= $${paramCount}`;
        params.push(startDate);
      }
      
      if (endDate) {
        paramCount++;
        sql += ` AND t.transaction_date <= $${paramCount}`;
        params.push(endDate);
      }
      
      sql += ` ORDER BY rank DESC, t.transaction_date DESC LIMIT $${paramCount + 1}`;
      params.push(limit);
      
      const result = await pool.query(sql, params);
      return result.rows;
    },

    // Aggregation query using window functions and CTEs
    spendingAnalysis: async (parent, { userId, startDate, endDate, groupBy }, context) => {
      const query = `
        WITH user_transactions AS (
          SELECT t.*, c.name as category_name, c.parent_id
          FROM transactions t
          JOIN accounts a ON t.account_id = a.id
          LEFT JOIN categories c ON t.category_id = c.id
          WHERE a.user_id = $1 
            AND t.transaction_date BETWEEN $2 AND $3
            AND t.transaction_type = 'debit'
        ),
        grouped_spending AS (
          SELECT 
            DATE_TRUNC($4, transaction_date) as period,
            category_id,
            category_name,
            SUM(amount) as total_spent,
            COUNT(*) as transaction_count,
            AVG(amount) as avg_transaction
          FROM user_transactions
          GROUP BY DATE_TRUNC($4, transaction_date), category_id, category_name
        ),
        period_totals AS (
          SELECT 
            period,
            SUM(total_spent) as period_total,
            SUM(SUM(total_spent)) OVER (ORDER BY period) as running_total
          FROM grouped_spending
          GROUP BY period
        )
        SELECT 
          json_build_object(
            'periods', json_agg(DISTINCT jsonb_build_object(
              'period', pt.period,
              'total', pt.period_total,
              'runningTotal', pt.running_total
            ) ORDER BY pt.period),
            'byCategory', json_agg(DISTINCT jsonb_build_object(
              'categoryId', gs.category_id,
              'categoryName', gs.category_name,
              'total', gs.total_spent,
              'average', gs.avg_transaction,
              'count', gs.transaction_count
            )),
            'totalSpent', SUM(gs.total_spent),
            'transactionCount', SUM(gs.transaction_count)
          ) as analysis
        FROM grouped_spending gs
        JOIN period_totals pt ON gs.period = pt.period
      `;
      
      const result = await pool.query(query, [userId, startDate, endDate, groupBy]);
      return result.rows[0].analysis;
    }
  },

  User: {
    // Relationship resolver - gets accounts for a user
    accounts: async (user, args, context) => {
      const query = `
        SELECT * FROM accounts 
        WHERE user_id = $1 AND is_active = true
        ORDER BY created_at DESC
      `;
      const result = await pool.query(query, [user.id]);
      return result.rows;
    },

    // Complex computed field using VIEW
    netWorth: async (user, args, context) => {
      const query = `
        SELECT * FROM net_worth 
        WHERE user_id = $1
      `;
      const result = await pool.query(query, [user.id]);
      return result.rows[0];
    },

    // Paginated relationship with filtering
    transactions: async (user, { limit, offset, startDate, endDate, categoryId }, context) => {
      let query = `
        SELECT t.* 
        FROM transactions t
        JOIN accounts a ON t.account_id = a.id
        WHERE a.user_id = $1
      `;
      
      const params = [user.id];
      let paramCount = 1;
      
      if (startDate) {
        paramCount++;
        query += ` AND t.transaction_date >= $${paramCount}`;
        params.push(startDate);
      }
      
      if (endDate) {
        paramCount++;
        query += ` AND t.transaction_date <= $${paramCount}`;
        params.push(endDate);
      }
      
      if (categoryId) {
        paramCount++;
        query += ` AND t.category_id = $${paramCount}`;
        params.push(categoryId);
      }
      
      query += ` ORDER BY t.transaction_date DESC, t.created_at DESC`;
      query += ` LIMIT $${paramCount + 1} OFFSET $${paramCount + 2}`;
      params.push(limit, offset);
      
      const result = await pool.query(query, params);
      return result.rows;
    }
  },

  Account: {
    // Uses DataLoader to batch-load transactions
    transactions: async (account, { limit = 50 }, context) => {
      // For simple cases, use the DataLoader
      if (limit === 50 && !args.startDate) {
        return accountTransactionsLoader.load(account.id);
      }
      
      // For filtered queries, use direct SQL
      const query = `
        SELECT * FROM transactions 
        WHERE account_id = $1
        ORDER BY transaction_date DESC
        LIMIT $2
      `;
      const result = await pool.query(query, [account.id, limit]);
      return result.rows;
    },

    // Uses stored function for complex calculation
    transactionsWithBalance: async (account, { limit }, context) => {
      const query = `
        SELECT * FROM get_transactions_with_balance($1, $2)
      `;
      const result = await pool.query(query, [account.id, limit]);
      return result.rows;
    },

    // Computed field using aggregation
    monthlyAverage: async (account, args, context) => {
      const query = `
        SELECT AVG(month_total) as average
        FROM (
          SELECT DATE_TRUNC('month', transaction_date) as month,
                 SUM(CASE 
                   WHEN transaction_type = 'debit' THEN amount 
                   ELSE 0 
                 END) as month_total
          FROM transactions
          WHERE account_id = $1
            AND transaction_date >= CURRENT_DATE - INTERVAL '6 months'
          GROUP BY DATE_TRUNC('month', transaction_date)
        ) monthly_totals
      `;
      const result = await pool.query(query, [account.id]);
      return result.rows[0].average || 0;
    }
  },

  Transaction: {
    // Simple relationship using DataLoader
    account: async (transaction, args, context) => {
      return accountLoader.load(transaction.account_id);
    },

    // Optional relationship with DataLoader
    category: async (transaction, args, context) => {
      if (!transaction.category_id) return null;
      return categoryLoader.load(transaction.category_id);
    },

    // Many-to-many relationship via junction table
    tags: async (transaction, args, context) => {
      const query = `
        SELECT t.* 
        FROM tags t
        JOIN transaction_tags tt ON t.id = tt.tag_id
        WHERE tt.transaction_id = $1
        ORDER BY t.name
      `;
      const result = await pool.query(query, [transaction.id]);
      return result.rows;
    }
  },

  Category: {
    // Self-referential relationship
    parent: async (category, args, context) => {
      if (!category.parent_id) return null;
      return categoryLoader.load(category.parent_id);
    },

    children: async (category, args, context) => {
      const query = `
        SELECT * FROM categories 
        WHERE parent_id = $1
        ORDER BY name
      `;
      const result = await pool.query(query, [category.id]);
      return result.rows;
    },

    // Complex aggregation with date math
    currentMonthSpending: async (category, args, context) => {
      const query = `
        SELECT COALESCE(SUM(amount), 0) as total
        FROM transactions
        WHERE category_id = $1
          AND transaction_type = 'debit'
          AND DATE_TRUNC('month', transaction_date) = DATE_TRUNC('month', CURRENT_DATE)
      `;
      const result = await pool.query(query, [category.id]);
      return result.rows[0].total;
    }
  },

  Budget: {
    // Many-to-many with additional fields
    categories: async (budget, args, context) => {
      const query = `
        SELECT 
          c.*,
          bc.amount as budget_amount,
          COALESCE(SUM(t.amount), 0) as spent,
          bc.amount - COALESCE(SUM(t.amount), 0) as remaining
        FROM budget_categories bc
        JOIN categories c ON bc.category_id = c.id
        LEFT JOIN transactions t ON c.id = t.category_id
          AND t.transaction_type = 'debit'
          AND t.transaction_date >= DATE_TRUNC('month', CURRENT_DATE)
          AND t.transaction_date < DATE_TRUNC('month', CURRENT_DATE) + INTERVAL '1 month'
        WHERE bc.budget_id = $1
        GROUP BY c.id, bc.amount
      `;
      const result = await pool.query(query, [budget.id]);
      
      return result.rows.map(row => ({
        category: row,
        amount: row.budget_amount,
        spent: row.spent,
        remaining: row.remaining,
        percentageUsed: row.budget_amount > 0 ? (row.spent / row.budget_amount * 100) : 0
      }));
    },

    // Uses stored function
    performance: async (budget, { startDate, endDate }, context) => {
      const query = `
        SELECT * FROM calculate_budget_performance($1, $2, $3)
      `;
      const result = await pool.query(query, [budget.id, startDate, endDate]);
      return result.rows;
    }
  },

  Mutation: {
    // Transaction with multiple INSERTs
    createTransfer: async (parent, { input }, context) => {
      const client = await pool.connect();
      
      try {
        await client.query('BEGIN');
        
        // Create debit transaction
        const debitResult = await client.query(`
          INSERT INTO transactions (account_id, amount, transaction_date, description, transaction_type)
          VALUES ($1, $2, $3, $4, 'debit')
          RETURNING *
        `, [input.fromAccountId, input.amount, input.date, input.description]);
        
        // Create credit transaction
        const creditResult = await client.query(`
          INSERT INTO transactions (account_id, amount, transaction_date, description, transaction_type)
          VALUES ($1, $2, $3, $4, 'credit')
          RETURNING *
        `, [input.toAccountId, input.amount, input.date, input.description]);
        
        // Create transfer record
        const transferResult = await client.query(`
          INSERT INTO transfers (from_account_id, to_account_id, from_transaction_id, to_transaction_id, amount, transfer_date)
          VALUES ($1, $2, $3, $4, $5, $6)
          RETURNING *
        `, [
          input.fromAccountId,
          input.toAccountId,
          debitResult.rows[0].id,
          creditResult.rows[0].id,
          input.amount,
          input.date
        ]);
        
        // Update account balances
        await client.query(`
          UPDATE accounts 
          SET balance = balance - $1,
              updated_at = CURRENT_TIMESTAMP
          WHERE id = $2
        `, [input.amount, input.fromAccountId]);
        
        await client.query(`
          UPDATE accounts 
          SET balance = balance + $1,
              updated_at = CURRENT_TIMESTAMP
          WHERE id = $2
        `, [input.amount, input.toAccountId]);
        
        await client.query('COMMIT');
        
        return transferResult.rows[0];
      } catch (error) {
        await client.query('ROLLBACK');
        throw error;
      } finally {
        client.release();
      }
    }
  },

  Subscription: {
    // Using PostgreSQL LISTEN/NOTIFY
    transactionAdded: {
      subscribe: async (parent, { accountId }, context) => {
        const client = await pool.connect();
        const channel = `transaction_added_${accountId}`;
        
        await client.query(`LISTEN ${channel}`);
        
        // Return async iterator for GraphQL subscriptions
        return {
          [Symbol.asyncIterator]: () => ({
            next: () => new Promise((resolve) => {
              client.on('notification', (msg) => {
                if (msg.channel === channel) {
                  resolve({ 
                    value: { transactionAdded: JSON.parse(msg.payload) },
                    done: false 
                  });
                }
              });
            })
          })
        };
      }
    }
  }
};

export default resolvers;
```

## Key Takeaways: GraphQL with PostgreSQL

The relationship between GraphQL and PostgreSQL involves three layers working together:

### 1. **PostgreSQL provides the relational structure**

- Foreign keys enforce data integrity
- Indexes optimize query performance
- Views and functions handle complex aggregations
- Row-level security provides multi-tenancy

### 2. **GraphQL provides the flexible query interface**

- Clients can request exactly the data they need
- Nested queries traverse relationships naturally
- Single endpoint serves all data needs
- Type system provides clear contracts

### 3. **The resolver layer bridges the gap**

- DataLoaders batch database queries to prevent N+1 problems
- Resolvers translate GraphQL queries into optimized SQL
- Context can handle authentication and set PostgreSQL session variables
- Transactions ensure data consistency for complex mutations

### Performance Optimizations

**Database Level:**

- Composite indexes match common GraphQL query patterns
- Materialized views cache expensive aggregations
- Stored functions encapsulate complex business logic
- Connection pooling manages database resources

**Resolver Level:**

- DataLoaders batch and cache database requests
- Query complexity analysis prevents expensive queries
- Cursor-based pagination handles large datasets
- Field-level resolvers enable lazy loading

### Real-World Benefits

For your finance application, this architecture gives you:

- **Flexibility**: Add new fields or relationships without versioning your API
- **Performance**: Optimize database queries independently of your API structure
- **Type Safety**: GraphQL schema provides compile-time type checking
- **Efficiency**: Clients get exactly what they need in one request
- **Scalability**: Can start simple and add caching/optimization as needed

The beauty is that PostgreSQL's relational model maps naturally to GraphQL's graph model - foreign keys become GraphQL relationships, views become computed fields, and stored functions become complex field resolvers. This gives you the best of both worlds: PostgreSQL's data integrity and GraphQL's query flexibility.

**human** (Aug 29, 2025, 09:39 PM)

Okay, lets toss in a bit more detail. If I wanted to design this application in .NET with Hotchocolate GraphQL library with a hexagonal architecture, would that be a good fit?

**assistant** (Aug 29, 2025, 09:43 PM)

 Absolutely! .NET with HotChocolate and hexagonal architecture is an excellent fit for a finance application. This combination gives you strong typing, clean separation of concerns, and enterprise-grade robustness. Let me show you how this would be structured:

#### create .NET Hexagonal Architecture for Finance App with HotChocolate

```csharp
// ============================================
// SOLUTION STRUCTURE
// ============================================
/*
FinanceTracker/
├── src/
│   ├── FinanceTracker.Domain/           # Core business logic (center of hexagon)
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Aggregates/
│   │   ├── Events/
│   │   ├── Exceptions/
│   │   └── Specifications/
│   │
│   ├── FinanceTracker.Application/      # Application services (use cases)
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Validators/
│   │
│   ├── FinanceTracker.Infrastructure/   # Secondary adapters
│   │   ├── Persistence/
│   │   │   ├── EntityConfigurations/
│   │   │   ├── Repositories/
│   │   │   └── Migrations/
│   │   ├── Services/
│   │   └── External/
│   │
│   ├── FinanceTracker.GraphQL/          # Primary adapter (GraphQL API)
│   │   ├── Types/
│   │   ├── Queries/
│   │   ├── Mutations/
│   │   ├── Subscriptions/
│   │   ├── DataLoaders/
│   │   └── Extensions/
│   │
│   └── FinanceTracker.WebApi/           # Host application
│       └── Program.cs
│
└── tests/
    ├── FinanceTracker.Domain.Tests/
    ├── FinanceTracker.Application.Tests/
    └── FinanceTracker.GraphQL.Tests/
*/

// ============================================
// DOMAIN LAYER - Core Business Logic
// ============================================

// Domain/ValueObjects/Money.cs
namespace FinanceTracker.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency = "USD")
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required");
        
        Amount = Math.Round(amount, 2);
        Currency = currency.ToUpperInvariant();
    }
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add {Currency} and {other.Currency}");
        
        return new Money(Amount + other.Amount, Currency);
    }
    
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract {other.Currency} from {Currency}");
        
        return new Money(Amount - other.Amount, Currency);
    }
    
    public static Money Zero(string currency = "USD") => new(0, currency);
    
    public bool IsNegative => Amount < 0;
    public bool IsPositive => Amount > 0;
}

// Domain/Entities/Transaction.cs
namespace FinanceTracker.Domain.Entities;

public class Transaction : Entity
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public Money Amount { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string Description { get; private set; }
    public TransactionType Type { get; private set; }
    public TransactionStatus Status { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? PayeeId { get; private set; }
    public string? Notes { get; private set; }
    public List<Guid> TagIds { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Navigation properties
    public virtual Account Account { get; private set; }
    public virtual Category? Category { get; private set; }
    public virtual Payee? Payee { get; private set; }
    public virtual ICollection<Tag> Tags { get; private set; }
    
    protected Transaction() { } // For EF Core
    
    public Transaction(
        Guid accountId,
        Money amount,
        DateTime transactionDate,
        string description,
        TransactionType type)
    {
        Id = Guid.NewGuid();
        AccountId = accountId;
        Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        TransactionDate = transactionDate;
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Type = type;
        Status = TransactionStatus.Cleared;
        TagIds = new List<Guid>();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TransactionCreatedEvent(this));
    }
    
    public void Categorize(Guid categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TransactionCategorizedEvent(Id, categoryId));
    }
    
    public void UpdateStatus(TransactionStatus newStatus)
    {
        if (Status == TransactionStatus.Reconciled && newStatus != TransactionStatus.Reconciled)
            throw new InvalidOperationException("Cannot change status of reconciled transaction");
        
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
}

// Domain/Aggregates/Account.cs
namespace FinanceTracker.Domain.Aggregates;

public class Account : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public Money Balance { get; private set; }
    public string? Institution { get; private set; }
    public decimal? InterestRate { get; private set; }
    public Money? CreditLimit { get; private set; }
    public bool IsActive { get; private set; }
    
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    
    protected Account() { } // For EF Core
    
    public Account(Guid userId, string name, AccountType type, Money initialBalance)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Type = type;
        Balance = initialBalance ?? Money.Zero();
        IsActive = true;
        
        ValidateAccountType();
        AddDomainEvent(new AccountCreatedEvent(this));
    }
    
    public Transaction AddTransaction(Money amount, DateTime date, string description, TransactionType type)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot add transaction to inactive account");
        
        var transaction = new Transaction(Id, amount, date, description, type);
        _transactions.Add(transaction);
        
        // Update balance
        if (type == TransactionType.Debit)
            Balance = Balance.Subtract(amount);
        else
            Balance = Balance.Add(amount);
        
        ValidateBalance();
        
        AddDomainEvent(new TransactionAddedEvent(transaction));
        
        return transaction;
    }
    
    public void Reconcile(DateTime asOfDate, Money expectedBalance)
    {
        var calculatedBalance = CalculateBalanceAsOf(asOfDate);
        
        if (calculatedBalance != expectedBalance)
        {
            throw new InvalidOperationException(
                $"Reconciliation failed. Expected: {expectedBalance}, Calculated: {calculatedBalance}");
        }
        
        foreach (var transaction in _transactions.Where(t => t.TransactionDate <= asOfDate))
        {
            transaction.UpdateStatus(TransactionStatus.Reconciled);
        }
        
        AddDomainEvent(new AccountReconciledEvent(Id, asOfDate, expectedBalance));
    }
    
    private Money CalculateBalanceAsOf(DateTime date)
    {
        var relevantTransactions = _transactions.Where(t => t.TransactionDate <= date);
        var balance = Money.Zero(Balance.Currency);
        
        foreach (var transaction in relevantTransactions)
        {
            balance = transaction.Type == TransactionType.Debit 
                ? balance.Subtract(transaction.Amount)
                : balance.Add(transaction.Amount);
        }
        
        return balance;
    }
    
    private void ValidateBalance()
    {
        if (Type == AccountType.CreditCard && CreditLimit != null)
        {
            if (Balance.Amount < -CreditLimit.Amount)
                throw new InvalidOperationException("Transaction would exceed credit limit");
        }
    }
    
    private void ValidateAccountType()
    {
        if (Type == AccountType.CreditCard && Balance.IsPositive)
            throw new InvalidOperationException("Credit card accounts should have negative or zero balance");
    }
}

// Domain/Specifications/TransactionSpecifications.cs
namespace FinanceTracker.Domain.Specifications;

public class TransactionsByDateRangeSpec : Specification<Transaction>
{
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;
    
    public TransactionsByDateRangeSpec(DateTime startDate, DateTime endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }
    
    public override Expression<Func<Transaction, bool>> ToExpression()
    {
        return t => t.TransactionDate >= _startDate && t.TransactionDate <= _endDate;
    }
}

// ============================================
// APPLICATION LAYER - Use Cases
// ============================================

// Application/Interfaces/IAccountRepository.cs
namespace FinanceTracker.Application.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account?> GetWithTransactionsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}

// Application/Commands/CreateTransferCommand.cs
namespace FinanceTracker.Application.Commands;

public record CreateTransferCommand(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    string Currency,
    DateTime Date,
    string Description) : IRequest<TransferResult>;

public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, TransferResult>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;
    
    public CreateTransferCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }
    
    public async Task<TransferResult> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        // Load aggregates
        var fromAccount = await _accountRepository.GetWithTransactionsAsync(request.FromAccountId, cancellationToken);
        var toAccount = await _accountRepository.GetWithTransactionsAsync(request.ToAccountId, cancellationToken);
        
        if (fromAccount == null || toAccount == null)
            throw new NotFoundException("Account not found");
        
        // Validate same user owns both accounts
        if (fromAccount.UserId != toAccount.UserId)
            throw new UnauthorizedException("Cannot transfer between accounts of different users");
        
        var amount = new Money(request.Amount, request.Currency);
        
        // Use domain logic to create transactions
        var debitTransaction = fromAccount.AddTransaction(
            amount, 
            request.Date, 
            $"Transfer to {toAccount.Name}: {request.Description}",
            TransactionType.Debit);
        
        var creditTransaction = toAccount.AddTransaction(
            amount,
            request.Date,
            $"Transfer from {fromAccount.Name}: {request.Description}",
            TransactionType.Credit);
        
        // Create transfer entity
        var transfer = new Transfer(
            fromAccount.Id,
            toAccount.Id,
            debitTransaction.Id,
            creditTransaction.Id,
            amount,
            request.Date);
        
        // Persist changes
        await _accountRepository.UpdateAsync(fromAccount, cancellationToken);
        await _accountRepository.UpdateAsync(toAccount, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        // Publish domain events
        foreach (var domainEvent in fromAccount.DomainEvents.Concat(toAccount.DomainEvents))
        {
            await _eventBus.PublishAsync(domainEvent, cancellationToken);
        }
        
        return new TransferResult(transfer.Id, debitTransaction.Id, creditTransaction.Id);
    }
}

// Application/Queries/GetAccountBalanceQuery.cs
namespace FinanceTracker.Application.Queries;

public record GetAccountBalanceQuery(Guid AccountId) : IRequest<AccountBalanceDto>;

public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, AccountBalanceDto>
{
    private readonly IDbConnection _dbConnection;
    
    public GetAccountBalanceQueryHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<AccountBalanceDto> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        // Use Dapper for read queries - bypass domain for performance
        const string sql = @"
            SELECT 
                a.id,
                a.name,
                a.balance,
                a.currency,
                COUNT(t.id) as transaction_count,
                MAX(t.transaction_date) as last_transaction_date
            FROM accounts a
            LEFT JOIN transactions t ON a.id = t.account_id
            WHERE a.id = @AccountId
            GROUP BY a.id, a.name, a.balance, a.currency";
        
        var result = await _dbConnection.QuerySingleOrDefaultAsync<AccountBalanceDto>(
            sql, 
            new { request.AccountId });
        
        if (result == null)
            throw new NotFoundException($"Account {request.AccountId} not found");
        
        return result;
    }
}

// Application/Services/BudgetAnalysisService.cs
namespace FinanceTracker.Application.Services;

public interface IBudgetAnalysisService
{
    Task<BudgetPerformance> AnalyzeBudgetPerformanceAsync(
        Guid budgetId, 
        DateTime startDate, 
        DateTime endDate,
        CancellationToken cancellationToken = default);
}

public class BudgetAnalysisService : IBudgetAnalysisService
{
    private readonly IDbConnection _dbConnection;
    private readonly IBudgetRepository _budgetRepository;
    
    public async Task<BudgetPerformance> AnalyzeBudgetPerformanceAsync(
        Guid budgetId,
        DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetId, cancellationToken);
        if (budget == null)
            throw new NotFoundException($"Budget {budgetId} not found");
        
        // Complex query using Dapper
        const string sql = @"
            SELECT 
                c.id AS CategoryId,
                c.name AS CategoryName,
                bc.amount AS BudgetedAmount,
                COALESCE(SUM(t.amount), 0) AS SpentAmount,
                bc.amount - COALESCE(SUM(t.amount), 0) AS RemainingAmount,
                CASE 
                    WHEN bc.amount > 0 THEN (COALESCE(SUM(t.amount), 0) / bc.amount * 100)
                    ELSE 0
                END AS PercentageUsed
            FROM budget_categories bc
            INNER JOIN categories c ON bc.category_id = c.id
            LEFT JOIN transactions t ON c.id = t.category_id
                AND t.transaction_date BETWEEN @StartDate AND @EndDate
                AND t.transaction_type = 'Debit'
            WHERE bc.budget_id = @BudgetId
            GROUP BY c.id, c.name, bc.amount";
        
        var categoryPerformances = await _dbConnection.QueryAsync<CategoryBudgetPerformance>(
            sql,
            new { BudgetId = budgetId, StartDate = startDate, EndDate = endDate });
        
        return new BudgetPerformance
        {
            BudgetId = budgetId,
            StartDate = startDate,
            EndDate = endDate,
            Categories = categoryPerformances.ToList(),
            TotalBudgeted = categoryPerformances.Sum(c => c.BudgetedAmount),
            TotalSpent = categoryPerformances.Sum(c => c.SpentAmount),
            TotalRemaining = categoryPerformances.Sum(c => c.RemainingAmount)
        };
    }
}

// ============================================
// INFRASTRUCTURE LAYER - Secondary Adapters
// ============================================

// Infrastructure/Persistence/FinanceContext.cs
namespace FinanceTracker.Infrastructure.Persistence;

public class FinanceContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    
    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceContext).Assembly);
        
        // Configure value objects
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.OwnsOne(t => t.Amount, amount =>
            {
                amount.Property(m => m.Amount).HasColumnName("amount").HasPrecision(15, 2);
                amount.Property(m => m.Currency).HasColumnName("currency").HasMaxLength(3);
            });
        });
        
        // Configure many-to-many relationships
        modelBuilder.Entity<Transaction>()
            .HasMany(t => t.Tags)
            .WithMany(tag => tag.Transactions)
            .UsingEntity("transaction_tags");
        
        // Add indexes
        modelBuilder.Entity<Transaction>()
            .HasIndex(t => new { t.AccountId, t.TransactionDate })
            .HasDatabaseName("IX_Transaction_AccountDate");
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Update timestamps
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}

// Infrastructure/Repositories/AccountRepository.cs
namespace FinanceTracker.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FinanceContext _context;
    
    public AccountRepository(FinanceContext context)
    {
        _context = context;
    }
    
    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
    
    public async Task<Account?> GetWithTransactionsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .Include(a => a.Transactions)
                .ThenInclude(t => t.Category)
            .Include(a => a.Transactions)
                .ThenInclude(t => t.Tags)
            .AsSplitQuery()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Account>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts
            .Where(a => a.UserId == userId && a.IsActive)
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);
    }
    
    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        await _context.Accounts.AddAsync(account, cancellationToken);
    }
    
    public Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Update(account);
        return Task.CompletedTask;
    }
}

// ============================================
// GRAPHQL LAYER - Primary Adapter
// ============================================

// GraphQL/Types/AccountType.cs
namespace FinanceTracker.GraphQL.Types;

[ExtendObjectType(typeof(Account))]
public class AccountTypeExtensions
{
    [UseDbContext(typeof(FinanceContext))]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Transaction> GetTransactions(
        [Parent] Account account,
        [ScopedService] FinanceContext context)
    {
        return context.Transactions
            .Where(t => t.AccountId == account.Id)
            .OrderByDescending(t => t.TransactionDate);
    }
    
    [DataLoader]
    internal static async Task<ILookup<Guid, Transaction>> GetTransactionsByAccountIdAsync(
        IReadOnlyList<Guid> accountIds,
        FinanceContext context,
        CancellationToken cancellationToken)
    {
        var transactions = await context.Transactions
            .Where(t => accountIds.Contains(t.AccountId))
            .ToListAsync(cancellationToken);
        
        return transactions.ToLookup(t => t.AccountId);
    }
    
    public async Task<decimal> GetMonthlyAverage(
        [Parent] Account account,
        [Service] IDbConnection connection)
    {
        const string sql = @"
            SELECT AVG(month_total) 
            FROM (
                SELECT DATE_TRUNC('month', transaction_date) as month,
                       SUM(CASE WHEN transaction_type = 'Debit' THEN amount ELSE 0 END) as month_total
                FROM transactions
                WHERE account_id = @AccountId
                  AND transaction_date >= DATEADD(month, -6, GETDATE())
                GROUP BY DATE_TRUNC('month', transaction_date)
            ) monthly";
        
        return await connection.QuerySingleOrDefaultAsync<decimal>(
            sql, 
            new { AccountId = account.Id });
    }
    
    [GraphQLName("runningBalance")]
    public async Task<IEnumerable<TransactionWithBalance>> GetTransactionsWithRunningBalance(
        [Parent] Account account,
        [Service] IAccountService accountService,
        int limit = 50)
    {
        return await accountService.GetTransactionsWithRunningBalanceAsync(account.Id, limit);
    }
}

// GraphQL/Queries/AccountQueries.cs
namespace FinanceTracker.GraphQL.Queries;

[ExtendObjectType("Query")]
public class AccountQueries
{
    [UseDbContext(typeof(FinanceContext))]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Account> GetAccount(
        Guid id,
        [ScopedService] FinanceContext context,
        [Service] ICurrentUserService currentUser)
    {
        return context.Accounts
            .Where(a => a.Id == id && a.UserId == currentUser.UserId);
    }
    
    [UseDbContext(typeof(FinanceContext))]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Account> GetAccounts(
        [ScopedService] FinanceContext context,
        [Service] ICurrentUserService currentUser)
    {
        return context.Accounts
            .Where(a => a.UserId == currentUser.UserId && a.IsActive);
    }
    
    public async Task<NetWorth> GetNetWorth(
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        return await mediator.Send(new GetNetWorthQuery(currentUser.UserId));
    }
}

// GraphQL/Mutations/AccountMutations.cs
namespace FinanceTracker.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class AccountMutations
{
    [Error(typeof(ValidationException))]
    [Error(typeof(NotFoundException))]
    [UseDbContext(typeof(FinanceContext))]
    [UseProjection]
    public async Task<Account> CreateAccount(
        CreateAccountInput input,
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser,
        [ScopedService] FinanceContext context)
    {
        var command = new CreateAccountCommand(
            currentUser.UserId,
            input.Name,
            input.AccountType,
            input.InitialBalance,
            input.Currency ?? "USD");
        
        var accountId = await mediator.Send(command);
        
        return await context.Accounts
            .FirstAsync(a => a.Id == accountId);
    }
    
    [Error(typeof(ValidationException))]
    [Error(typeof(InsufficientFundsException))]
    public async Task<TransferPayload> CreateTransfer(
        CreateTransferInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateTransferCommand(
            input.FromAccountId,
            input.ToAccountId,
            input.Amount,
            input.Currency ?? "USD",
            input.Date ?? DateTime.UtcNow,
            input.Description);
        
        var result = await mediator.Send(command);
        
        return new TransferPayload(result);
    }
    
    [Error(typeof(ConcurrencyException))]
    public async Task<Transaction> UpdateTransaction(
        Guid id,
        UpdateTransactionInput input,
        [Service] IMediator mediator)
    {
        var command = new UpdateTransactionCommand(id, input);
        return await mediator.Send(command);
    }
}

// GraphQL/Subscriptions/TransactionSubscriptions.cs
namespace FinanceTracker.GraphQL.Subscriptions;

[ExtendObjectType("Subscription")]
public class TransactionSubscriptions
{
    [Subscribe]
    [Topic("{accountId}")]
    public Transaction OnTransactionAdded(
        [EventMessage] Transaction transaction,
        Guid accountId)
    {
        return transaction;
    }
    
    [Subscribe]
    public ValueTask<ISourceStream<BudgetAlert>> OnBudgetAlert(
        Guid budgetId,
        [Service] ITopicEventReceiver receiver)
    {
        return receiver.SubscribeAsync<BudgetAlert>($"budget-alert-{budgetId}");
    }
}

// GraphQL/DataLoaders/CategoryDataLoader.cs
namespace FinanceTracker.GraphQL.DataLoaders;

public class CategoryDataLoader : BatchDataLoader<Guid, Category>
{
    private readonly IDbContextFactory<FinanceContext> _contextFactory;
    
    public CategoryDataLoader(
        IDbContextFactory<FinanceContext> contextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) 
        : base(batchScheduler, options)
    {
        _contextFactory = contextFactory;
    }
    
    protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        return await context.Categories
            .Where(c => keys.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }
}

// ============================================
// HOST CONFIGURATION
// ============================================

// WebApi/Program.cs
namespace FinanceTracker.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services - Domain
        builder.Services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        });
        
        // Add services - Infrastructure
        builder.Services.AddDbContext<FinanceContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("FinanceDb"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(FinanceContext).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(3);
                });
            
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        builder.Services.AddPooledDbContextFactory<FinanceContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("FinanceDb"));
        });
        
        // Add Dapper connection for read queries
        builder.Services.AddScoped<IDbConnection>(sp =>
            new NpgsqlConnection(builder.Configuration.GetConnectionString("FinanceDb")));
        
        // Register repositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Add application services
        builder.Services.AddScoped<IBudgetAnalysisService, BudgetAnalysisService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        
        // Add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Auth:Authority"];
                options.Audience = builder.Configuration["Auth:Audience"];
            });
        
        builder.Services.AddAuthorization();
        
        // Add current user service
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        // Add GraphQL with HotChocolate
        builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<AccountQueries>()
                .AddTypeExtension<TransactionQueries>()
                .AddTypeExtension<BudgetQueries>()
            .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<AccountMutations>()
                .AddTypeExtension<TransactionMutations>()
                .AddTypeExtension<BudgetMutations>()
            .AddSubscriptionType(d => d.Name("Subscription"))
                .AddTypeExtension<TransactionSubscriptions>()
            .AddType<AccountType>()
            .AddType<TransactionType>()
            .AddType<CategoryType>()
            .AddTypeExtension<AccountTypeExtensions>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<FinanceContext>(DbContextKind.Pooled)
            .AddDataLoader<CategoryDataLoader>()
            .AddDataLoader<PayeeDataLoader>()
            .AddInMemorySubscriptions()
            .AddErrorFilter<GraphQLErrorFilter>()
            .AddDiagnosticEventListener<GraphQLDiagnosticEventListener>()
            .ModifyRequestOptions(opt => 
            {
                opt.IncludeExceptionDetails = builder.Environment.IsDevelopment();
            });
        
        // Add Redis for distributed caching
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });
        
        // Add health checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<FinanceContext>()
            .AddRedis(builder.Configuration.GetConnectionString("Redis"));
        
        var app = builder.Build();
        
        // Configure pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseWebSockets();
        
        app.MapGraphQL()
            .RequireAuthorization();
        
        app.MapBananaCakePop("/graphql-ui")
            .RequireAuthorization();
        
        app.MapHealthChecks("/health");
        
        app.Run();
    }
}

// ============================================
// CONFIGURATION
// ============================================

// appsettings.json structure
/*
{
  "ConnectionStrings": {
    "FinanceDb": "Host=localhost;Database=finance_tracker;Username=postgres;Password=password",
    "Redis": "localhost:6379"
  },
  "Auth": {
    "Authority": "https://your-auth-provider.com",
    "Audience": "finance-tracker-api"
  },
  "GraphQL": {
    "MaxExecutionDepth": 15,
    "MaxComplexity": 1000
  }
}
*/
```

## Why This Architecture Works Perfectly

The combination of .NET, HotChocolate, and hexagonal architecture is excellent for a finance application for several reasons:

### **Hexagonal Architecture Benefits**

**Clean Separation**: The domain layer (center of the hexagon) contains pure business logic without any infrastructure concerns. Your `Account` aggregate can enforce business rules like credit limits without knowing anything about databases or GraphQL.

**Testability**: Each layer can be tested independently. Domain logic tests don't need a database, application services can use mock repositories, and GraphQL resolvers can be tested with in-memory data.

**Flexibility**: You can swap adapters without touching core logic. Switch from PostgreSQL to CosmosDB? Just write new repository implementations. Add a REST API alongside GraphQL? Add another primary adapter.

### **HotChocolate Strengths**

**Code-First Approach**: Your C# classes become your GraphQL schema. The type safety flows from database through domain to API, catching errors at compile time.

**Built-in Optimizations**:

- **Projections**: Automatically generates efficient SQL queries based on GraphQL field selection
- **DataLoaders**: Solves N+1 problems automatically
- **Filtering/Sorting/Paging**: Adds these capabilities with simple attributes

**Integration Excellence**: HotChocolate integrates beautifully with:

- Entity Framework Core for automatic query optimization
- MediatR for CQRS pattern
- Dependency injection for clean architecture
- Authorization policies for security

### **Financial Domain Fit**

**Value Objects**: Money as a value object ensures you never accidentally add USD to EUR. The type system prevents entire categories of bugs.

**Aggregate Boundaries**: Account as an aggregate root ensures transactional consistency. All changes to transactions go through the account, maintaining invariants.

**Event Sourcing Ready**: The domain events in the aggregates can easily be persisted for audit trails, crucial for financial applications.

**CQRS Pattern**: Using MediatR separates commands (write operations with business logic) from queries (optimized reads with Dapper), perfect for the read-heavy nature of finance apps.

### **Performance Optimizations**

The architecture provides multiple levels of optimization:

1. **Database**: Pooled DbContext, split queries, compiled queries
2. **Application**: Dapper for complex read queries, avoiding EF overhead
3. **GraphQL**: DataLoaders batch requests, projections minimize data fetched
4. **Caching**: Redis for frequently accessed data

### **Real-World Considerations**

**Validation**: FluentValidation in the application layer ensures data integrity before it reaches the domain.

**Security**: Row-level security via the `ICurrentUserService` ensures users only see their data.

**Observability**: Built-in diagnostics for GraphQL operations, health checks for monitoring.

**Scalability**: The architecture supports horizontal scaling - you can run multiple instances behind a load balancer with Redis managing distributed cache and subscriptions.

### **Development Experience**

The strongly-typed nature of C# combined with HotChocolate's code-first approach means:

- IntelliSense everywhere
- Compile-time error checking
- Automatic GraphQL schema generation
- Self-documenting API through GraphQL introspection

This architecture gives you enterprise-grade robustness while maintaining the flexibility that makes GraphQL attractive. The hexagonal architecture ensures your business logic remains pure and testable, while HotChocolate provides a powerful, performant GraphQL layer that can evolve with your needs.

**human** (Aug 29, 2025, 09:45 PM)

Are there any other critical considerations that I havent brought up yet about this applications needs, or any extra information I could provide to help you provide more complete answers?

**assistant** (Aug 29, 2025, 09:46 PM)

 Excellent question! Let me highlight some critical considerations that haven't been addressed yet but are essential for a production finance application:

## Critical Missing Considerations

### **1. Multi-Currency Handling**

Your application will likely need sophisticated currency support:

- **Exchange Rates**: Historical rates for accurate reporting, real-time rates for conversions
- **Currency Conversion**: When/how to convert, which rate provider to use
- **Reporting Currency**: Users may want to view everything in their "home" currency
- **Transaction-level Currency**: Supporting accounts in different currencies

**Questions for you:**

- Will users have accounts in multiple currencies?
- Do you need historical exchange rates for accurate past reporting?
- Should the app support cryptocurrency?

### **2. Financial Calculations & Precision**

Financial calculations have unique requirements:

- **Decimal Precision**: How many decimal places? (crypto needs more than traditional currency)
- **Rounding Rules**: Different financial contexts require different rounding (banker's rounding vs. standard)
- **Interest Calculations**: Compound vs. simple, different compounding periods
- **Tax Calculations**: Vary by jurisdiction and transaction type

**Questions for you:**

- What level of precision do you need?
- Will you calculate interest or investment returns?
- Do you need tax categorization or reporting?

### **3. Data Import/Export & Integrations**

Users rarely want to manually enter every transaction:

- **Bank Connections**: Plaid, Yodlee, or direct bank APIs?
- **File Imports**: OFX, QFX, CSV formats from banks
- **Export Formats**: Tax software, spreadsheets, accounting software
- **Open Banking**: PSD2 in Europe, similar regulations elsewhere

**Questions for you:**

- Which banks/financial institutions do you need to support?
- What import formats are essential?
- Do you need real-time transaction sync or periodic imports?

### **4. Compliance & Regulations**

Financial applications face strict requirements:

- **Data Residency**: Where can you store financial data?
- **PCI Compliance**: If handling card numbers
- **GDPR/Privacy Laws**: Right to deletion, data portability
- **Financial Regulations**: Vary by country (SOC2, PSD2, etc.)
- **Audit Trails**: Immutable logs of all changes

**Questions for you:**

- Which geographic regions will you serve?
- Will you store sensitive data like account numbers?
- What are your audit/compliance requirements?

### **5. Security Architecture**

Beyond basic authentication:

```csharp
// Additional security layers needed:
- Field-level encryption for sensitive data
- Token management for bank connections  
- Secure storage of API keys
- Rate limiting per user/operation
- Fraud detection patterns
- Session management
- 2FA/MFA requirements
```

**Questions for you:**

- What authentication provider will you use?
- Do you need field-level encryption?
- What are your session timeout requirements?

### **6. Business Model & Monetization**

This affects technical architecture:

- **Freemium Limits**: Transaction limits, account limits, feature gates
- **Subscription Tiers**: Different features per tier
- **White-labeling**: B2B offerings for banks/companies
- **API Access**: Monetizing API access for third parties

**Questions for you:**

- What's your monetization model?
- Will you have different subscription tiers?
- Do you need usage-based billing tracking?

### **7. Scale & Performance Requirements**

Finance apps can have unique load patterns:

- **End-of-month**: Spike when people check finances
- **Tax Season**: Massive load increase
- **Market Hours**: If supporting investments
- **Batch Processing**: Recurring transactions, interest calculations

**Questions for you:**

- How many users are you targeting?
- What's your expected transaction volume?
- Do you need real-time or can some processes be async?

### **8. Mobile & Offline Considerations**

Many users primarily use mobile for finance:

- **Offline Support**: Entering transactions without connection
- **Sync Strategy**: Conflict resolution when syncing
- **Push Notifications**: Bill reminders, unusual transactions
- **Biometric Authentication**: FaceID/TouchID integration

**Questions for you:**

- Is mobile a primary platform?
- Do you need offline transaction entry?
- What notifications are critical?

### **9. Machine Learning & Intelligence**

Modern finance apps increasingly use ML:

- **Categorization**: Auto-categorizing transactions
- **Anomaly Detection**: Unusual spending patterns
- **Predictions**: Cash flow forecasting
- **Insights**: Spending trends, saving opportunities

**Questions for you:**

- Do you want automatic categorization?
- Should the app provide financial insights?
- Do you need fraud detection?

### **10. Specific Domain Features**

Depending on your focus:

**Personal Finance:**

- Bill tracking and reminders
- Shared/family accounts
- Envelope budgeting
- Debt snowball/avalanche calculators

**Business Finance:**

- Invoice management
- Receipt scanning/OCR
- Mileage tracking
- Employee expense management

**Investment Tracking:**

- Real-time quotes
- Cost basis tracking
- Dividend reinvestment
- Tax lot management

**Questions for you:**

- Is this for personal, business, or both?
- Which advanced features are must-haves vs. nice-to-haves?

### **Technical Architecture Additions Based on These**

Would you like me to show how any of these would modify the architecture? For example:

- Adding event sourcing for audit trails
- Implementing a service bus for bank integrations
- Adding background job processing for recurring transactions
- Implementing field-level encryption
- Setting up multi-tenancy for B2B scenarios

Understanding your specific requirements for these areas would help me provide more targeted architectural guidance and code examples.

**human** (Aug 29, 2025, 09:47 PM)

I would be interested in seeing how event source could help with audit trails

**assistant** (Aug 29, 2025, 09:51 PM)

 Excellent choice! Event sourcing is particularly powerful for finance applications because it provides an immutable audit trail, enables temporal queries, and can help with compliance. Let me show you how to implement event sourcing alongside your existing architecture.

#### create Event Sourcing for Finance App with Audit Trails

```csharp
// ============================================
// EVENT SOURCING ARCHITECTURE
// ============================================

// Domain/Events/Base/DomainEvent.cs
namespace FinanceTracker.Domain.Events;

public abstract record DomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    public string EventType => GetType().Name;
    public int EventVersion { get; init; } = 1;
    
    // Metadata for audit purposes
    public Guid? UserId { get; init; }
    public string? UserEmail { get; init; }
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }
    public Dictionary<string, object>? AdditionalMetadata { get; init; }
}

// Domain/Events/Account/AccountEvents.cs
namespace FinanceTracker.Domain.Events.Account;

public record AccountCreated : DomainEvent
{
    public Guid AccountId { get; init; }
    public string AccountName { get; init; }
    public AccountType AccountType { get; init; }
    public decimal InitialBalance { get; init; }
    public string Currency { get; init; }
    public string? Institution { get; init; }
}

public record AccountBalanceAdjusted : DomainEvent
{
    public Guid AccountId { get; init; }
    public decimal PreviousBalance { get; init; }
    public decimal NewBalance { get; init; }
    public string Reason { get; init; }
    public string AdjustmentType { get; init; } // "transaction", "reconciliation", "correction"
}

public record AccountClosed : DomainEvent
{
    public Guid AccountId { get; init; }
    public decimal FinalBalance { get; init; }
    public string ClosureReason { get; init; }
}

public record AccountReconciled : DomainEvent
{
    public Guid AccountId { get; init; }
    public DateTime ReconciliationDate { get; init; }
    public decimal StatementBalance { get; init; }
    public decimal CalculatedBalance { get; init; }
    public List<Guid> ReconciledTransactionIds { get; init; }
}

// Domain/Events/Transaction/TransactionEvents.cs
namespace FinanceTracker.Domain.Events.Transaction;

public record TransactionCreated : DomainEvent
{
    public Guid TransactionId { get; init; }
    public Guid AccountId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public TransactionType TransactionType { get; init; }
    public DateTime TransactionDate { get; init; }
    public string Description { get; init; }
    public Guid? CategoryId { get; init; }
    public Guid? PayeeId { get; init; }
    
    // Capture the state for audit
    public decimal AccountBalanceAfter { get; init; }
}

public record TransactionModified : DomainEvent
{
    public Guid TransactionId { get; init; }
    public Dictionary<string, object> OriginalValues { get; init; }
    public Dictionary<string, object> NewValues { get; init; }
    public string ModificationReason { get; init; }
}

public record TransactionCategorized : DomainEvent
{
    public Guid TransactionId { get; init; }
    public Guid? OldCategoryId { get; init; }
    public Guid NewCategoryId { get; init; }
    public bool WasAutoCategorized { get; init; }
}

public record TransactionDeleted : DomainEvent
{
    public Guid TransactionId { get; init; }
    public string DeletionReason { get; init; }
    public decimal AmountReversed { get; init; }
    public decimal AccountBalanceAfter { get; init; }
}

// Domain/Events/Budget/BudgetEvents.cs
namespace FinanceTracker.Domain.Events.Budget;

public record BudgetExceeded : DomainEvent
{
    public Guid BudgetId { get; init; }
    public Guid CategoryId { get; init; }
    public decimal BudgetAmount { get; init; }
    public decimal ActualAmount { get; init; }
    public decimal ExceededBy { get; init; }
    public DateTime PeriodStart { get; init; }
    public DateTime PeriodEnd { get; init; }
}

// ============================================
// EVENT STORE IMPLEMENTATION
// ============================================

// Infrastructure/EventStore/IEventStore.cs
namespace FinanceTracker.Infrastructure.EventStore;

public interface IEventStore
{
    Task<Guid> SaveEventAsync(DomainEvent @event, CancellationToken cancellationToken = default);
    Task SaveEventsAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken cancellationToken = default);
    Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, DateTime? fromDate, DateTime? toDate, CancellationToken cancellationToken = default);
    Task<T?> GetAggregateAsync<T>(Guid aggregateId, CancellationToken cancellationToken = default) where T : class, IEventSourcedAggregate, new();
    Task<T?> GetAggregateAtPointInTimeAsync<T>(Guid aggregateId, DateTime pointInTime, CancellationToken cancellationToken = default) where T : class, IEventSourcedAggregate, new();
}

// Infrastructure/EventStore/PostgresEventStore.cs
namespace FinanceTracker.Infrastructure.EventStore;

public class PostgresEventStore : IEventStore
{
    private readonly IDbConnection _connection;
    private readonly IEventSerializer _serializer;
    private readonly ILogger<PostgresEventStore> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PostgresEventStore(
        IDbConnection connection,
        IEventSerializer serializer,
        ILogger<PostgresEventStore> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _connection = connection;
        _serializer = serializer;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Guid> SaveEventAsync(DomainEvent @event, CancellationToken cancellationToken = default)
    {
        EnrichEventWithMetadata(@event);
        
        const string sql = @"
            INSERT INTO event_store (
                event_id,
                aggregate_id,
                event_type,
                event_version,
                event_data,
                event_metadata,
                occurred_at,
                user_id,
                user_email,
                ip_address,
                user_agent
            ) VALUES (
                @EventId,
                @AggregateId,
                @EventType,
                @EventVersion,
                @EventData::jsonb,
                @EventMetadata::jsonb,
                @OccurredAt,
                @UserId,
                @UserEmail,
                @IpAddress,
                @UserAgent
            )";
        
        var aggregateId = ExtractAggregateId(@event);
        var eventData = _serializer.Serialize(@event);
        var metadata = JsonSerializer.Serialize(@event.AdditionalMetadata ?? new Dictionary<string, object>());
        
        await _connection.ExecuteAsync(sql, new
        {
            @event.EventId,
            AggregateId = aggregateId,
            @event.EventType,
            @event.EventVersion,
            EventData = eventData,
            EventMetadata = metadata,
            @event.OccurredAt,
            @event.UserId,
            @event.UserEmail,
            @event.IpAddress,
            @event.UserAgent
        });
        
        _logger.LogInformation("Event {EventType} saved for aggregate {AggregateId}", 
            @event.EventType, aggregateId);
        
        return @event.EventId;
    }
    
    public async Task SaveEventsAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        using var transaction = _connection.BeginTransaction();
        
        try
        {
            foreach (var @event in events)
            {
                await SaveEventAsync(@event, cancellationToken);
            }
            
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    
    public async Task<IEnumerable<DomainEvent>> GetEventsAsync(
        Guid aggregateId, 
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                event_id,
                event_type,
                event_version,
                event_data,
                event_metadata,
                occurred_at,
                user_id,
                user_email
            FROM event_store
            WHERE aggregate_id = @AggregateId
            ORDER BY occurred_at, sequence_number";
        
        var results = await _connection.QueryAsync<EventStoreRecord>(sql, new { AggregateId = aggregateId });
        
        return results.Select(r => _serializer.Deserialize(r.EventType, r.EventData));
    }
    
    public async Task<T?> GetAggregateAsync<T>(
        Guid aggregateId,
        CancellationToken cancellationToken = default) where T : class, IEventSourcedAggregate, new()
    {
        var events = await GetEventsAsync(aggregateId, cancellationToken);
        if (!events.Any()) return null;
        
        var aggregate = new T();
        aggregate.LoadFromHistory(events);
        return aggregate;
    }
    
    public async Task<T?> GetAggregateAtPointInTimeAsync<T>(
        Guid aggregateId,
        DateTime pointInTime,
        CancellationToken cancellationToken = default) where T : class, IEventSourcedAggregate, new()
    {
        const string sql = @"
            SELECT 
                event_id,
                event_type,
                event_version,
                event_data,
                event_metadata,
                occurred_at
            FROM event_store
            WHERE aggregate_id = @AggregateId
              AND occurred_at <= @PointInTime
            ORDER BY occurred_at, sequence_number";
        
        var results = await _connection.QueryAsync<EventStoreRecord>(
            sql, 
            new { AggregateId = aggregateId, PointInTime = pointInTime });
        
        if (!results.Any()) return null;
        
        var events = results.Select(r => _serializer.Deserialize(r.EventType, r.EventData));
        
        var aggregate = new T();
        aggregate.LoadFromHistory(events);
        return aggregate;
    }
    
    private void EnrichEventWithMetadata(DomainEvent @event)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            @event.GetType().GetProperty(nameof(DomainEvent.IpAddress))?.SetValue(
                @event, context.Connection.RemoteIpAddress?.ToString());
            
            @event.GetType().GetProperty(nameof(DomainEvent.UserAgent))?.SetValue(
                @event, context.Request.Headers["User-Agent"].ToString());
        }
    }
    
    private Guid ExtractAggregateId(DomainEvent @event)
    {
        // Use reflection or pattern matching to extract the aggregate ID
        return @event switch
        {
            AccountCreated e => e.AccountId,
            AccountBalanceAdjusted e => e.AccountId,
            TransactionCreated e => e.TransactionId,
            TransactionModified e => e.TransactionId,
            _ => throw new InvalidOperationException($"Unknown event type: {@event.EventType}")
        };
    }
}

// ============================================
// EVENT-SOURCED AGGREGATE
// ============================================

// Domain/Aggregates/EventSourcedAccount.cs
namespace FinanceTracker.Domain.Aggregates;

public interface IEventSourcedAggregate
{
    Guid Id { get; }
    int Version { get; }
    void LoadFromHistory(IEnumerable<DomainEvent> events);
    IEnumerable<DomainEvent> GetUncommittedEvents();
    void MarkEventsAsCommitted();
}

public class EventSourcedAccount : IEventSourcedAggregate
{
    private readonly List<DomainEvent> _uncommittedEvents = new();
    private readonly Dictionary<Type, Action<DomainEvent>> _eventHandlers;
    
    public Guid Id { get; private set; }
    public int Version { get; private set; }
    
    // Current state rebuilt from events
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public Money Balance { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    
    // Transaction history
    private readonly List<TransactionSnapshot> _transactions = new();
    public IReadOnlyList<TransactionSnapshot> Transactions => _transactions.AsReadOnly();
    
    public EventSourcedAccount()
    {
        _eventHandlers = new Dictionary<Type, Action<DomainEvent>>
        {
            [typeof(AccountCreated)] = Apply,
            [typeof(TransactionCreated)] = Apply,
            [typeof(AccountBalanceAdjusted)] = Apply,
            [typeof(AccountReconciled)] = Apply,
            [typeof(AccountClosed)] = Apply
        };
    }
    
    // Create new account (command)
    public static EventSourcedAccount Create(string name, AccountType type, Money initialBalance, Guid userId)
    {
        var account = new EventSourcedAccount();
        
        var @event = new AccountCreated
        {
            AccountId = Guid.NewGuid(),
            AccountName = name,
            AccountType = type,
            InitialBalance = initialBalance.Amount,
            Currency = initialBalance.Currency,
            UserId = userId
        };
        
        account.RaiseEvent(@event);
        return account;
    }
    
    // Add transaction (command)
    public void AddTransaction(Money amount, DateTime date, string description, TransactionType type)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot add transactions to closed account");
        
        var newBalance = type == TransactionType.Debit 
            ? Balance.Subtract(amount)
            : Balance.Add(amount);
        
        var @event = new TransactionCreated
        {
            TransactionId = Guid.NewGuid(),
            AccountId = Id,
            Amount = amount.Amount,
            Currency = amount.Currency,
            TransactionType = type,
            TransactionDate = date,
            Description = description,
            AccountBalanceAfter = newBalance.Amount
        };
        
        RaiseEvent(@event);
    }
    
    // Reconcile account (command)
    public void Reconcile(DateTime reconciliationDate, Money statementBalance, List<Guid> transactionIds)
    {
        var calculatedBalance = CalculateBalanceAsOf(reconciliationDate);
        
        if (Math.Abs(calculatedBalance.Amount - statementBalance.Amount) > 0.01m)
        {
            // Create adjustment if needed
            var adjustment = statementBalance.Subtract(calculatedBalance);
            
            RaiseEvent(new AccountBalanceAdjusted
            {
                AccountId = Id,
                PreviousBalance = calculatedBalance.Amount,
                NewBalance = statementBalance.Amount,
                Reason = $"Reconciliation adjustment on {reconciliationDate:yyyy-MM-dd}",
                AdjustmentType = "reconciliation"
            });
        }
        
        RaiseEvent(new AccountReconciled
        {
            AccountId = Id,
            ReconciliationDate = reconciliationDate,
            StatementBalance = statementBalance.Amount,
            CalculatedBalance = calculatedBalance.Amount,
            ReconciledTransactionIds = transactionIds
        });
    }
    
    // Event Sourcing mechanics
    public void LoadFromHistory(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event, false);
            Version++;
        }
    }
    
    public IEnumerable<DomainEvent> GetUncommittedEvents() => _uncommittedEvents;
    
    public void MarkEventsAsCommitted() => _uncommittedEvents.Clear();
    
    private void RaiseEvent(DomainEvent @event)
    {
        ApplyEvent(@event, true);
        _uncommittedEvents.Add(@event);
    }
    
    private void ApplyEvent(DomainEvent @event, bool isNew)
    {
        if (_eventHandlers.TryGetValue(@event.GetType(), out var handler))
        {
            handler(@event);
        }
        
        if (isNew)
        {
            Version++;
        }
    }
    
    // Event handlers (state mutations)
    private void Apply(DomainEvent @event)
    {
        switch (@event)
        {
            case AccountCreated e:
                Id = e.AccountId;
                Name = e.AccountName;
                Type = e.AccountType;
                Balance = new Money(e.InitialBalance, e.Currency);
                IsActive = true;
                CreatedAt = e.OccurredAt;
                break;
                
            case TransactionCreated e:
                var transactionType = e.TransactionType;
                Balance = transactionType == TransactionType.Debit
                    ? Balance.Subtract(new Money(e.Amount, e.Currency))
                    : Balance.Add(new Money(e.Amount, e.Currency));
                
                _transactions.Add(new TransactionSnapshot
                {
                    TransactionId = e.TransactionId,
                    Amount = e.Amount,
                    Type = e.TransactionType,
                    Date = e.TransactionDate,
                    Description = e.Description,
                    BalanceAfter = e.AccountBalanceAfter
                });
                break;
                
            case AccountBalanceAdjusted e:
                Balance = new Money(e.NewBalance, Balance.Currency);
                break;
                
            case AccountClosed e:
                IsActive = false;
                ClosedAt = e.OccurredAt;
                break;
        }
    }
    
    private Money CalculateBalanceAsOf(DateTime date)
    {
        var relevantTransactions = _transactions.Where(t => t.Date <= date);
        var balance = new Money(0, Balance.Currency);
        
        foreach (var transaction in relevantTransactions)
        {
            var amount = new Money(transaction.Amount, Balance.Currency);
            balance = transaction.Type == TransactionType.Debit
                ? balance.Subtract(amount)
                : balance.Add(amount);
        }
        
        return balance;
    }
}

// ============================================
// AUDIT QUERY SERVICE
// ============================================

// Application/Services/AuditService.cs
namespace FinanceTracker.Application.Services;

public interface IAuditService
{
    Task<AuditTrail> GetAuditTrailAsync(Guid entityId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AuditEntry>> GetUserActivityAsync(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<AccountSnapshot> GetAccountSnapshotAsync(Guid accountId, DateTime pointInTime, CancellationToken cancellationToken = default);
    Task<IEnumerable<ChangeComparison>> CompareStatesAsync(Guid entityId, DateTime time1, DateTime time2, CancellationToken cancellationToken = default);
    Task<ComplianceReport> GenerateComplianceReportAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
}

public class AuditService : IAuditService
{
    private readonly IDbConnection _connection;
    private readonly IEventStore _eventStore;
    
    public AuditService(IDbConnection connection, IEventStore eventStore)
    {
        _connection = connection;
        _eventStore = eventStore;
    }
    
    public async Task<AuditTrail> GetAuditTrailAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                es.event_id,
                es.event_type,
                es.event_data,
                es.occurred_at,
                es.user_id,
                es.user_email,
                es.ip_address,
                es.user_agent,
                u.name as user_name
            FROM event_store es
            LEFT JOIN users u ON es.user_id = u.id
            WHERE es.aggregate_id = @EntityId
            ORDER BY es.occurred_at DESC";
        
        var events = await _connection.QueryAsync<AuditEntry>(sql, new { EntityId = entityId });
        
        return new AuditTrail
        {
            EntityId = entityId,
            Entries = events.Select(e => new AuditEntry
            {
                EventId = e.EventId,
                EventType = e.EventType,
                Description = GenerateHumanReadableDescription(e.EventType, e.EventData),
                OccurredAt = e.OccurredAt,
                UserId = e.UserId,
                UserName = e.UserName,
                UserEmail = e.UserEmail,
                IpAddress = e.IpAddress,
                Changes = ExtractChanges(e.EventType, e.EventData)
            }).ToList()
        };
    }
    
    public async Task<IEnumerable<AuditEntry>> GetUserActivityAsync(
        Guid userId, 
        DateTime from, 
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                es.*,
                CASE 
                    WHEN es.event_type LIKE '%Account%' THEN 'Account'
                    WHEN es.event_type LIKE '%Transaction%' THEN 'Transaction'
                    WHEN es.event_type LIKE '%Budget%' THEN 'Budget'
                    ELSE 'Other'
                END as entity_type,
                CASE
                    WHEN es.event_type LIKE '%Created' THEN 'Create'
                    WHEN es.event_type LIKE '%Modified' OR es.event_type LIKE '%Updated' THEN 'Update'
                    WHEN es.event_type LIKE '%Deleted' THEN 'Delete'
                    ELSE 'Other'
                END as action_type
            FROM event_store es
            WHERE es.user_id = @UserId
              AND es.occurred_at BETWEEN @From AND @To
            ORDER BY es.occurred_at DESC";
        
        return await _connection.QueryAsync<AuditEntry>(sql, new { UserId = userId, From = from, To = to });
    }
    
    public async Task<AccountSnapshot> GetAccountSnapshotAsync(
        Guid accountId, 
        DateTime pointInTime,
        CancellationToken cancellationToken = default)
    {
        // Rebuild account state at specific point in time
        var account = await _eventStore.GetAggregateAtPointInTimeAsync<EventSourcedAccount>(
            accountId, 
            pointInTime, 
            cancellationToken);
        
        if (account == null)
            throw new NotFoundException($"Account {accountId} not found or didn't exist at {pointInTime}");
        
        return new AccountSnapshot
        {
            AccountId = account.Id,
            SnapshotTime = pointInTime,
            Name = account.Name,
            Balance = account.Balance.Amount,
            Currency = account.Balance.Currency,
            TransactionCount = account.Transactions.Count,
            IsActive = account.IsActive,
            LastTransactionDate = account.Transactions.LastOrDefault()?.Date
        };
    }
    
    public async Task<IEnumerable<ChangeComparison>> CompareStatesAsync(
        Guid entityId,
        DateTime time1,
        DateTime time2,
        CancellationToken cancellationToken = default)
    {
        var account1 = await _eventStore.GetAggregateAtPointInTimeAsync<EventSourcedAccount>(
            entityId, time1, cancellationToken);
        
        var account2 = await _eventStore.GetAggregateAtPointInTimeAsync<EventSourcedAccount>(
            entityId, time2, cancellationToken);
        
        var changes = new List<ChangeComparison>();
        
        // Compare properties
        if (account1.Balance != account2.Balance)
        {
            changes.Add(new ChangeComparison
            {
                Property = "Balance",
                ValueAt1 = account1.Balance.ToString(),
                ValueAt2 = account2.Balance.ToString(),
                Time1 = time1,
                Time2 = time2
            });
        }
        
        if (account1.Name != account2.Name)
        {
            changes.Add(new ChangeComparison
            {
                Property = "Name",
                ValueAt1 = account1.Name,
                ValueAt2 = account2.Name,
                Time1 = time1,
                Time2 = time2
            });
        }
        
        var transactionDiff = account2.Transactions.Count - account1.Transactions.Count;
        if (transactionDiff != 0)
        {
            changes.Add(new ChangeComparison
            {
                Property = "Transaction Count",
                ValueAt1 = account1.Transactions.Count.ToString(),
                ValueAt2 = account2.Transactions.Count.ToString(),
                Time1 = time1,
                Time2 = time2
            });
        }
        
        return changes;
    }
    
    public async Task<ComplianceReport> GenerateComplianceReportAsync(
        DateTime from,
        DateTime to,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            WITH event_stats AS (
                SELECT 
                    DATE(occurred_at) as event_date,
                    event_type,
                    COUNT(*) as event_count,
                    COUNT(DISTINCT user_id) as unique_users,
                    COUNT(DISTINCT aggregate_id) as affected_entities
                FROM event_store
                WHERE occurred_at BETWEEN @From AND @To
                GROUP BY DATE(occurred_at), event_type
            ),
            sensitive_operations AS (
                SELECT 
                    event_type,
                    user_email,
                    COUNT(*) as operation_count
                FROM event_store
                WHERE occurred_at BETWEEN @From AND @To
                  AND event_type IN ('AccountDeleted', 'TransactionDeleted', 'AccountBalanceAdjusted')
                GROUP BY event_type, user_email
            ),
            data_modifications AS (
                SELECT 
                    aggregate_id,
                    COUNT(*) as modification_count,
                    MAX(occurred_at) as last_modified
                FROM event_store
                WHERE occurred_at BETWEEN @From AND @To
                  AND event_type LIKE '%Modified%'
                GROUP BY aggregate_id
                HAVING COUNT(*) > 5  -- Flag entities with many modifications
            )
            SELECT 
                (SELECT COUNT(DISTINCT event_id) FROM event_store WHERE occurred_at BETWEEN @From AND @To) as total_events,
                (SELECT COUNT(DISTINCT user_id) FROM event_store WHERE occurred_at BETWEEN @From AND @To) as active_users,
                (SELECT json_agg(row_to_json(event_stats)) FROM event_stats) as daily_activity,
                (SELECT json_agg(row_to_json(sensitive_operations)) FROM sensitive_operations) as sensitive_ops,
                (SELECT json_agg(row_to_json(data_modifications)) FROM data_modifications) as flagged_entities";
        
        var result = await _connection.QuerySingleAsync<dynamic>(sql, new { From = from, To = to });
        
        return new ComplianceReport
        {
            ReportPeriod = new DateRange { From = from, To = to },
            TotalEvents = result.total_events,
            ActiveUsers = result.active_users,
            DailyActivity = JsonSerializer.Deserialize<List<DailyActivity>>(result.daily_activity),
            SensitiveOperations = JsonSerializer.Deserialize<List<SensitiveOperation>>(result.sensitive_ops),
            FlaggedEntities = JsonSerializer.Deserialize<List<FlaggedEntity>>(result.flagged_entities),
            GeneratedAt = DateTime.UtcNow
        };
    }
    
    private string GenerateHumanReadableDescription(string eventType, string eventData)
    {
        // Parse event data and generate readable description
        return eventType switch
        {
            "AccountCreated" => "Account was created",
            "TransactionCreated" => "New transaction added",
            "AccountBalanceAdjusted" => "Account balance was manually adjusted",
            "AccountReconciled" => "Account was reconciled with bank statement",
            _ => $"Event: {eventType}"
        };
    }
    
    private Dictionary<string, object> ExtractChanges(string eventType, string eventData)
    {
        // Parse the JSON event data and extract relevant changes
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(eventData);
        
        // Filter to only show relevant fields based on event type
        return eventType switch
        {
            "TransactionModified" => data.Where(kvp => kvp.Key.StartsWith("Original") || kvp.Key.StartsWith("New"))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            _ => data
        };
    }
}

// ============================================
// GRAPHQL INTEGRATION
// ============================================

// GraphQL/Types/AuditTypes.cs
namespace FinanceTracker.GraphQL.Types;

public class AuditTrailType : ObjectType<AuditTrail>
{
    protected override void Configure(IObjectTypeDescriptor<AuditTrail> descriptor)
    {
        descriptor.Field(a => a.Entries)
            .UsePaging()
            .UseFiltering()
            .UseSorting();
    }
}

[ExtendObjectType("Query")]
public class AuditQueries
{
    [Authorize(Policy = "RequireAuditAccess")]
    public async Task<AuditTrail> GetAuditTrail(
        Guid entityId,
        [Service] IAuditService auditService)
    {
        return await auditService.GetAuditTrailAsync(entityId);
    }
    
    [Authorize(Policy = "RequireAuditAccess")]
    public async Task<AccountSnapshot> GetAccountSnapshot(
        Guid accountId,
        DateTime pointInTime,
        [Service] IAuditService auditService)
    {
        return await auditService.GetAccountSnapshotAsync(accountId, pointInTime);
    }
    
    [Authorize(Policy = "RequireComplianceAccess")]
    public async Task<ComplianceReport> GenerateComplianceReport(
        DateTime from,
        DateTime to,
        [Service] IAuditService auditService)
    {
        return await auditService.GenerateComplianceReportAsync(from, to);
    }
    
    public async Task<IEnumerable<ChangeComparison>> CompareAccountStates(
        Guid accountId,
        DateTime time1,
        DateTime time2,
        [Service] IAuditService auditService,
        [Service] ICurrentUserService currentUser)
    {
        // Verify user owns the account
        // ... validation logic ...
        
        return await auditService.CompareStatesAsync(accountId, time1, time2);
    }
}

// ============================================
// DATABASE SCHEMA
// ============================================

/*
-- Event Store table with comprehensive audit fields
CREATE TABLE event_store (
    sequence_number BIGSERIAL PRIMARY KEY,
    event_id UUID NOT NULL UNIQUE,
    aggregate_id UUID NOT NULL,
    event_type VARCHAR(255) NOT NULL,
    event_version INT NOT NULL DEFAULT 1,
    event_data JSONB NOT NULL,
    event_metadata JSONB,
    occurred_at TIMESTAMP WITH TIME ZONE NOT NULL,
    
    -- Audit fields
    user_id UUID,
    user_email VARCHAR(255),
    ip_address INET,
    user_agent TEXT,
    
    -- Indexing for queries
    INDEX idx_event_store_aggregate (aggregate_id, occurred_at),
    INDEX idx_event_store_type (event_type, occurred_at),
    INDEX idx_event_store_user (user_id, occurred_at),
    INDEX idx_event_store_date (occurred_at DESC),
    INDEX idx_event_store_event_id (event_id)
);

-- Snapshots table for performance optimization
CREATE TABLE event_snapshots (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    aggregate_id UUID NOT NULL,
    aggregate_type VARCHAR(255) NOT NULL,
    version INT NOT NULL,
    snapshot_data JSONB NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_snapshots_aggregate (aggregate_id, version DESC),
    UNIQUE(aggregate_id, version)
);

-- Audit read model for fast queries
CREATE MATERIALIZED VIEW audit_summary AS
SELECT 
    DATE(occurred_at) as audit_date,
    user_id,
    COUNT(*) as total_events,
    COUNT(DISTINCT aggregate_id) as entities_modified,
    array_agg(DISTINCT event_type) as event_types
FROM event_store
GROUP BY DATE(occurred_at), user_id;

CREATE INDEX idx_audit_summary_date ON audit_summary(audit_date DESC);
CREATE INDEX idx_audit_summary_user ON audit_summary(user_id);

-- Function to maintain audit integrity
CREATE OR REPLACE FUNCTION prevent_event_modification()
RETURNS TRIGGER AS $$
BEGIN
    RAISE EXCEPTION 'Event store records cannot be modified or deleted';
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER prevent_event_updates
    BEFORE UPDATE OR DELETE ON event_store
    FOR EACH ROW
    EXECUTE FUNCTION prevent_event_modification();
*/

// ============================================
// USAGE EXAMPLE
// ============================================

// In your command handler
public class CreateTransferCommandHandler : IRequestHandler<CreateTransferCommand, TransferResult>
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;
    
    public async Task<TransferResult> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
    {
        // Load event-sourced aggregates
        var fromAccount = await _eventStore.GetAggregateAsync<EventSourcedAccount>(
            request.FromAccountId, cancellationToken);
        
        var toAccount = await _eventStore.GetAggregateAsync<EventSourcedAccount>(
            request.ToAccountId, cancellationToken);
        
        // Execute business logic (generates events)
        var amount = new Money(request.Amount, request.Currency);
        fromAccount.AddTransaction(amount, request.Date, request.Description, TransactionType.Debit);
        toAccount.AddTransaction(amount, request.Date, request.Description, TransactionType.Credit);
        
        // Save events to event store
        var allEvents = fromAccount.GetUncommittedEvents()
            .Concat(toAccount.GetUncommittedEvents());
        
        await _eventStore.SaveEventsAsync(allEvents, cancellationToken);
        
        // Mark events as committed
        fromAccount.MarkEventsAsCommitted();
        toAccount.MarkEventsAsCommitted();
        
        // Publish events for projections/read models
        foreach (var @event in allEvents)
        {
            await _mediator.Publish(@event, cancellationToken);
        }
        
        return new TransferResult { Success = true };
    }
}
```

## Benefits of Event Sourcing for Finance Applications

### **Complete Audit Trail**

Every change is captured as an immutable event with full context:

- **Who**: User ID, email, IP address
- **What**: The exact change made
- **When**: Precise timestamp
- **Why**: Business context (reconciliation, adjustment, etc.)
- **How**: User agent, API endpoint used

### **Time Travel Capabilities**

You can reconstruct the exact state at any point in time:

```csharp
// "What was my account balance on December 31st for tax purposes?"
var snapshot = await auditService.GetAccountSnapshotAsync(
    accountId, 
    new DateTime(2024, 12, 31, 23, 59, 59));

// "Show me how this account changed between audits"
var changes = await auditService.CompareStatesAsync(
    accountId,
    lastAuditDate,
    currentAuditDate);
```

### **Compliance & Regulatory Benefits**

**Immutability**: Events cannot be modified or deleted (enforced by database triggers), ensuring data integrity for auditors.

**Traceability**: Every financial transaction can be traced back to its origin, including all modifications.

**Reporting**: Generate compliance reports showing all sensitive operations, unusual patterns, or high-frequency modifications.

### **Business Intelligence**

Event sourcing provides rich data for analytics:

- User behavior patterns
- Transaction velocity trends  
- Fraud detection (unusual event patterns)
- System usage analytics

### **Debugging & Support**

When users report issues, you can:

- Replay exact sequence of events
- Understand the precise state at the time of the problem
- Track down data inconsistencies to their source

### **Advanced Features Enabled**

**Retroactive Corrections**: Apply new business rules to historical data by replaying events.

**What-If Scenarios**: Test how different rules would have affected historical data.

**Compensating Transactions**: Reverse complex operations by understanding their full history.

### **Performance Considerations**

The architecture includes optimizations:

**Snapshots**: Periodic snapshots avoid replaying entire history

```sql
-- Take snapshot every 100 events
IF (SELECT COUNT(*) FROM event_store WHERE aggregate_id = @AggregateId) % 100 = 0
  INSERT INTO event_snapshots...
```

**Read Models**: Materialized views for common queries avoid event replay

```sql
-- Optimized view for current balances
CREATE MATERIALIZED VIEW current_account_balances AS
SELECT aggregate_id, 
       (snapshot_data->>'Balance')::decimal as balance
FROM event_snapshots
WHERE (aggregate_id, version) IN (
  SELECT aggregate_id, MAX(version) 
  FROM event_snapshots 
  GROUP BY aggregate_id
);
```

**CQRS Integration**: Commands write to event store, queries read from optimized projections.

### **GraphQL Benefits**

The event-sourced audit trail integrates beautifully with GraphQL:

```graphql
query AccountHistory {
  account(id: "...") {
    currentBalance
    
    # Historical snapshot
    snapshotAt(dateTime: "2024-12-31T23:59:59Z") {
      balance
      transactionCount
    }
    
    # Full audit trail
    auditTrail {
      entries {
        eventType
        occurredAt
        user { name email }
        changes
      }
    }
    
    # Compare two points in time
    comparison(
      from: "2024-01-01T00:00:00Z"
      to: "2024-12-31T23:59:59Z"
    ) {
      balanceChange
      transactionCount
    }
  }
}
```

### **Challenges & Solutions**

**Storage Growth**: Events accumulate over time.

- Solution: Archive old events to cold storage, keep recent events hot

**Query Performance**: Replaying events can be slow.

- Solution: Snapshots + read models + caching

**Complexity**: More complex than CRUD.

- Solution: Clear separation between write model (events) and read model (projections)

Event sourcing provides the financial-grade audit trail that regulators expect while enabling powerful features for both compliance and user experience. The immutable history becomes a valuable asset rather than just a compliance requirement.

**human** (Aug 29, 2025, 09:55 PM)

Is this a feature I could add on at a later date, or something that I would need to implement up front?

**assistant** (Aug 29, 2025, 09:57 PM)

 Great question! Event sourcing is one of those architectural decisions that's **significantly easier to implement upfront** but *can* be added later with careful planning. Let me break down both approaches:

## Starting with Event Sourcing (Recommended for Finance)

### Advantages

- **Clean architecture from day one** - no technical debt
- **Complete audit trail from the beginning** - no gaps in history
- **Simpler codebase** - one consistent pattern throughout
- **No migration complexity** - avoid risky data migrations later

### The Minimal Version

You can start with a "lite" version that captures events without full event sourcing:

#### create Hybrid Approach: Start Simple, Event-Source Ready

```csharp
// ============================================
// OPTION 1: AUDIT LOG PATTERN (Start Simple)
// ============================================
// This approach uses traditional CRUD with comprehensive audit logging
// Can be migrated to full event sourcing later

// Domain/Base/AuditableEntity.cs
namespace FinanceTracker.Domain.Base;

public abstract class AuditableEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public string? UpdatedBy { get; protected set; }
    
    // Store events that occurred during this operation
    private readonly List<DomainEvent> _domainEvents = new();
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents.Add(@event);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

// Infrastructure/Persistence/AuditLogInterceptor.cs
namespace FinanceTracker.Infrastructure.Persistence;

public class AuditLogInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuditLogInterceptor(
        ICurrentUserService currentUser,
        IHttpContextAccessor httpContextAccessor)
    {
        _currentUser = currentUser;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return result;
        
        var auditEntries = new List<AuditEntry>();
        
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is AuditableEntity entity)
            {
                var auditEntry = CreateAuditEntry(entry);
                if (auditEntry != null)
                    auditEntries.Add(auditEntry);
                
                // Update timestamps
                var now = DateTime.UtcNow;
                var userId = _currentUser.UserId?.ToString();
                
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = now;
                        entity.CreatedBy = userId;
                        entity.UpdatedAt = now;
                        entity.UpdatedBy = userId;
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = now;
                        entity.UpdatedBy = userId;
                        break;
                }
            }
        }
        
        // Save audit entries
        if (auditEntries.Any())
        {
            context.Set<AuditLog>().AddRange(auditEntries.Select(ae => new AuditLog
            {
                Id = Guid.NewGuid(),
                EntityType = ae.EntityType,
                EntityId = ae.EntityId,
                Action = ae.Action,
                OldValues = JsonSerializer.Serialize(ae.OldValues),
                NewValues = JsonSerializer.Serialize(ae.NewValues),
                ChangedProperties = JsonSerializer.Serialize(ae.ChangedProperties),
                UserId = _currentUser.UserId,
                UserEmail = _currentUser.Email,
                IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
                UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                Timestamp = DateTime.UtcNow
            }));
        }
        
        return result;
    }
    
    private AuditEntry? CreateAuditEntry(EntityEntry entry)
    {
        if (entry.State == EntityState.Unchanged || entry.State == EntityState.Detached)
            return null;
        
        var auditEntry = new AuditEntry
        {
            EntityType = entry.Entity.GetType().Name,
            EntityId = entry.Properties
                .FirstOrDefault(p => p.Metadata.IsPrimaryKey())?
                .CurrentValue?.ToString() ?? "",
            Action = entry.State.ToString()
        };
        
        foreach (var property in entry.Properties)
        {
            var propertyName = property.Metadata.Name;
            
            // Skip audit fields themselves
            if (propertyName.Contains("Created") || propertyName.Contains("Updated"))
                continue;
            
            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                    break;
                    
                case EntityState.Modified:
                    if (property.IsModified)
                    {
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        auditEntry.ChangedProperties.Add(propertyName);
                    }
                    break;
                    
                case EntityState.Deleted:
                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                    break;
            }
        }
        
        return auditEntry;
    }
}

// ============================================
// OPTION 2: DOMAIN EVENTS WITH STATE STORE
// ============================================
// This captures events but still uses traditional state storage
// Easier migration path to full event sourcing

// Domain/Aggregates/Account.cs
namespace FinanceTracker.Domain.Aggregates;

public class Account : AuditableEntity
{
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public Money Balance { get; private set; }
    public bool IsActive { get; private set; }
    
    // Still store state traditionally
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    
    public Account(string name, AccountType type, Money initialBalance, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Type = type;
        Balance = initialBalance;
        IsActive = true;
        
        // Capture the event for audit
        AddDomainEvent(new AccountCreated
        {
            AccountId = Id,
            AccountName = name,
            AccountType = type,
            InitialBalance = initialBalance.Amount,
            Currency = initialBalance.Currency,
            UserId = userId
        });
    }
    
    public void AddTransaction(Money amount, DateTime date, string description, TransactionType type)
    {
        var transaction = new Transaction(Id, amount, date, description, type);
        _transactions.Add(transaction);
        
        // Update state
        Balance = type == TransactionType.Debit 
            ? Balance.Subtract(amount)
            : Balance.Add(amount);
        
        // Capture event
        AddDomainEvent(new TransactionCreated
        {
            TransactionId = transaction.Id,
            AccountId = Id,
            Amount = amount.Amount,
            Currency = amount.Currency,
            TransactionType = type,
            TransactionDate = date,
            Description = description,
            AccountBalanceAfter = Balance.Amount
        });
    }
}

// Infrastructure/Persistence/DomainEventDispatcher.cs
namespace FinanceTracker.Infrastructure.Persistence;

public class DomainEventDispatcher : SaveChangesInterceptor
{
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return result;
        
        // Collect all domain events
        var domainEvents = context.ChangeTracker
            .Entries<AuditableEntity>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();
        
        // Save to event store (but not as source of truth yet)
        foreach (var @event in domainEvents)
        {
            await _eventStore.SaveEventAsync(@event, cancellationToken);
        }
        
        return result;
    }
    
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return result;
        
        // After successful save, publish events for projections
        var entities = context.ChangeTracker
            .Entries<AuditableEntity>()
            .Select(e => e.Entity)
            .ToList();
        
        foreach (var entity in entities)
        {
            foreach (var @event in entity.DomainEvents)
            {
                await _mediator.Publish(@event, cancellationToken);
            }
            entity.ClearDomainEvents();
        }
        
        return result;
    }
}

// ============================================
// MIGRATION PATH TO FULL EVENT SOURCING
// ============================================

// Step 1: Add event recording alongside current system
public class MigrationToEventSourcingService
{
    private readonly IDbConnection _connection;
    private readonly IEventStore _eventStore;
    
    public async Task Step1_BackfillHistoricalEvents()
    {
        // Create "genesis" events from current state
        const string sql = @"
            SELECT * FROM accounts 
            ORDER BY created_at";
        
        var accounts = await _connection.QueryAsync<Account>(sql);
        
        foreach (var account in accounts)
        {
            // Create synthetic historical events
            var createdEvent = new AccountCreated
            {
                AccountId = account.Id,
                AccountName = account.Name,
                AccountType = account.Type,
                InitialBalance = 0, // We don't know the original
                Currency = account.Currency,
                OccurredAt = account.CreatedAt,
                UserId = Guid.Parse(account.CreatedBy ?? Guid.Empty.ToString())
            };
            
            await _eventStore.SaveEventAsync(createdEvent);
            
            // Create events for all transactions
            var transactions = await GetTransactionsForAccount(account.Id);
            foreach (var transaction in transactions.OrderBy(t => t.CreatedAt))
            {
                var transactionEvent = new TransactionCreated
                {
                    TransactionId = transaction.Id,
                    AccountId = account.Id,
                    Amount = transaction.Amount,
                    TransactionDate = transaction.TransactionDate,
                    OccurredAt = transaction.CreatedAt
                    // ... other fields
                };
                
                await _eventStore.SaveEventAsync(transactionEvent);
            }
        }
    }
    
    public async Task Step2_RunDualWriteMode()
    {
        // For a period, write to both systems:
        // 1. Traditional tables (source of truth)
        // 2. Event store (building confidence)
        
        // This happens automatically with DomainEventDispatcher above
    }
    
    public async Task Step3_ValidateEventSourcedData()
    {
        // Compare event-sourced aggregates with traditional data
        const string sql = "SELECT id FROM accounts";
        var accountIds = await _connection.QueryAsync<Guid>(sql);
        
        var discrepancies = new List<string>();
        
        foreach (var accountId in accountIds)
        {
            // Load from traditional store
            var traditionalAccount = await GetTraditionalAccount(accountId);
            
            // Load from event store
            var eventSourcedAccount = await _eventStore
                .GetAggregateAsync<EventSourcedAccount>(accountId);
            
            // Compare
            if (traditionalAccount.Balance != eventSourcedAccount.Balance)
            {
                discrepancies.Add($"Account {accountId}: " +
                    $"Traditional balance: {traditionalAccount.Balance}, " +
                    $"Event-sourced balance: {eventSourcedAccount.Balance}");
            }
        }
        
        if (discrepancies.Any())
        {
            throw new InvalidOperationException(
                $"Validation failed: {string.Join("\n", discrepancies)}");
        }
    }
    
    public async Task Step4_SwitchToEventSourcing()
    {
        // After validation period:
        // 1. Make event store the source of truth
        // 2. Traditional tables become read models/projections
        // 3. Update repository implementations to use event store
    }
}

// ============================================
// RECOMMENDED: START WITH HYBRID APPROACH
// ============================================

// Program.cs - Configuration for hybrid approach
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add audit infrastructure from day one
        builder.Services.AddDbContext<FinanceContext>(options =>
        {
            options.UseNpgsql(connectionString);
            
            // Add interceptors for audit logging
            options.AddInterceptors(
                new AuditLogInterceptor(),
                new DomainEventDispatcher(),
                new SoftDeleteInterceptor() // Never hard delete financial data
            );
        });
        
        // Add event store (initially just for audit, not source of truth)
        builder.Services.AddSingleton<IEventStore, PostgresEventStore>();
        
        // Feature flag for event sourcing
        builder.Services.Configure<EventSourcingOptions>(options =>
        {
            options.Enabled = false; // Start with false, enable when ready
            options.DualWriteMode = true; // Write to both systems during transition
        });
        
        // Repository that can switch between implementations
        builder.Services.AddScoped<IAccountRepository>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<EventSourcingOptions>>();
            
            if (options.Value.Enabled)
            {
                return new EventSourcedAccountRepository(
                    provider.GetRequiredService<IEventStore>());
            }
            else
            {
                return new TraditionalAccountRepository(
                    provider.GetRequiredService<FinanceContext>());
            }
        });
    }
}

// ============================================
// DECISION FRAMEWORK
// ============================================

/*
START WITH EVENT SOURCING IF:
- You're in a regulated industry (finance, healthcare)
- Audit trail is a hard requirement
- You need to support complex financial reconciliation
- You have experience with event sourcing
- You're building from scratch

START WITH HYBRID APPROACH IF:
- You need to launch quickly
- Team is learning event sourcing
- Requirements might change significantly
- You want to validate the business model first
- You need to integrate with existing systems

START WITH TRADITIONAL CRUD IF:
- It's a simple expense tracker
- You have very limited time/resources
- Audit requirements are minimal
- You're building an MVP for validation

MIGRATION DIFFICULTY SCORE:
- CRUD → Audit Logs: Easy (1-2 sprints)
- Audit Logs → Domain Events: Moderate (2-4 sprints)
- Domain Events → Event Sourcing: Hard (1-2 months)
- CRUD → Event Sourcing: Very Hard (2-3 months)

KEY INSIGHT:
The hybrid approach (Domain Events with State Store) gives you:
1. Immediate audit trail capabilities
2. Clean domain model with events
3. Traditional database for queries
4. Smooth migration path to full event sourcing
5. Ability to "replay" history if needed

This is often the sweet spot for financial applications.
*/
```

## My Recommendation for Your Finance App

**Start with the Hybrid Approach** (Domain Events with State Store). Here's why:

### Benefits

1. **Immediate Compliance**: You get audit trails from day one
2. **Familiar Development**: Team can work with normal Entity Framework patterns
3. **Future-Proof**: Easy migration to full event sourcing if needed
4. **Fast Queries**: No event replay needed for normal operations
5. **Reversible**: Can abandon event sourcing without major refactoring

### What This Looks Like in Practice

**Day 1**: Your `Account` entity raises domain events, but you still update the balance field directly. Events are stored for audit only.

**Month 3**: You realize you need to show historical balances for tax reports. Good news - you have all the events! You can build this feature without changing your core architecture.

**Month 6**: Auditors love your immutable audit trail. You decide to gradually move some aggregates to full event sourcing.

**Year 2**: You've migrated critical financial aggregates to event sourcing while keeping simpler entities (like user profiles) in traditional CRUD.

### Critical Success Factors

1. **Design your domain events well from the start** - they're harder to change later
2. **Store enough context in events** - include balances, not just deltas
3. **Use soft deletes everywhere** - never lose financial data
4. **Version your events** - add a version field even if you don't use it yet

### The Minimum Viable Audit

At the very least, implement:

```csharp
// Every financial operation creates an immutable audit record
public class AuditLog
{
    public Guid Id { get; set; }
    public string EntityType { get; set; }
    public string EntityId { get; set; }
    public string Action { get; set; }
    public string OldValues { get; set; } // JSON
    public string NewValues { get; set; } // JSON
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
    public string IpAddress { get; set; }
}
```

This alone will save you during compliance audits and customer disputes.

### When to Commit to Full Event Sourcing

Consider full event sourcing when you need:

- **Time-travel queries** ("What was the balance on Dec 31?")
- **Complex reconciliation** (matching bank statements)
- **Retroactive corrections** (applying new tax rules to past data)
- **Multi-entity sagas** (complex transfer workflows)

The hybrid approach lets you defer this decision while keeping your options open. In finance, this flexibility is valuable - regulations change, requirements evolve, and having that complete history becomes increasingly valuable over time.

**human** (Aug 29, 2025, 10:00 PM)

So from the other features you mentioned, I think this would mostly be a personal budgeting application with these features you mentioned:

**Personal Finance:**
- Bill tracking and reminders
- Shared/family accounts
- Envelope budgeting
- Debt snowball/avalanche calculators

**9. Machine Learning & Intelligence**
Modern finance apps increasingly use ML:
- **Categorization**: Auto-categorizing transactions
- **Anomaly Detection**: Unusual spending patterns
- **Predictions**: Cash flow forecasting
- **Insights**: Spending trends, saving opportunities
**Questions for you:**
- Do you want automatic categorization?
- Should the app provide financial insights?

I think these both would be very relevant

**assistant** (Aug 29, 2025, 10:04 PM)

 Excellent! A personal budgeting app with family sharing and ML-powered insights is a great focus. Let me show you how these features would integrate into your architecture:

#### create Personal Finance Features with ML Integration

```csharp
// ============================================
// FAMILY/SHARED ACCOUNTS DOMAIN MODEL
// ============================================

// Domain/Entities/Household.cs
namespace FinanceTracker.Domain.Entities;

public class Household : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private readonly List<HouseholdMember> _members = new();
    public IReadOnlyCollection<HouseholdMember> Members => _members.AsReadOnly();
    
    private readonly List<SharedAccount> _sharedAccounts = new();
    public IReadOnlyCollection<SharedAccount> SharedAccounts => _sharedAccounts.AsReadOnly();
    
    private readonly List<SharedBudget> _sharedBudgets = new();
    public IReadOnlyCollection<SharedBudget> SharedBudgets => _sharedBudgets.AsReadOnly();
    
    public Household(string name, Guid createdByUserId)
    {
        Id = Guid.NewGuid();
        Name = name;
        CreatedByUserId = createdByUserId;
        CreatedAt = DateTime.UtcNow;
        
        // Creator is automatically an admin
        _members.Add(new HouseholdMember(createdByUserId, HouseholdRole.Admin));
        
        AddDomainEvent(new HouseholdCreatedEvent(Id, name, createdByUserId));
    }
    
    public void InviteMember(string email, HouseholdRole role = HouseholdRole.Member)
    {
        var invitation = new HouseholdInvitation(Id, email, role);
        AddDomainEvent(new MemberInvitedEvent(Id, email, role));
        return invitation;
    }
    
    public void ShareAccount(Guid accountId, AccountPermissions permissions)
    {
        if (!_members.Any(m => m.Role == HouseholdRole.Admin))
            throw new InvalidOperationException("Only admins can share accounts");
        
        _sharedAccounts.Add(new SharedAccount(accountId, permissions));
        AddDomainEvent(new AccountSharedEvent(Id, accountId, permissions));
    }
}

public class HouseholdMember
{
    public Guid UserId { get; private set; }
    public HouseholdRole Role { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public AccountPermissions DefaultPermissions { get; private set; }
    
    public HouseholdMember(Guid userId, HouseholdRole role)
    {
        UserId = userId;
        Role = role;
        JoinedAt = DateTime.UtcNow;
        DefaultPermissions = role == HouseholdRole.Admin 
            ? AccountPermissions.FullAccess 
            : AccountPermissions.ViewOnly;
    }
}

public enum HouseholdRole
{
    Admin,      // Can add/remove members, share accounts, modify all settings
    Parent,     // Can manage children's accounts and set limits
    Member,     // Can view and contribute to shared accounts
    Child,      // Limited access with parental controls
    ViewOnly    // Can only view shared information
}

[Flags]
public enum AccountPermissions
{
    None = 0,
    View = 1,
    AddTransactions = 2,
    EditTransactions = 4,
    DeleteTransactions = 8,
    ModifyBudgets = 16,
    ViewReports = 32,
    FullAccess = View | AddTransactions | EditTransactions | DeleteTransactions | ModifyBudgets | ViewReports
}

// ============================================
// ENVELOPE BUDGETING SYSTEM
// ============================================

// Domain/Aggregates/EnvelopeBudget.cs
namespace FinanceTracker.Domain.Aggregates;

public class EnvelopeBudget : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public BudgetPeriod Period { get; private set; }
    public DateTime StartDate { get; private set; }
    public bool IsActive { get; private set; }
    
    private readonly List<Envelope> _envelopes = new();
    public IReadOnlyCollection<Envelope> Envelopes => _envelopes.AsReadOnly();
    
    public Money TotalAllocated => _envelopes.Aggregate(
        Money.Zero("USD"), 
        (sum, env) => sum.Add(env.AllocatedAmount));
    
    public Money TotalRemaining => _envelopes.Aggregate(
        Money.Zero("USD"),
        (sum, env) => sum.Add(env.RemainingAmount));
    
    public EnvelopeBudget(Guid userId, string name, BudgetPeriod period)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Period = period;
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
    
    public Envelope CreateEnvelope(string name, Money allocatedAmount, EnvelopeCategory category)
    {
        var envelope = new Envelope(Id, name, allocatedAmount, category);
        _envelopes.Add(envelope);
        
        AddDomainEvent(new EnvelopeCreatedEvent(Id, envelope.Id, name, allocatedAmount));
        return envelope;
    }
    
    public void TransferBetweenEnvelopes(Guid fromEnvelopeId, Guid toEnvelopeId, Money amount)
    {
        var fromEnvelope = _envelopes.FirstOrDefault(e => e.Id == fromEnvelopeId);
        var toEnvelope = _envelopes.FirstOrDefault(e => e.Id == toEnvelopeId);
        
        if (fromEnvelope == null || toEnvelope == null)
            throw new InvalidOperationException("Envelope not found");
        
        fromEnvelope.Withdraw(amount);
        toEnvelope.Deposit(amount);
        
        AddDomainEvent(new EnvelopeTransferEvent(fromEnvelopeId, toEnvelopeId, amount));
    }
    
    public void AllocateTransaction(Guid transactionId, Guid envelopeId, Money amount)
    {
        var envelope = _envelopes.FirstOrDefault(e => e.Id == envelopeId);
        if (envelope == null)
            throw new InvalidOperationException("Envelope not found");
        
        envelope.AllocateTransaction(transactionId, amount);
        AddDomainEvent(new TransactionAllocatedToEnvelopeEvent(transactionId, envelopeId, amount));
    }
    
    public void RolloverUnspentFunds()
    {
        foreach (var envelope in _envelopes.Where(e => e.RolloverEnabled))
        {
            if (envelope.RemainingAmount.IsPositive)
            {
                envelope.Rollover();
                AddDomainEvent(new EnvelopeRolledOverEvent(
                    envelope.Id, 
                    envelope.RemainingAmount,
                    Period.GetNextPeriodStart(StartDate)));
            }
        }
    }
}

public class Envelope
{
    public Guid Id { get; private set; }
    public Guid BudgetId { get; private set; }
    public string Name { get; private set; }
    public Money AllocatedAmount { get; private set; }
    public Money SpentAmount { get; private set; }
    public Money RemainingAmount => AllocatedAmount.Subtract(SpentAmount);
    public EnvelopeCategory Category { get; private set; }
    public bool RolloverEnabled { get; private set; }
    public string? Color { get; private set; }
    public string? Icon { get; private set; }
    
    private readonly List<EnvelopeTransaction> _transactions = new();
    public IReadOnlyCollection<EnvelopeTransaction> Transactions => _transactions.AsReadOnly();
    
    public Envelope(Guid budgetId, string name, Money allocatedAmount, EnvelopeCategory category)
    {
        Id = Guid.NewGuid();
        BudgetId = budgetId;
        Name = name;
        AllocatedAmount = allocatedAmount;
        SpentAmount = Money.Zero(allocatedAmount.Currency);
        Category = category;
        RolloverEnabled = category != EnvelopeCategory.Fixed;
    }
    
    public void AllocateTransaction(Guid transactionId, Money amount)
    {
        if (amount.Amount > RemainingAmount.Amount)
            throw new InvalidOperationException($"Insufficient funds in envelope {Name}");
        
        SpentAmount = SpentAmount.Add(amount);
        _transactions.Add(new EnvelopeTransaction(transactionId, amount, DateTime.UtcNow));
    }
    
    public void Withdraw(Money amount)
    {
        if (amount.Amount > RemainingAmount.Amount)
            throw new InvalidOperationException($"Insufficient funds in envelope {Name}");
        
        AllocatedAmount = AllocatedAmount.Subtract(amount);
    }
    
    public void Deposit(Money amount)
    {
        AllocatedAmount = AllocatedAmount.Add(amount);
    }
}

public enum EnvelopeCategory
{
    Fixed,          // Rent, mortgage, insurance
    Variable,       // Groceries, gas, entertainment
    Savings,        // Emergency fund, vacation
    SinkingFund,    // Car maintenance, home repairs
    Discretionary   // Fun money, hobbies
}

// ============================================
// BILL TRACKING & REMINDERS
// ============================================

// Domain/Entities/Bill.cs
namespace FinanceTracker.Domain.Entities;

public class Bill : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public Money Amount { get; private set; }
    public BillFrequency Frequency { get; private set; }
    public DateTime NextDueDate { get; private set; }
    public Guid? PayeeId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Guid? LinkedAccountId { get; private set; }
    public bool AutoPay { get; private set; }
    public int ReminderDaysBefore { get; private set; }
    public bool IsActive { get; private set; }
    
    // Variable bills
    public bool IsVariableAmount { get; private set; }
    public Money? MinimumAmount { get; private set; }
    public Money? AverageAmount { get; private set; }
    
    private readonly List<BillPayment> _paymentHistory = new();
    public IReadOnlyCollection<BillPayment> PaymentHistory => _paymentHistory.AsReadOnly();
    
    public Bill(
        Guid userId,
        string name,
        Money amount,
        BillFrequency frequency,
        DateTime firstDueDate)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Amount = amount;
        Frequency = frequency;
        NextDueDate = firstDueDate;
        ReminderDaysBefore = 3;
        IsActive = true;
    }
    
    public void RecordPayment(Money amountPaid, DateTime paymentDate, Guid? transactionId = null)
    {
        var payment = new BillPayment(Id, amountPaid, paymentDate, transactionId);
        _paymentHistory.Add(payment);
        
        // Update average for variable bills
        if (IsVariableAmount)
        {
            var recentPayments = _paymentHistory
                .Where(p => p.PaymentDate >= DateTime.UtcNow.AddMonths(-6))
                .Select(p => p.Amount.Amount);
            
            if (recentPayments.Any())
            {
                AverageAmount = new Money(recentPayments.Average(), Amount.Currency);
            }
        }
        
        // Calculate next due date
        NextDueDate = Frequency.CalculateNextDueDate(NextDueDate);
        
        AddDomainEvent(new BillPaidEvent(Id, amountPaid, paymentDate, NextDueDate));
    }
    
    public bool IsDueSoon(int daysAhead = 7)
    {
        return NextDueDate <= DateTime.UtcNow.AddDays(daysAhead);
    }
    
    public bool IsOverdue()
    {
        return NextDueDate < DateTime.UtcNow;
    }
}

public class BillFrequency
{
    public FrequencyType Type { get; private set; }
    public int? DayOfMonth { get; private set; }
    public DayOfWeek? DayOfWeek { get; private set; }
    public int? IntervalDays { get; private set; }
    
    public static BillFrequency Monthly(int dayOfMonth) => new()
    {
        Type = FrequencyType.Monthly,
        DayOfMonth = dayOfMonth
    };
    
    public static BillFrequency Weekly(DayOfWeek dayOfWeek) => new()
    {
        Type = FrequencyType.Weekly,
        DayOfWeek = dayOfWeek
    };
    
    public DateTime CalculateNextDueDate(DateTime currentDueDate)
    {
        return Type switch
        {
            FrequencyType.Weekly => currentDueDate.AddDays(7),
            FrequencyType.Biweekly => currentDueDate.AddDays(14),
            FrequencyType.Monthly => currentDueDate.AddMonths(1),
            FrequencyType.Quarterly => currentDueDate.AddMonths(3),
            FrequencyType.Annually => currentDueDate.AddYears(1),
            FrequencyType.Custom => currentDueDate.AddDays(IntervalDays ?? 30),
            _ => currentDueDate.AddMonths(1)
        };
    }
}

// ============================================
// DEBT MANAGEMENT (SNOWBALL/AVALANCHE)
// ============================================

// Domain/Aggregates/DebtPayoffPlan.cs
namespace FinanceTracker.Domain.Aggregates;

public class DebtPayoffPlan : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public PayoffStrategy Strategy { get; private set; }
    public Money MonthlyPaymentBudget { get; private set; }
    public DateTime StartDate { get; private set; }
    public bool IsActive { get; private set; }
    
    private readonly List<Debt> _debts = new();
    public IReadOnlyCollection<Debt> Debts => _debts.AsReadOnly();
    
    public DebtPayoffPlan(
        Guid userId,
        string name,
        PayoffStrategy strategy,
        Money monthlyPaymentBudget)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Strategy = strategy;
        MonthlyPaymentBudget = monthlyPaymentBudget;
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
    
    public void AddDebt(
        string name,
        Money currentBalance,
        decimal interestRate,
        Money minimumPayment,
        Guid? linkedAccountId = null)
    {
        var debt = new Debt(
            Id,
            name,
            currentBalance,
            interestRate,
            minimumPayment,
            linkedAccountId);
        
        _debts.Add(debt);
        AddDomainEvent(new DebtAddedToPlanEvent(Id, debt.Id, name, currentBalance));
    }
    
    public PayoffProjection CalculatePayoffProjection()
    {
        var orderedDebts = OrderDebtsByStrategy();
        var projections = new List<DebtProjection>();
        var availableForExtra = MonthlyPaymentBudget.Subtract(GetTotalMinimumPayments());
        
        var currentDate = StartDate;
        var totalInterestPaid = Money.Zero(MonthlyPaymentBudget.Currency);
        
        foreach (var debt in orderedDebts)
        {
            var projection = CalculateDebtProjection(
                debt,
                availableForExtra,
                currentDate);
            
            projections.Add(projection);
            currentDate = projection.PayoffDate;
            totalInterestPaid = totalInterestPaid.Add(projection.TotalInterest);
            
            // After this debt is paid off, add its minimum payment to available extra
            availableForExtra = availableForExtra.Add(debt.MinimumPayment);
        }
        
        return new PayoffProjection
        {
            Strategy = Strategy,
            Projections = projections,
            TotalPayoffDate = projections.Max(p => p.PayoffDate),
            TotalInterestPaid = totalInterestPaid,
            MonthsToPayoff = CalculateMonthsBetween(StartDate, projections.Max(p => p.PayoffDate)),
            TotalPaid = projections.Sum(p => p.TotalPaid)
        };
    }
    
    private IEnumerable<Debt> OrderDebtsByStrategy()
    {
        return Strategy switch
        {
            PayoffStrategy.Snowball => _debts.OrderBy(d => d.CurrentBalance.Amount),
            PayoffStrategy.Avalanche => _debts.OrderByDescending(d => d.InterestRate),
            PayoffStrategy.Custom => _debts.OrderBy(d => d.Priority),
            _ => _debts
        };
    }
    
    private Money GetTotalMinimumPayments()
    {
        return _debts.Aggregate(
            Money.Zero(MonthlyPaymentBudget.Currency),
            (sum, debt) => sum.Add(debt.MinimumPayment));
    }
}

public class Debt
{
    public Guid Id { get; private set; }
    public Guid PlanId { get; private set; }
    public string Name { get; private set; }
    public Money OriginalBalance { get; private set; }
    public Money CurrentBalance { get; private set; }
    public decimal InterestRate { get; private set; }
    public Money MinimumPayment { get; private set; }
    public Guid? LinkedAccountId { get; private set; }
    public int Priority { get; set; }
    public bool IsPaidOff => CurrentBalance.Amount <= 0;
    
    public void RecordPayment(Money amount, Money interestPortion, Money principalPortion)
    {
        CurrentBalance = CurrentBalance.Subtract(principalPortion);
        
        if (CurrentBalance.Amount < 0)
            CurrentBalance = Money.Zero(CurrentBalance.Currency);
    }
}

public enum PayoffStrategy
{
    Snowball,   // Lowest balance first
    Avalanche,  // Highest interest rate first
    Custom      // User-defined order
}

// ============================================
// MACHINE LEARNING INTEGRATION
// ============================================

// Application/Services/TransactionCategorizationService.cs
namespace FinanceTracker.Application.Services;

public interface ITransactionCategorizationService
{
    Task<CategoryPrediction> PredictCategoryAsync(Transaction transaction);
    Task TrainModelAsync(IEnumerable<Transaction> transactions);
    Task<IEnumerable<CategorySuggestion>> GetCategorySuggestionsAsync(string description, Money amount);
}

public class TransactionCategorizationService : ITransactionCategorizationService
{
    private readonly IMLModelService _mlService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<TransactionCategorizationService> _logger;
    
    public async Task<CategoryPrediction> PredictCategoryAsync(Transaction transaction)
    {
        // Prepare features for ML model
        var features = new TransactionFeatures
        {
            Description = NormalizeDescription(transaction.Description),
            Amount = (float)transaction.Amount.Amount,
            DayOfWeek = (int)transaction.TransactionDate.DayOfWeek,
            DayOfMonth = transaction.TransactionDate.Day,
            IsWeekend = transaction.TransactionDate.DayOfWeek >= DayOfWeek.Saturday ? 1 : 0,
            MerchantName = ExtractMerchantName(transaction.Description),
            TransactionType = transaction.Type.ToString()
        };
        
        // Get prediction from ML model
        var prediction = await _mlService.PredictAsync<TransactionFeatures, CategoryPrediction>(
            "transaction_categorization_model",
            features);
        
        // If confidence is high enough, auto-categorize
        if (prediction.Confidence > 0.85)
        {
            await ApplyCategoryAsync(transaction, prediction.CategoryId);
            
            _logger.LogInformation(
                "Auto-categorized transaction {TransactionId} as {Category} with {Confidence:P} confidence",
                transaction.Id, prediction.CategoryName, prediction.Confidence);
        }
        
        return prediction;
    }
    
    public async Task TrainModelAsync(IEnumerable<Transaction> transactions)
    {
        // Prepare training data
        var trainingData = transactions
            .Where(t => t.CategoryId.HasValue) // Only use categorized transactions
            .Select(t => new TransactionTrainingData
            {
                Features = ExtractFeatures(t),
                Label = t.CategoryId.Value
            });
        
        // Train using ML.NET
        var mlContext = new MLContext(seed: 0);
        
        var pipeline = mlContext.Transforms.Text
            .FeaturizeText("DescriptionFeatures", nameof(TransactionFeatures.Description))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding("MerchantFeatures", nameof(TransactionFeatures.MerchantName)))
            .Append(mlContext.Transforms.Concatenate("Features",
                "DescriptionFeatures",
                "MerchantFeatures",
                nameof(TransactionFeatures.Amount),
                nameof(TransactionFeatures.DayOfWeek),
                nameof(TransactionFeatures.IsWeekend)))
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        
        var model = pipeline.Fit(trainingData);
        
        // Save model
        await _mlService.SaveModelAsync("transaction_categorization_model", model);
    }
    
    private string ExtractMerchantName(string description)
    {
        // Use regex patterns to extract merchant names
        // Example: "AMAZON.COM*1234 SEATTLE WA" -> "AMAZON"
        var patterns = new[]
        {
            @"^([A-Z\s]+?)(?:\*|\s{2,}|\d)",
            @"^([A-Z][A-Z\s&]+?)(?:\s+\d|$)",
            @"^(.+?)(?:\s+\d{2}/\d{2})"
        };
        
        foreach (var pattern in patterns)
        {
            var match = Regex.Match(description.ToUpper(), pattern);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }
        }
        
        return description.Split(' ')[0].ToUpper();
    }
}

// Application/Services/SpendingInsightsService.cs
namespace FinanceTracker.Application.Services;

public interface ISpendingInsightsService
{
    Task<IEnumerable<SpendingInsight>> GenerateInsightsAsync(Guid userId, DateTime from, DateTime to);
    Task<AnomalyDetectionResult> DetectAnomaliesAsync(Guid userId, DateTime from, DateTime to);
    Task<CashFlowForecast> ForecastCashFlowAsync(Guid userId, int daysAhead);
}

public class SpendingInsightsService : ISpendingInsightsService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMLModelService _mlService;
    
    public async Task<IEnumerable<SpendingInsight>> GenerateInsightsAsync(
        Guid userId, 
        DateTime from, 
        DateTime to)
    {
        var insights = new List<SpendingInsight>();
        var transactions = await _transactionRepository.GetByUserAndDateRangeAsync(userId, from, to);
        
        // Spending trend analysis
        var currentMonthSpending = transactions
            .Where(t => t.TransactionDate >= DateTime.UtcNow.AddDays(-30))
            .Sum(t => t.Amount.Amount);
        
        var previousMonthSpending = transactions
            .Where(t => t.TransactionDate >= DateTime.UtcNow.AddDays(-60) 
                    && t.TransactionDate < DateTime.UtcNow.AddDays(-30))
            .Sum(t => t.Amount.Amount);
        
        if (currentMonthSpending > previousMonthSpending * 1.2m)
        {
            insights.Add(new SpendingInsight
            {
                Type = InsightType.Warning,
                Title = "Spending Increase Detected",
                Description = $"Your spending this month is {((currentMonthSpending / previousMonthSpending - 1) * 100):F0}% higher than last month",
                Priority = InsightPriority.High,
                ActionableAdvice = "Review your recent transactions to identify areas where you can cut back"
            });
        }
        
        // Category analysis
        var categorySpending = transactions
            .Where(t => t.CategoryId.HasValue)
            .GroupBy(t => t.Category.Name)
            .Select(g => new
            {
                Category = g.Key,
                Total = g.Sum(t => t.Amount.Amount),
                Count = g.Count(),
                Average = g.Average(t => t.Amount.Amount)
            })
            .OrderByDescending(c => c.Total);
        
        var topCategory = categorySpending.FirstOrDefault();
        if (topCategory != null)
        {
            insights.Add(new SpendingInsight
            {
                Type = InsightType.Information,
                Title = $"Highest Spending Category: {topCategory.Category}",
                Description = $"You spent {topCategory.Total:C} on {topCategory.Category} across {topCategory.Count} transactions",
                Priority = InsightPriority.Medium,
                ActionableAdvice = $"Consider setting a budget for {topCategory.Category} to track this spending"
            });
        }
        
        // Subscription detection
        var recurringTransactions = await DetectRecurringTransactionsAsync(transactions);
        if (recurringTransactions.Any())
        {
            var totalSubscriptions = recurringTransactions.Sum(r => r.Amount);
            insights.Add(new SpendingInsight
            {
                Type = InsightType.Tip,
                Title = "Recurring Subscriptions Detected",
                Description = $"You have {recurringTransactions.Count()} recurring payments totaling {totalSubscriptions:C} per month",
                Priority = InsightPriority.Low,
                ActionableAdvice = "Review these subscriptions and cancel any you're not actively using",
                RelatedTransactions = recurringTransactions.Select(r => r.TransactionId).ToList()
            });
        }
        
        return insights;
    }
    
    public async Task<AnomalyDetectionResult> DetectAnomaliesAsync(
        Guid userId,
        DateTime from,
        DateTime to)
    {
        var transactions = await _transactionRepository.GetByUserAndDateRangeAsync(userId, from, to);
        
        // Prepare time series data
        var timeSeries = transactions
            .GroupBy(t => t.TransactionDate.Date)
            .Select(g => new TimeSeriesData
            {
                Timestamp = g.Key,
                Value = (float)g.Sum(t => t.Amount.Amount)
            })
            .OrderBy(t => t.Timestamp)
            .ToList();
        
        // Use ML.NET's anomaly detection
        var mlContext = new MLContext();
        var dataView = mlContext.Data.LoadFromEnumerable(timeSeries);
        
        var pipeline = mlContext.Transforms
            .DetectSpikeBySsa(
                outputColumnName: nameof(AnomalyPrediction.IsAnomaly),
                inputColumnName: nameof(TimeSeriesData.Value),
                confidence: 95,
                pvalueHistoryLength: 30,
                trainingWindowSize: 90,
                seasonalityWindowSize: 30);
        
        var model = pipeline.Fit(dataView);
        var predictions = model.Transform(dataView);
        
        var anomalies = mlContext.Data
            .CreateEnumerable<AnomalyPrediction>(predictions, reuseRowObject: false)
            .Where(p => p.IsAnomaly)
            .ToList();
        
        return new AnomalyDetectionResult
        {
            AnomaliesDetected = anomalies.Count,
            Anomalies = anomalies.Select(a => new SpendingAnomaly
            {
                Date = timeSeries[a.Index].Timestamp,
                Amount = timeSeries[a.Index].Value,
                Severity = CalculateSeverity(a.Score),
                Description = GenerateAnomalyDescription(a, timeSeries)
            }).ToList()
        };
    }
    
    public async Task<CashFlowForecast> ForecastCashFlowAsync(Guid userId, int daysAhead)
    {
        // Get historical data
        var historicalData = await _transactionRepository
            .GetByUserAndDateRangeAsync(
                userId,
                DateTime.UtcNow.AddMonths(-6),
                DateTime.UtcNow);
        
        // Separate income and expenses
        var dailyIncome = CalculateDailyAverages(
            historicalData.Where(t => t.Type == TransactionType.Credit));
        
        var dailyExpenses = CalculateDailyAverages(
            historicalData.Where(t => t.Type == TransactionType.Debit));
        
        // Get scheduled transactions
        var scheduledTransactions = await GetScheduledTransactionsAsync(userId, daysAhead);
        
        // Use time series forecasting
        var forecastModel = await _mlService.GetModelAsync<TimeSeriesForecastModel>("cashflow_forecast");
        
        var forecast = new CashFlowForecast
        {
            ForecastPeriod = new DateRange
            {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.AddDays(daysAhead)
            },
            PredictedDailyIncome = dailyIncome,
            PredictedDailyExpenses = dailyExpenses,
            ScheduledTransactions = scheduledTransactions,
            PredictedBalance = await CalculatePredictedBalanceAsync(
                userId,
                dailyIncome,
                dailyExpenses,
                scheduledTransactions,
                daysAhead),
            ConfidenceLevel = 0.85,
            Warnings = GenerateCashFlowWarnings(forecast)
        };
        
        return forecast;
    }
}

// ============================================
// GRAPHQL SCHEMA ADDITIONS
// ============================================

// GraphQL/Types/PersonalFinanceTypes.cs
namespace FinanceTracker.GraphQL.Types;

[ExtendObjectType("Query")]
public class PersonalFinanceQueries
{
    [UseDbContext(typeof(FinanceContext))]
    [UseProjection]
    public IQueryable<Household> GetMyHousehold(
        [Service] ICurrentUserService currentUser,
        [ScopedService] FinanceContext context)
    {
        return context.Households
            .Where(h => h.Members.Any(m => m.UserId == currentUser.UserId));
    }
    
    public async Task<EnvelopeBudget> GetActiveEnvelopeBudget(
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        return await mediator.Send(new GetActiveEnvelopeBudgetQuery(currentUser.UserId));
    }
    
    public async Task<IEnumerable<Bill>> GetUpcomingBills(
        int daysAhead,
        [Service] IBillService billService,
        [Service] ICurrentUserService currentUser)
    {
        return await billService.GetUpcomingBillsAsync(currentUser.UserId, daysAhead);
    }
    
    public async Task<PayoffProjection> CalculateDebtPayoff(
        PayoffStrategy strategy,
        decimal monthlyBudget,
        [Service] IDebtCalculationService debtService,
        [Service] ICurrentUserService currentUser)
    {
        return await debtService.CalculatePayoffProjectionAsync(
            currentUser.UserId,
            strategy,
            new Money(monthlyBudget, "USD"));
    }
    
    [Authorize]
    public async Task<IEnumerable<SpendingInsight>> GetSpendingInsights(
        DateTime? from,
        DateTime? to,
        [Service] ISpendingInsightsService insightsService,
        [Service] ICurrentUserService currentUser)
    {
        var fromDate = from ?? DateTime.UtcNow.AddMonths(-1);
        var toDate = to ?? DateTime.UtcNow;
        
        return await insightsService.GenerateInsightsAsync(
            currentUser.UserId,
            fromDate,
            toDate);
    }
    
    public async Task<CashFlowForecast> GetCashFlowForecast(
        int daysAhead,
        [Service] ISpendingInsightsService insightsService,
        [Service] ICurrentUserService currentUser)
    {
        return await insightsService.ForecastCashFlowAsync(
            currentUser.UserId,
            daysAhead);
    }
}

[ExtendObjectType("Mutation")]
public class PersonalFinanceMutations
{
    public async Task<Household> CreateHousehold(
        string name,
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        var command = new CreateHouseholdCommand(name, currentUser.UserId);
        return await mediator.Send(command);
    }
    
    public async Task<EnvelopeTransferResult> TransferBetweenEnvelopes(
        Guid fromEnvelopeId,
        Guid toEnvelopeId,
        decimal amount,
        [Service] IMediator mediator)
    {
        var command = new TransferBetweenEnvelopesCommand(
            fromEnvelopeId,
            toEnvelopeId,
            new Money(amount, "USD"));
        
        return await mediator.Send(command);
    }
    
    public async Task<Bill> CreateBill(
        CreateBillInput input,
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        var command = new CreateBillCommand(
            currentUser.UserId,
            input.Name,
            new Money(input.Amount, input.Currency ?? "USD"),
            input.Frequency,
            input.FirstDueDate);
        
        return await mediator.Send(command);
    }
    
    public async Task<DebtPayoffPlan> CreateDebtPayoffPlan(
        CreateDebtPlanInput input,
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        var command = new CreateDebtPayoffPlanCommand(
            currentUser.UserId,
            input.Name,
            input.Strategy,
            new Money(input.MonthlyBudget, "USD"),
            input.Debts);
        
        return await mediator.Send(command);
    }
}

[ExtendObjectType("Subscription")]
public class PersonalFinanceSubscriptions
{
    [Subscribe]
    [Topic("bill-reminder-{userId}")]
    public BillReminder OnBillReminder(
        [EventMessage] BillReminder reminder,
        [Service] ICurrentUserService currentUser)
    {
        return reminder;
    }
    
    [Subscribe]
    [Topic("spending-alert-{userId}")]
    public SpendingAlert OnSpendingAlert(
        [EventMessage] SpendingAlert alert,
        [Service] ICurrentUserService currentUser)
    {
        return alert;
    }
}
```

## Key Features for Personal Budgeting

### **1. Family/Household Management**

The household system allows:

- **Shared accounts** with granular permissions (view-only, add transactions, full access)
- **Role-based access** (Admin, Parent, Member, Child) for family dynamics
- **Parental controls** for children's spending
- **Unified reporting** across all household members

### **2. Envelope Budgeting**

Zero-based budgeting approach where every dollar has a job:

- **Virtual envelopes** for different spending categories
- **Real-time tracking** of remaining funds in each envelope
- **Transfer between envelopes** when priorities change
- **Rollover options** for unused funds
- **Visual indicators** (colors, icons) for quick recognition

### **3. Bill Management**

Never miss a payment:

- **Automatic reminders** via push notifications
- **Variable bill tracking** (utilities that fluctuate)
- **Payment history** for each bill
- **Auto-pay indicators**
- **Upcoming bills dashboard**

### **4. Debt Strategies**

Accelerate debt payoff:

- **Snowball method** (smallest balance first for psychological wins)
- **Avalanche method** (highest interest first for mathematical optimization)
- **Custom strategies** for specific situations
- **Visual progress tracking**
- **Payoff projections** with timelines

### **5. ML-Powered Intelligence**

**Auto-Categorization:**

- Learns from user behavior
- 85%+ confidence threshold for auto-apply
- Merchant name extraction
- Pattern recognition for recurring transactions

**Anomaly Detection:**

- Flags unusual spending patterns
- Severity scoring based on deviation
- Contextual alerts (e.g., "spending 3x normal at restaurants")

**Cash Flow Forecasting:**

- Predicts future balance based on patterns
- Incorporates scheduled transactions
- Warns about potential shortfalls
- Suggests optimal bill payment timing

**Spending Insights:**

- Trend analysis across categories
- Subscription detection and management
- Peer comparison (optional, anonymized)
- Actionable recommendations

## GraphQL Query Examples

```graphql
# Get comprehensive financial overview
query MyFinancialDashboard {
  myHousehold {
    members {
      name
      role
      monthlySpending
    }
    sharedAccounts {
      name
      balance
      permissions
    }
  }
  
  activeEnvelopeBudget {
    envelopes {
      name
      allocatedAmount
      remainingAmount
      percentageUsed
      category
      color
    }
  }
  
  upcomingBills(daysAhead: 14) {
    name
    amount
    dueDate
    isOverdue
    autoPay
  }
  
  spendingInsights {
    type
    title
    description
    actionableAdvice
    priority
  }
  
  cashFlowForecast(daysAhead: 30) {
    predictedBalance
    warnings
    scheduledTransactions {
      date
      amount
      description
    }
  }
}

# Calculate debt payoff scenarios
query DebtScenarios {
  snowballStrategy: calculateDebtPayoff(
    strategy: SNOWBALL
    monthlyBudget: 2000
  ) {
    totalPayoffDate
    totalInterestPaid
    monthsToPayoff
  }
  
  avalancheStrategy: calculateDebtPayoff(
    strategy: AVALANCHE
    monthlyBudget: 2000
  ) {
    totalPayoffDate
    totalInterestPaid
    monthsToPayoff
  }
}
```

## Implementation Priorities

Based on user value and complexity, I'd suggest this implementation order:

### **Phase 1: Core Features (MVP)**

1. Basic account/transaction management
2. Manual categorization
3. Simple budgets (no envelopes yet)
4. Basic reporting

### **Phase 2: Family Features**

1. Household creation and invites
2. Shared accounts with permissions
3. Unified reporting

### **Phase 3: Advanced Budgeting**

1. Envelope budgeting system
2. Bill tracking and reminders
3. Recurring transactions

### **Phase 4: Intelligence**

1. Auto-categorization with ML
2. Basic spending insights
3. Anomaly detection

### **Phase 5: Optimization**

1. Debt payoff calculators
2. Cash flow forecasting
3. Advanced insights and recommendations

The beauty of this architecture is that each phase builds naturally on the previous one, and the event-driven design means you can add ML features without disrupting existing functionality. The GraphQL API allows the frontend to evolve independently, requesting only the data it needs as new features are added.

**human** (Aug 29, 2025, 10:07 PM)

I would like to have more budgeting strategies available Envelope budgeting. WOuld that be a problem?

**assistant** (Aug 29, 2025, 10:10 PM)

 Not at all! Having multiple budgeting strategies is actually a great idea and the architecture can easily support it. Let me show you how to implement a flexible budgeting system that supports multiple strategies:

#### create Multiple Budgeting Strategies Architecture

```csharp
// ============================================
// FLEXIBLE BUDGETING STRATEGY SYSTEM
// ============================================

// Domain/Aggregates/Budget.cs
namespace FinanceTracker.Domain.Aggregates;

public abstract class Budget : AggregateRoot
{
    public Guid Id { get; protected set; }
    public Guid UserId { get; protected set; }
    public string Name { get; protected set; }
    public BudgetType Type { get; protected set; }
    public BudgetPeriod Period { get; protected set; }
    public DateTime StartDate { get; protected set; }
    public DateTime? EndDate { get; protected set; }
    public bool IsActive { get; protected set; }
    public Dictionary<string, object> Settings { get; protected set; }
    
    protected Budget()
    {
        Settings = new Dictionary<string, object>();
    }
    
    public abstract BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate);
    
    public abstract BudgetAllocation AllocateTransaction(
        Transaction transaction);
    
    public abstract IEnumerable<BudgetWarning> CheckBudgetHealth(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate);
    
    public abstract BudgetForecast GenerateForecast(
        IEnumerable<Transaction> historicalTransactions,
        int daysAhead);
}

public enum BudgetType
{
    Envelope,           // Zero-based, every dollar allocated
    Traditional,        // Category-based spending limits
    Percentage,         // 50/30/20 or custom percentages
    Priority,           // Pay Yourself First
    Rolling,            // Continuous average-based
    ValueBased,         // Align spending with personal values
    Hybrid              // Combination of strategies
}

// ============================================
// ENVELOPE BUDGETING (Zero-Based)
// ============================================

public class EnvelopeBudget : Budget
{
    private readonly List<Envelope> _envelopes = new();
    public IReadOnlyCollection<Envelope> Envelopes => _envelopes.AsReadOnly();
    
    public bool StrictMode { get; private set; } // Prevents overspending
    public bool AllowBorrowing { get; private set; } // Can borrow from other envelopes
    
    public EnvelopeBudget(
        Guid userId,
        string name,
        BudgetPeriod period,
        bool strictMode = true)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Envelope;
        Period = period;
        StartDate = DateTime.UtcNow;
        IsActive = true;
        StrictMode = strictMode;
        AllowBorrowing = !strictMode;
    }
    
    public override BudgetAllocation AllocateTransaction(Transaction transaction)
    {
        // Find matching envelope based on category or rules
        var envelope = FindBestEnvelope(transaction);
        
        if (envelope == null)
        {
            return BudgetAllocation.Unallocated(transaction.Id);
        }
        
        if (StrictMode && envelope.RemainingAmount.Amount < transaction.Amount.Amount)
        {
            return BudgetAllocation.Rejected(
                transaction.Id,
                $"Insufficient funds in envelope {envelope.Name}");
        }
        
        envelope.AllocateTransaction(transaction.Id, transaction.Amount);
        
        return BudgetAllocation.Allocated(
            transaction.Id,
            envelope.Id,
            envelope.RemainingAmount);
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        var performance = new EnvelopeBudgetPerformance
        {
            TotalAllocated = _envelopes.Sum(e => e.AllocatedAmount.Amount),
            TotalSpent = _envelopes.Sum(e => e.SpentAmount.Amount),
            TotalRemaining = _envelopes.Sum(e => e.RemainingAmount.Amount),
            EnvelopePerformances = _envelopes.Select(e => new EnvelopePerformance
            {
                EnvelopeId = e.Id,
                Name = e.Name,
                Allocated = e.AllocatedAmount,
                Spent = e.SpentAmount,
                Remaining = e.RemainingAmount,
                PercentageUsed = e.AllocatedAmount.Amount > 0 
                    ? (e.SpentAmount.Amount / e.AllocatedAmount.Amount) * 100 
                    : 0
            }).ToList()
        };
        
        return performance;
    }
}

// ============================================
// TRADITIONAL BUDGETING (Category Limits)
// ============================================

public class TraditionalBudget : Budget
{
    private readonly Dictionary<Guid, CategoryBudget> _categoryBudgets = new();
    public IReadOnlyDictionary<Guid, CategoryBudget> CategoryBudgets => _categoryBudgets;
    
    public bool TrackIncome { get; private set; }
    public Money? ExpectedIncome { get; private set; }
    
    public TraditionalBudget(
        Guid userId,
        string name,
        BudgetPeriod period,
        Money? expectedIncome = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Traditional;
        Period = period;
        StartDate = DateTime.UtcNow;
        IsActive = true;
        ExpectedIncome = expectedIncome;
        TrackIncome = expectedIncome != null;
    }
    
    public void SetCategoryBudget(Guid categoryId, Money amount, bool isFlexible = false)
    {
        _categoryBudgets[categoryId] = new CategoryBudget
        {
            CategoryId = categoryId,
            BudgetedAmount = amount,
            IsFlexible = isFlexible, // Flexible categories can go over budget
            WarningThreshold = 0.8m // Warn at 80% spent
        };
        
        AddDomainEvent(new CategoryBudgetSetEvent(Id, categoryId, amount));
    }
    
    public override BudgetAllocation AllocateTransaction(Transaction transaction)
    {
        if (!transaction.CategoryId.HasValue)
        {
            return BudgetAllocation.Unallocated(transaction.Id);
        }
        
        if (!_categoryBudgets.TryGetValue(transaction.CategoryId.Value, out var categoryBudget))
        {
            // No budget set for this category - track but don't restrict
            return BudgetAllocation.Tracked(transaction.Id, transaction.CategoryId.Value);
        }
        
        var currentSpending = GetCategorySpending(transaction.CategoryId.Value);
        var newTotal = currentSpending.Add(transaction.Amount);
        
        if (newTotal.Amount > categoryBudget.BudgetedAmount.Amount)
        {
            if (!categoryBudget.IsFlexible)
            {
                return BudgetAllocation.OverBudget(
                    transaction.Id,
                    transaction.CategoryId.Value,
                    newTotal.Subtract(categoryBudget.BudgetedAmount));
            }
        }
        
        return BudgetAllocation.Allocated(
            transaction.Id,
            transaction.CategoryId.Value,
            categoryBudget.BudgetedAmount.Subtract(newTotal));
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        var categorizedSpending = transactions
            .Where(t => t.CategoryId.HasValue && t.Type == TransactionType.Debit)
            .GroupBy(t => t.CategoryId.Value)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(t => t.Amount.Amount));
        
        return new TraditionalBudgetPerformance
        {
            Categories = _categoryBudgets.Select(cb => new CategoryPerformance
            {
                CategoryId = cb.Key,
                Budgeted = cb.Value.BudgetedAmount,
                Spent = new Money(categorizedSpending.GetValueOrDefault(cb.Key, 0), "USD"),
                Remaining = cb.Value.BudgetedAmount.Subtract(
                    new Money(categorizedSpending.GetValueOrDefault(cb.Key, 0), "USD")),
                IsOverBudget = categorizedSpending.GetValueOrDefault(cb.Key, 0) > cb.Value.BudgetedAmount.Amount
            }).ToList(),
            TotalBudgeted = _categoryBudgets.Sum(cb => cb.Value.BudgetedAmount.Amount),
            TotalSpent = categorizedSpending.Sum(cs => cs.Value),
            UncategorizedSpending = transactions
                .Where(t => !t.CategoryId.HasValue && t.Type == TransactionType.Debit)
                .Sum(t => t.Amount.Amount)
        };
    }
}

// ============================================
// PERCENTAGE-BASED BUDGETING (50/30/20, etc.)
// ============================================

public class PercentageBudget : Budget
{
    private readonly Dictionary<string, BudgetPercentageRule> _rules = new();
    public IReadOnlyDictionary<string, BudgetPercentageRule> Rules => _rules;
    
    public Money MonthlyIncome { get; private set; }
    public PercentageStrategy Strategy { get; private set; }
    
    public PercentageBudget(
        Guid userId,
        string name,
        Money monthlyIncome,
        PercentageStrategy strategy = PercentageStrategy.Standard_50_30_20)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Percentage;
        Period = BudgetPeriod.Monthly;
        StartDate = DateTime.UtcNow;
        IsActive = true;
        MonthlyIncome = monthlyIncome;
        Strategy = strategy;
        
        InitializeRules(strategy);
    }
    
    private void InitializeRules(PercentageStrategy strategy)
    {
        switch (strategy)
        {
            case PercentageStrategy.Standard_50_30_20:
                _rules["Needs"] = new BudgetPercentageRule
                {
                    Name = "Needs",
                    Percentage = 50,
                    Categories = new[] { "Housing", "Utilities", "Groceries", "Insurance", "Transportation" },
                    Description = "Essential expenses"
                };
                _rules["Wants"] = new BudgetPercentageRule
                {
                    Name = "Wants",
                    Percentage = 30,
                    Categories = new[] { "Entertainment", "Dining Out", "Hobbies", "Shopping" },
                    Description = "Discretionary spending"
                };
                _rules["Savings"] = new BudgetPercentageRule
                {
                    Name = "Savings & Debt",
                    Percentage = 20,
                    Categories = new[] { "Emergency Fund", "Investments", "Debt Payment" },
                    Description = "Financial goals"
                };
                break;
                
            case PercentageStrategy.Aggressive_30_20_50:
                _rules["Needs"] = new BudgetPercentageRule { Name = "Needs", Percentage = 30 };
                _rules["Wants"] = new BudgetPercentageRule { Name = "Wants", Percentage = 20 };
                _rules["Savings"] = new BudgetPercentageRule { Name = "Savings", Percentage = 50 };
                break;
                
            case PercentageStrategy.Balanced_40_30_30:
                _rules["Needs"] = new BudgetPercentageRule { Name = "Needs", Percentage = 40 };
                _rules["Wants"] = new BudgetPercentageRule { Name = "Wants", Percentage = 30 };
                _rules["Savings"] = new BudgetPercentageRule { Name = "Savings", Percentage = 30 };
                break;
                
            case PercentageStrategy.Custom:
                // User defines their own percentages
                break;
        }
    }
    
    public void SetCustomRule(string name, decimal percentage, string[] categories)
    {
        if (_rules.Sum(r => r.Value.Percentage) + percentage > 100)
        {
            throw new InvalidOperationException("Total percentages cannot exceed 100%");
        }
        
        _rules[name] = new BudgetPercentageRule
        {
            Name = name,
            Percentage = percentage,
            Categories = categories
        };
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        var performance = new PercentageBudgetPerformance
        {
            MonthlyIncome = MonthlyIncome,
            Rules = new List<RulePerformance>()
        };
        
        foreach (var rule in _rules.Values)
        {
            var budgetedAmount = MonthlyIncome.Amount * (rule.Percentage / 100);
            var spent = transactions
                .Where(t => rule.Categories.Contains(t.Category?.Name ?? "") 
                       && t.Type == TransactionType.Debit)
                .Sum(t => t.Amount.Amount);
            
            performance.Rules.Add(new RulePerformance
            {
                RuleName = rule.Name,
                Percentage = rule.Percentage,
                BudgetedAmount = new Money(budgetedAmount, MonthlyIncome.Currency),
                SpentAmount = new Money(spent, MonthlyIncome.Currency),
                RemainingAmount = new Money(budgetedAmount - spent, MonthlyIncome.Currency),
                PercentageOfIncomeUsed = (spent / MonthlyIncome.Amount) * 100
            });
        }
        
        return performance;
    }
}

public enum PercentageStrategy
{
    Standard_50_30_20,      // Traditional balanced approach
    Aggressive_30_20_50,    // High savings rate
    Balanced_40_30_30,      // Equal wants and savings
    Debt_Focus_35_5_60,     // Minimal wants, maximum debt payment
    FIRE_25_5_70,          // Financial Independence Retire Early
    Custom                  // User-defined percentages
}

// ============================================
// PRIORITY-BASED BUDGETING (Pay Yourself First)
// ============================================

public class PriorityBudget : Budget
{
    private readonly List<BudgetPriority> _priorities = new();
    public IReadOnlyCollection<BudgetPriority> Priorities => _priorities.AsReadOnly();
    
    public Money MonthlyIncome { get; private set; }
    public bool AutoAllocate { get; private set; }
    
    public PriorityBudget(
        Guid userId,
        string name,
        Money monthlyIncome,
        bool autoAllocate = true)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Priority;
        Period = BudgetPeriod.Monthly;
        MonthlyIncome = monthlyIncome;
        AutoAllocate = autoAllocate;
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
    
    public void AddPriority(
        string name,
        Money amount,
        int order,
        PriorityType type,
        bool isFixed = true)
    {
        _priorities.Add(new BudgetPriority
        {
            Id = Guid.NewGuid(),
            Name = name,
            Amount = amount,
            Order = order,
            Type = type,
            IsFixed = isFixed,
            IsFunded = false
        });
        
        // Re-sort priorities
        _priorities.Sort((a, b) => a.Order.CompareTo(b.Order));
    }
    
    public AllocationPlan GenerateAllocationPlan()
    {
        var plan = new AllocationPlan
        {
            TotalIncome = MonthlyIncome,
            Allocations = new List<PlannedAllocation>()
        };
        
        var remaining = MonthlyIncome.Amount;
        
        foreach (var priority in _priorities.OrderBy(p => p.Order))
        {
            var allocation = Math.Min(priority.Amount.Amount, remaining);
            
            plan.Allocations.Add(new PlannedAllocation
            {
                PriorityId = priority.Id,
                Name = priority.Name,
                PlannedAmount = priority.Amount,
                AllocatedAmount = new Money(allocation, MonthlyIncome.Currency),
                IsFullyFunded = allocation >= priority.Amount.Amount,
                Order = priority.Order
            });
            
            remaining -= allocation;
            
            if (remaining <= 0)
                break;
        }
        
        plan.UnallocatedAmount = new Money(remaining, MonthlyIncome.Currency);
        return plan;
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        var plan = GenerateAllocationPlan();
        
        return new PriorityBudgetPerformance
        {
            Priorities = _priorities.Select(p =>
            {
                var allocated = plan.Allocations
                    .FirstOrDefault(a => a.PriorityId == p.Id)?
                    .AllocatedAmount ?? Money.Zero(MonthlyIncome.Currency);
                
                var spent = CalculateSpentForPriority(p, transactions);
                
                return new PriorityPerformance
                {
                    Priority = p,
                    AllocatedAmount = allocated,
                    SpentAmount = spent,
                    IsFullyFunded = allocated.Amount >= p.Amount.Amount,
                    FundingPercentage = p.Amount.Amount > 0 
                        ? (allocated.Amount / p.Amount.Amount) * 100 
                        : 100
                };
            }).ToList()
        };
    }
}

public class BudgetPriority
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Money Amount { get; set; }
    public int Order { get; set; }
    public PriorityType Type { get; set; }
    public bool IsFixed { get; set; }
    public bool IsFunded { get; set; }
    public List<Guid> LinkedCategories { get; set; } = new();
    public Guid? LinkedAccountId { get; set; } // For automatic transfers
}

public enum PriorityType
{
    Savings,            // Emergency fund, retirement
    DebtPayment,        // Extra debt payments
    Investment,         // Brokerage, crypto
    Goal,              // Vacation, large purchase
    FixedExpense,      // Rent, insurance
    VariableExpense    // Groceries, utilities
}

// ============================================
// ROLLING BUDGET (Continuous Average)
// ============================================

public class RollingBudget : Budget
{
    public int RollingPeriodDays { get; private set; }
    public Dictionary<Guid, RollingAverage> CategoryAverages { get; private set; }
    public decimal VarianceThreshold { get; private set; } // Alert if spending varies by this %
    
    public RollingBudget(
        Guid userId,
        string name,
        int rollingPeriodDays = 90,
        decimal varianceThreshold = 0.2m)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Rolling;
        RollingPeriodDays = rollingPeriodDays;
        VarianceThreshold = varianceThreshold;
        CategoryAverages = new Dictionary<Guid, RollingAverage>();
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
    
    public void UpdateRollingAverages(IEnumerable<Transaction> transactions)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-RollingPeriodDays);
        var recentTransactions = transactions.Where(t => t.TransactionDate >= cutoffDate);
        
        var categoryGroups = recentTransactions
            .Where(t => t.CategoryId.HasValue)
            .GroupBy(t => t.CategoryId.Value);
        
        foreach (var group in categoryGroups)
        {
            var dailyAverage = group.Sum(t => t.Amount.Amount) / RollingPeriodDays;
            var standardDeviation = CalculateStandardDeviation(group.Select(t => t.Amount.Amount));
            
            CategoryAverages[group.Key] = new RollingAverage
            {
                CategoryId = group.Key,
                DailyAverage = dailyAverage,
                MonthlyProjection = dailyAverage * 30,
                StandardDeviation = standardDeviation,
                LastUpdated = DateTime.UtcNow,
                TransactionCount = group.Count()
            };
        }
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        UpdateRollingAverages(transactions);
        
        var currentPeriodStart = asOfDate.AddDays(-30);
        var currentPeriodTransactions = transactions
            .Where(t => t.TransactionDate >= currentPeriodStart && t.TransactionDate <= asOfDate);
        
        return new RollingBudgetPerformance
        {
            RollingPeriodDays = RollingPeriodDays,
            Categories = CategoryAverages.Select(ca =>
            {
                var currentSpending = currentPeriodTransactions
                    .Where(t => t.CategoryId == ca.Key)
                    .Sum(t => t.Amount.Amount);
                
                var variance = ca.Value.MonthlyProjection > 0
                    ? (currentSpending - ca.Value.MonthlyProjection) / ca.Value.MonthlyProjection
                    : 0;
                
                return new RollingCategoryPerformance
                {
                    CategoryId = ca.Key,
                    RollingAverage = ca.Value,
                    CurrentPeriodSpending = currentSpending,
                    Variance = variance,
                    IsAnomaly = Math.Abs(variance) > VarianceThreshold
                };
            }).ToList()
        };
    }
}

// ============================================
// VALUE-BASED BUDGETING
// ============================================

public class ValueBasedBudget : Budget
{
    private readonly List<PersonalValue> _values = new();
    public IReadOnlyCollection<PersonalValue> Values => _values.AsReadOnly();
    
    public Money MonthlyBudget { get; private set; }
    
    public ValueBasedBudget(
        Guid userId,
        string name,
        Money monthlyBudget)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.ValueBased;
        MonthlyBudget = monthlyBudget;
        Period = BudgetPeriod.Monthly;
        StartDate = DateTime.UtcNow;
        IsActive = true;
    }
    
    public void AddValue(
        string name,
        string description,
        int importance, // 1-10 scale
        decimal targetPercentage)
    {
        _values.Add(new PersonalValue
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Importance = importance,
            TargetPercentage = targetPercentage,
            LinkedCategories = new List<Guid>()
        });
        
        // Re-normalize percentages if they exceed 100%
        NormalizeValuePercentages();
    }
    
    public void LinkCategoryToValue(Guid categoryId, Guid valueId, decimal weight = 1.0m)
    {
        var value = _values.FirstOrDefault(v => v.Id == valueId);
        if (value != null)
        {
            value.LinkedCategories.Add(categoryId);
            value.CategoryWeights[categoryId] = weight;
        }
    }
    
    public ValueAlignment AnalyzeValueAlignment(IEnumerable<Transaction> transactions)
    {
        var totalSpending = transactions
            .Where(t => t.Type == TransactionType.Debit)
            .Sum(t => t.Amount.Amount);
        
        var alignment = new ValueAlignment
        {
            Values = new List<ValueScore>()
        };
        
        foreach (var value in _values)
        {
            var valueSpending = transactions
                .Where(t => value.LinkedCategories.Contains(t.CategoryId ?? Guid.Empty))
                .Sum(t => t.Amount.Amount * value.CategoryWeights.GetValueOrDefault(t.CategoryId ?? Guid.Empty, 1));
            
            var actualPercentage = totalSpending > 0 ? (valueSpending / totalSpending) * 100 : 0;
            
            alignment.Values.Add(new ValueScore
            {
                Value = value,
                TargetPercentage = value.TargetPercentage,
                ActualPercentage = actualPercentage,
                AlignmentScore = CalculateAlignmentScore(value.TargetPercentage, actualPercentage),
                SpendingAmount = new Money(valueSpending, "USD")
            });
        }
        
        alignment.OverallAlignmentScore = alignment.Values.Average(v => v.AlignmentScore);
        
        return alignment;
    }
    
    private decimal CalculateAlignmentScore(decimal target, decimal actual)
    {
        // Returns a score from 0-100 based on how close actual is to target
        var difference = Math.Abs(target - actual);
        var maxDifference = Math.Max(target, 100 - target);
        return Math.Max(0, 100 - (difference / maxDifference * 100));
    }
}

public class PersonalValue
{
    public Guid Id { get; set; }
    public string Name { get; set; } // "Family", "Health", "Growth", "Security"
    public string Description { get; set; }
    public int Importance { get; set; } // 1-10 scale
    public decimal TargetPercentage { get; set; }
    public List<Guid> LinkedCategories { get; set; } = new();
    public Dictionary<Guid, decimal> CategoryWeights { get; set; } = new();
}

// ============================================
// HYBRID BUDGETING (Combine Multiple Strategies)
// ============================================

public class HybridBudget : Budget
{
    private readonly List<Budget> _componentBudgets = new();
    public IReadOnlyCollection<Budget> ComponentBudgets => _componentBudgets.AsReadOnly();
    
    public Dictionary<BudgetType, decimal> StrategyWeights { get; private set; }
    
    public HybridBudget(
        Guid userId,
        string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Type = BudgetType.Hybrid;
        StartDate = DateTime.UtcNow;
        IsActive = true;
        StrategyWeights = new Dictionary<BudgetType, decimal>();
    }
    
    public void AddComponentBudget(Budget budget, decimal weight = 1.0m)
    {
        _componentBudgets.Add(budget);
        StrategyWeights[budget.Type] = weight;
        
        AddDomainEvent(new ComponentBudgetAddedEvent(Id, budget.Id, budget.Type, weight));
    }
    
    public override BudgetAllocation AllocateTransaction(Transaction transaction)
    {
        // Get allocation recommendations from each component
        var allocations = _componentBudgets
            .Select(b => new
            {
                Budget = b,
                Allocation = b.AllocateTransaction(transaction),
                Weight = StrategyWeights[b.Type]
            })
            .ToList();
        
        // Weighted voting system for final allocation
        var finalAllocation = DetermineConsensusAllocation(allocations);
        
        return finalAllocation;
    }
    
    public override BudgetPerformance CalculatePerformance(
        IEnumerable<Transaction> transactions,
        DateTime asOfDate)
    {
        var componentPerformances = _componentBudgets
            .Select(b => new
            {
                Type = b.Type,
                Performance = b.CalculatePerformance(transactions, asOfDate),
                Weight = StrategyWeights[b.Type]
            })
            .ToList();
        
        return new HybridBudgetPerformance
        {
            ComponentPerformances = componentPerformances.ToDictionary(
                cp => cp.Type,
                cp => cp.Performance),
            OverallScore = CalculateWeightedScore(componentPerformances)
        };
    }
}

// ============================================
// GRAPHQL INTEGRATION
// ============================================

[ExtendObjectType("Query")]
public class BudgetingQueries
{
    public async Task<IEnumerable<Budget>> GetMyBudgets(
        [Service] IBudgetRepository budgetRepository,
        [Service] ICurrentUserService currentUser)
    {
        return await budgetRepository.GetUserBudgetsAsync(currentUser.UserId);
    }
    
    public async Task<BudgetComparison> CompareBudgetStrategies(
        Money monthlyIncome,
        [Service] IBudgetComparisonService comparisonService,
        [Service] ICurrentUserService currentUser)
    {
        return await comparisonService.CompareStrategiesAsync(
            currentUser.UserId,
            monthlyIncome);
    }
    
    public async Task<BudgetRecommendation> GetBudgetRecommendation(
        [Service] IBudgetRecommendationService recommendationService,
        [Service] ICurrentUserService currentUser)
    {
        return await recommendationService.RecommendStrategyAsync(currentUser.UserId);
    }
}

[ExtendObjectType("Mutation")]
public class BudgetingMutations
{
    public async Task<Budget> CreateBudget(
        CreateBudgetInput input,
        [Service] IBudgetFactory budgetFactory,
        [Service] IMediator mediator,
        [Service] ICurrentUserService currentUser)
    {
        // Factory pattern to create the right budget type
        var budget = budgetFactory.CreateBudget(
            input.Type,
            currentUser.UserId,
            input.Name,
            input.Settings);
        
        var command = new CreateBudgetCommand(budget);
        return await mediator.Send(command);
    }
    
    public async Task<bool> SwitchBudgetStrategy(
        Guid currentBudgetId,
        BudgetType newType,
        [Service] IBudgetMigrationService migrationService,
        [Service] ICurrentUserService currentUser)
    {
        return await migrationService.MigrateBudgetAsync(
            currentBudgetId,
            newType,
            currentUser.UserId);
    }
}
```

## Multiple Budgeting Strategies - Overview

This architecture provides **7 different budgeting strategies** that users can choose from or even combine:

### **1. Envelope Budgeting** (Zero-Based)

- Every dollar allocated to a specific envelope
- Strict or flexible modes
- Best for: People who want complete control

### **2. Traditional Budgeting** (Category Limits)

- Set spending limits per category
- Track but don't restrict unbudgeted categories
- Best for: Simple, straightforward budgeting

### **3. Percentage-Based** (50/30/20, etc.)

- Allocate income by percentages
- Multiple preset strategies (standard, aggressive, FIRE)
- Best for: Beginners or those wanting guidelines

### **4. Priority-Based** (Pay Yourself First)

- Fund priorities in order of importance
- Automatic allocation planning
- Best for: Goal-oriented savers

### **5. Rolling Budget** (Continuous Average)

- Based on rolling averages (e.g., last 90 days)
- Detects spending anomalies automatically
- Best for: Variable income or irregular expenses

### **6. Value-Based Budgeting**

- Align spending with personal values
- Track how well spending matches priorities
- Best for: Mindful, intentional spending

### **7. Hybrid Budgeting**

- Combine multiple strategies
- Weighted decision making
- Best for: Advanced users with complex needs

## Key Advantages of This Design

### **Flexibility**

Users can:

- Start with one strategy and switch later
- Run multiple budgets simultaneously
- Migrate between strategies with data preservation

### **Personalization**

Each strategy can be customized:

- 50/30/20 can become 40/40/20
- Envelope budgets can allow borrowing between envelopes
- Priority budgets can have custom priority levels

### **Intelligence**

The system can:

- Recommend strategies based on spending patterns
- Compare how different strategies would perform
- Automatically detect which strategy fits best

## GraphQL Usage Examples

```graphql
# Get budget recommendations based on my spending
query GetBudgetRecommendation {
  budgetRecommendation {
    recommendedStrategy
    reasoning
    projectedSavings
    confidenceScore
  }
}

# Compare different strategies
query CompareBudgets {
  compareBudgetStrategies(monthlyIncome: 5000) {
    strategies {
      type
      projectedMonthlySavings
      difficultyLevel
      flexibilityScore
      bestFor
    }
  }
}

# Create a percentage-based budget
mutation CreatePercentageBudget {
  createBudget(input: {
    type: PERCENTAGE
    name: "My 50/30/20 Budget"
    settings: {
      monthlyIncome: 5000
      strategy: "Standard_50_30_20"
    }
  }) {
    id
    rules {
      name
      percentage
      budgetedAmount
    }
  }
}

# Switch strategies mid-month
mutation SwitchStrategy {
  switchBudgetStrategy(
    currentBudgetId: "..."
    newType: PRIORITY
  ) {
    success
    migratedData {
      categories
      allocations
    }
  }
}
```

## Implementation Strategy

### **Phase 1: Core Infrastructure**

Build the abstract `Budget` base class and the infrastructure for:

- Budget performance calculation
- Transaction allocation
- Budget health checks

### **Phase 2: Implement 2-3 Strategies**

Start with the most common:

1. Traditional (easiest to understand)
2. Percentage-based (50/30/20 is popular)
3. Envelope (most requested by budget enthusiasts)

### **Phase 3: Add Intelligence**

- Strategy recommendation engine
- Performance comparison tools
- Automatic strategy optimization

### **Phase 4: Advanced Strategies**

- Rolling budgets for variable income
- Value-based for mindful spenders
- Hybrid for power users

### **Phase 5: Migration & Switching**

- Seamless strategy switching
- Historical performance comparison
- A/B testing different strategies

## User Experience Benefits

**For New Users:**

- Quiz to recommend best strategy
- Guided setup with templates
- Can start simple and grow

**For Power Users:**

- Multiple simultaneous budgets
- Custom hybrid strategies
- Advanced analytics and comparisons

**For Everyone:**

- Switch strategies without losing data
- See how different strategies would perform
- Get alerts when a different strategy might work better

This flexible architecture means you're not locked into one budgeting philosophy. Users can experiment and find what works best for their situation, and even use different strategies for different purposes (e.g., envelope for monthly expenses, priority-based for annual goals).

**human** (Aug 29, 2025, 10:14 PM)

I know you had previously generated an ERD in a mermaid chart format. Could we updated that ERD now that we have the concepts of audit trails, machine learning and intelligence, flexible budget types, and debt payoff calculators?

**assistant** (Aug 29, 2025, 10:16 PM)

 Absolutely! Let me create an updated ERD that includes all the new concepts we've discussed:

#### create Complete Finance App ERD with All Features

```
erDiagram
    User ||--o{ Account : owns
    User ||--o{ Category : creates
    User ||--o{ Budget : manages
    User ||--o{ Goal : sets
    User ||--o{ Tag : defines
    User ||--o{ Alert : configures
    User ||--o{ Report : generates
    User ||--o{ ScheduledTransaction : schedules
    User ||--o{ Bill : tracks
    User ||--o{ DebtPayoffPlan : creates
    User ||--o{ MLPreference : configures
    User }o--o{ Household : "belongs to"
    
    Household ||--o{ HouseholdMember : contains
    Household ||--o{ SharedAccount : shares
    Household ||--o{ SharedBudget : manages
    HouseholdMember }o--|| User : "is a"
    
    Account ||--o{ Transaction : contains
    Account ||--o{ Transfer : source
    Account ||--o{ Transfer : destination
    Account ||--o{ Goal : "funds towards"
    Account ||--o{ NetWorthSnapshot : "included in"
    Account ||--o{ Investment : holds
    Account ||--o{ Loan : represents
    Account ||--o{ AccountEvent : "generates events"
    
    Transaction }o--|| Category : "belongs to"
    Transaction }o--|| Payee : "paid to"
    Transaction }o--o{ Tag : "tagged with"
    Transaction ||--o{ Attachment : "has receipts"
    Transaction }o--|| ScheduledTransaction : "created by"
    Transaction ||--o{ TransactionEvent : "generates events"
    Transaction ||--o{ MLPrediction : "has predictions"
    Transaction }o--o{ EnvelopeAllocation : "allocated to"
    
    Category ||--o{ Category : "parent of"
    Category ||--o{ CategoryBudget : "budgeted in"
    Category ||--o{ MLTrainingData : "used for training"
    Category ||--o{ ValueMapping : "maps to values"
    
    Budget ||--o{ BudgetPeriod : "has periods"
    Budget ||--|| BudgetStrategy : "implements"
    Budget ||--o{ BudgetEvent : "generates events"
    
    BudgetStrategy ||--o{ EnvelopeBudget : "type of"
    BudgetStrategy ||--o{ TraditionalBudget : "type of"
    BudgetStrategy ||--o{ PercentageBudget : "type of"
    BudgetStrategy ||--o{ PriorityBudget : "type of"
    BudgetStrategy ||--o{ RollingBudget : "type of"
    BudgetStrategy ||--o{ ValueBasedBudget : "type of"
    BudgetStrategy ||--o{ HybridBudget : "type of"
    
    EnvelopeBudget ||--o{ Envelope : contains
    Envelope ||--o{ EnvelopeAllocation : tracks
    Envelope ||--o{ EnvelopeTransfer : "transfers between"
    
    TraditionalBudget ||--o{ CategoryBudget : defines
    CategoryBudget }o--|| Category : "limits spending for"
    
    PercentageBudget ||--o{ PercentageRule : defines
    PercentageRule }o--o{ Category : includes
    
    PriorityBudget ||--o{ BudgetPriority : defines
    BudgetPriority }o--o{ Category : "links to"
    BudgetPriority }o--|| Account : "auto-transfers to"
    
    ValueBasedBudget ||--o{ PersonalValue : defines
    PersonalValue ||--o{ ValueMapping : "maps categories"
    
    HybridBudget ||--o{ Budget : combines
    
    BudgetPeriod }o--o{ Transaction : tracks
    BudgetPeriod ||--o{ BudgetPerformance : calculates
    
    Transfer }o--|| Transaction : "is type of"
    
    ScheduledTransaction }o--|| Payee : "pays to"
    ScheduledTransaction }o--|| Category : "categorized as"
    ScheduledTransaction ||--o{ RecurrenceRule : "follows"
    
    Bill ||--|| ScheduledTransaction : "is type of"
    Bill }o--|| Payee : "owed to"
    Bill ||--o{ BillPayment : "tracks payments"
    Bill ||--o{ BillReminder : generates
    
    Investment }o--|| Portfolio : "grouped in"
    Portfolio }o--|| User : "owned by"
    
    Loan }o--|| Payee : "owed to"
    Loan }o--|| DebtPayoffPlan : "included in"
    
    NetWorthSnapshot }o--|| User : "calculated for"
    
    Alert }o--|| Account : "monitors"
    Alert }o--|| Budget : "watches"
    Alert }o--|| Bill : "reminds about"
    Alert }o--|| Goal : "tracks progress"
    Alert }o--|| SpendingAnomaly : "triggered by"
    
    Report }o--o{ Account : analyzes
    Report }o--o{ Category : summarizes
    Report }o--o{ Budget : compares
    
    DebtPayoffPlan ||--o{ Debt : contains
    DebtPayoffPlan ||--|| PayoffStrategy : implements
    DebtPayoffPlan ||--o{ PayoffProjection : generates
    Debt }o--|| Account : "tracks in"
    
    EventStore ||--o{ DomainEvent : stores
    DomainEvent ||--o{ AccountEvent : "type of"
    DomainEvent ||--o{ TransactionEvent : "type of"
    DomainEvent ||--o{ BudgetEvent : "type of"
    DomainEvent ||--o{ UserEvent : "type of"
    
    AuditLog ||--o{ AuditEntry : contains
    AuditEntry }o--|| User : "performed by"
    AuditEntry }o--|| DomainEvent : "references"
    
    MLModel ||--o{ MLPrediction : generates
    MLModel ||--o{ MLTrainingData : "trained on"
    MLModel ||--|| MLModelType : "is type of"
    
    MLModelType ||--o{ CategoryPredictionModel : "type of"
    MLModelType ||--o{ AnomalyDetectionModel : "type of"
    MLModelType ||--o{ CashFlowForecastModel : "type of"
    MLModelType ||--o{ RecurringTransactionModel : "type of"
    
    MLPrediction }o--|| Transaction : "predicts for"
    MLPrediction }o--|| Category : "suggests"
    
    MLTrainingData }o--o{ Transaction : "uses"
    MLTrainingData }o--o{ Category : "labels"
    
    SpendingInsight ||--o{ InsightRecommendation : provides
    SpendingInsight }o--o{ Transaction : "based on"
    SpendingInsight }o--|| User : "generated for"
    
    SpendingAnomaly }o--|| Transaction : "detected in"
    SpendingAnomaly ||--|| AnomalyDetectionModel : "detected by"
    
    CashFlowForecast }o--|| User : "generated for"
    CashFlowForecast ||--o{ ForecastPeriod : contains
    CashFlowForecast ||--|| CashFlowForecastModel : "generated by"

    User {
        uuid id PK
        string email UK
        string name
        string currency
        json preferences
        json ml_preferences
        datetime created_at
        datetime updated_at
    }
    
    Account {
        uuid id PK
        uuid user_id FK
        string name
        string type
        decimal balance
        string institution
        string currency
        decimal interest_rate
        boolean is_active
        datetime created_at
        datetime updated_at
    }
    
    Transaction {
        uuid id PK
        uuid account_id FK
        uuid category_id FK
        uuid payee_id FK
        decimal amount
        date transaction_date
        string description
        string type
        string status
        json metadata
        uuid ml_prediction_id FK
        decimal confidence_score
        boolean auto_categorized
        datetime created_at
        datetime updated_at
    }
    
    Budget {
        uuid id PK
        uuid user_id FK
        string name
        string budget_type
        string period_type
        json settings
        boolean is_active
        datetime start_date
        datetime end_date
        datetime created_at
    }
    
    BudgetStrategy {
        uuid id PK
        string type
        string name
        json configuration
        boolean supports_migration
    }
    
    EnvelopeBudget {
        uuid id PK
        uuid budget_id FK
        boolean strict_mode
        boolean allow_borrowing
        decimal total_allocated
    }
    
    Envelope {
        uuid id PK
        uuid envelope_budget_id FK
        string name
        decimal allocated_amount
        decimal spent_amount
        string category
        boolean rollover_enabled
        string color
        string icon
    }
    
    DebtPayoffPlan {
        uuid id PK
        uuid user_id FK
        string name
        string strategy
        decimal monthly_budget
        datetime start_date
        boolean is_active
    }
    
    Debt {
        uuid id PK
        uuid plan_id FK
        uuid account_id FK
        string name
        decimal original_balance
        decimal current_balance
        decimal interest_rate
        decimal minimum_payment
        integer priority
    }
    
    Bill {
        uuid id PK
        uuid user_id FK
        string name
        decimal amount
        string frequency
        date next_due_date
        boolean auto_pay
        boolean is_variable
        decimal average_amount
        integer reminder_days
    }
    
    EventStore {
        bigint sequence_number PK
        uuid event_id UK
        uuid aggregate_id IX
        string event_type
        integer event_version
        jsonb event_data
        jsonb event_metadata
        timestamp occurred_at IX
        uuid user_id IX
        string user_email
        inet ip_address
        text user_agent
    }
    
    AuditLog {
        uuid id PK
        string entity_type
        string entity_id
        string action
        jsonb old_values
        jsonb new_values
        jsonb changed_properties
        uuid user_id FK
        string user_email
        inet ip_address
        timestamp timestamp IX
    }
    
    MLModel {
        uuid id PK
        string model_name UK
        string model_type
        string version
        bytea model_data
        json hyperparameters
        decimal accuracy_score
        datetime trained_at
        datetime deployed_at
        boolean is_active
    }
    
    MLPrediction {
        uuid id PK
        uuid transaction_id FK
        uuid model_id FK
        uuid predicted_category_id FK
        decimal confidence_score
        json features_used
        boolean accepted_by_user
        datetime predicted_at
    }
    
    SpendingInsight {
        uuid id PK
        uuid user_id FK
        string insight_type
        string title
        text description
        string priority
        json recommendations
        json related_transactions
        datetime generated_at
        boolean dismissed
    }
    
    SpendingAnomaly {
        uuid id PK
        uuid user_id FK
        uuid transaction_id FK
        string anomaly_type
        decimal severity_score
        decimal expected_value
        decimal actual_value
        text description
        datetime detected_at
        boolean acknowledged
    }
    
    Household {
        uuid id PK
        string name
        uuid created_by FK
        datetime created_at
    }
    
    HouseholdMember {
        uuid id PK
        uuid household_id FK
        uuid user_id FK
        string role
        json permissions
        datetime joined_at
    }
    
    PersonalValue {
        uuid id PK
        uuid budget_id FK
        string name
        string description
        integer importance
        decimal target_percentage
    }
    
    ValueMapping {
        uuid value_id FK
        uuid category_id FK
        decimal weight
    }
```

This comprehensive ERD now includes all the major features we've discussed:

## Key Additions and Changes

### **Event Sourcing & Audit Trail**

- **EventStore**: Central repository for all domain events with complete audit metadata
- **DomainEvent**: Base event type with subtypes for different aggregates
- **AuditLog**: Simplified audit trail for non-event-sourced entities
- **AuditEntry**: Individual audit records with before/after values

### **Machine Learning & Intelligence**

- **MLModel**: Stores trained models with versioning
- **MLModelType**: Different model types (categorization, anomaly detection, forecasting)
- **MLPrediction**: Predictions made for transactions
- **MLTrainingData**: Historical data used for training
- **SpendingInsight**: Generated insights and recommendations
- **SpendingAnomaly**: Detected unusual patterns
- **CashFlowForecast**: Future balance predictions

### **Flexible Budget Types**

- **BudgetStrategy**: Base strategy that all budget types implement
- **EnvelopeBudget**: Zero-based budgeting with envelopes
- **TraditionalBudget**: Category-based limits
- **PercentageBudget**: 50/30/20 and similar strategies
- **PriorityBudget**: Pay yourself first approach
- **RollingBudget**: Based on rolling averages
- **ValueBasedBudget**: Align spending with values
- **HybridBudget**: Combines multiple strategies

### **Personal Finance Features**

- **Household**: Family account management
- **HouseholdMember**: Members with roles and permissions
- **Bill**: Recurring bill tracking
- **BillPayment**: Payment history
- **DebtPayoffPlan**: Debt elimination strategies
- **Debt**: Individual debts within a plan
- **PersonalValue**: Values for value-based budgeting
- **ValueMapping**: Links categories to personal values

### **Enhanced Relationships**

- Transactions now have ML predictions and confidence scores
- Budgets can implement different strategies
- Categories are used for ML training
- Events are linked to audit entries
- Households share accounts and budgets
- Multiple budget types can coexist

## Architecture Benefits

1. **Traceability**: Every change flows through EventStore, creating an immutable audit trail

2. **Intelligence**: ML models continuously learn from user behavior, improving predictions over time

3. **Flexibility**: Users can switch between budget strategies or combine them without losing data

4. **Scalability**: The event-sourced architecture supports temporal queries and retroactive corrections

5. **Family-Friendly**: Household management allows shared finances with granular permissions

6. **Comprehensive**: Covers everything from basic transactions to complex debt strategies

This ERD represents a sophisticated personal finance platform that can grow with users' needs, from simple expense tracking to complex family financial management with ML-powered insights.

**human** (Aug 29, 2025, 10:16 PM)

There was an error with the ERD. Could you try writing it again?

**assistant** (Aug 29, 2025, 10:17 PM)

 Let me rewrite the ERD with corrected syntax:

#### rewrite Untitled

```
erDiagram
    User ||--o{ Account : owns
    User ||--o{ Category : creates
    User ||--o{ Budget : manages
    User ||--o{ Goal : sets
    User ||--o{ Tag : defines
    User ||--o{ Alert : configures
    User ||--o{ Report : generates
    User ||--o{ ScheduledTransaction : schedules
    User ||--o{ Bill : tracks
    User ||--o{ DebtPayoffPlan : creates
    User ||--o{ MLPreference : configures
    User }o--o{ Household : "belongs to"
    
    Household ||--o{ HouseholdMember : contains
    Household ||--o{ SharedAccount : shares
    Household ||--o{ SharedBudget : manages
    HouseholdMember }o--|| User : "is a"
    
    Account ||--o{ Transaction : contains
    Account ||--o{ Transfer : "source for"
    Account ||--o{ Transfer : "destination for"
    Account ||--o{ Goal : "funds towards"
    Account ||--o{ AccountEvent : "generates events"
    
    Transaction }o--|| Category : "belongs to"
    Transaction }o--|| Payee : "paid to"
    Transaction }o--o{ Tag : "tagged with"
    Transaction ||--o{ Attachment : "has receipts"
    Transaction }o--|| ScheduledTransaction : "created by"
    Transaction ||--o{ TransactionEvent : "generates events"
    Transaction ||--o{ MLPrediction : "has predictions"
    Transaction }o--o{ EnvelopeAllocation : "allocated to"
    
    Category ||--o{ Category : "parent of"
    Category ||--o{ CategoryBudget : "budgeted in"
    Category ||--o{ MLTrainingData : "used for training"
    Category ||--o{ ValueMapping : "maps to values"
    
    Budget ||--o{ BudgetPeriod : "has periods"
    Budget ||--o{ BudgetEvent : "generates events"
    
    EnvelopeBudget }|--|| Budget : extends
    TraditionalBudget }|--|| Budget : extends
    PercentageBudget }|--|| Budget : extends
    PriorityBudget }|--|| Budget : extends
    RollingBudget }|--|| Budget : extends
    ValueBasedBudget }|--|| Budget : extends
    HybridBudget }|--|| Budget : extends
    
    EnvelopeBudget ||--o{ Envelope : contains
    Envelope ||--o{ EnvelopeAllocation : tracks
    Envelope ||--o{ EnvelopeTransfer : "transfers between"
    
    TraditionalBudget ||--o{ CategoryBudget : defines
    CategoryBudget }o--|| Category : "limits spending for"
    
    PercentageBudget ||--o{ PercentageRule : defines
    PercentageRule }o--o{ Category : includes
    
    PriorityBudget ||--o{ BudgetPriority : defines
    BudgetPriority }o--o{ Category : "links to"
    BudgetPriority }o--|| Account : "auto-transfers to"
    
    ValueBasedBudget ||--o{ PersonalValue : defines
    PersonalValue ||--o{ ValueMapping : "maps categories"
    
    HybridBudget ||--o{ ComponentBudget : combines
    ComponentBudget }o--|| Budget : references
    
    BudgetPeriod }o--o{ Transaction : tracks
    BudgetPeriod ||--o{ BudgetPerformance : calculates
    
    Transfer }o--|| Transaction : "debit transaction"
    Transfer }o--|| Transaction : "credit transaction"
    
    ScheduledTransaction }o--|| Payee : "pays to"
    ScheduledTransaction }o--|| Category : "categorized as"
    ScheduledTransaction ||--o{ RecurrenceRule : "follows"
    
    Bill ||--|| ScheduledTransaction : extends
    Bill }o--|| Payee : "owed to"
    Bill ||--o{ BillPayment : "tracks payments"
    Bill ||--o{ BillReminder : generates
    
    Goal }o--|| Account : "linked to"
    
    DebtPayoffPlan ||--o{ Debt : contains
    DebtPayoffPlan ||--o{ PayoffProjection : generates
    Debt }o--|| Account : "tracks in"
    
    EventStore ||--o{ DomainEvent : stores
    AccountEvent }|--|| DomainEvent : "type of"
    TransactionEvent }|--|| DomainEvent : "type of"
    BudgetEvent }|--|| DomainEvent : "type of"
    UserEvent }|--|| DomainEvent : "type of"
    
    AuditLog ||--o{ AuditEntry : contains
    AuditEntry }o--|| User : "performed by"
    AuditEntry }o--|| DomainEvent : "references"
    
    MLModel ||--o{ MLPrediction : generates
    MLModel ||--o{ MLTrainingData : "trained on"
    
    MLPrediction }o--|| Transaction : "predicts for"
    MLPrediction }o--|| Category : "suggests"
    
    MLTrainingData }o--o{ Transaction : "uses"
    MLTrainingData }o--o{ Category : "labels"
    
    SpendingInsight ||--o{ InsightRecommendation : provides
    SpendingInsight }o--o{ Transaction : "based on"
    SpendingInsight }o--|| User : "generated for"
    
    SpendingAnomaly }o--|| Transaction : "detected in"
    SpendingAnomaly }o--|| MLModel : "detected by"
    
    CashFlowForecast }o--|| User : "generated for"
    CashFlowForecast ||--o{ ForecastPeriod : contains
    CashFlowForecast }o--|| MLModel : "generated by"

    User {
        uuid id PK
        string email UK
        string name
        string currency
        json preferences
        datetime created_at
    }
    
    Account {
        uuid id PK
        uuid user_id FK
        string name
        string account_type
        decimal balance
        string institution
        string currency
        boolean is_active
    }
    
    Transaction {
        uuid id PK
        uuid account_id FK
        uuid category_id FK
        uuid payee_id FK
        decimal amount
        date transaction_date
        string description
        string transaction_type
        string status
        json metadata
        boolean auto_categorized
    }
    
    Category {
        uuid id PK
        uuid user_id FK
        uuid parent_id FK
        string name
        string icon
        string color
        boolean is_income
    }
    
    Budget {
        uuid id PK
        uuid user_id FK
        string name
        string budget_type
        string period_type
        json settings
        boolean is_active
        datetime start_date
    }
    
    EnvelopeBudget {
        uuid id PK
        uuid budget_id FK
        boolean strict_mode
        boolean allow_borrowing
        decimal total_allocated
    }
    
    Envelope {
        uuid id PK
        uuid envelope_budget_id FK
        string name
        decimal allocated_amount
        decimal spent_amount
        string envelope_category
        boolean rollover_enabled
    }
    
    TraditionalBudget {
        uuid id PK
        uuid budget_id FK
        decimal expected_income
        boolean track_income
    }
    
    CategoryBudget {
        uuid id PK
        uuid traditional_budget_id FK
        uuid category_id FK
        decimal budgeted_amount
        boolean is_flexible
        decimal warning_threshold
    }
    
    PercentageBudget {
        uuid id PK
        uuid budget_id FK
        decimal monthly_income
        string strategy_type
    }
    
    PercentageRule {
        uuid id PK
        uuid percentage_budget_id FK
        string name
        decimal percentage
        string description
    }
    
    PriorityBudget {
        uuid id PK
        uuid budget_id FK
        decimal monthly_income
        boolean auto_allocate
    }
    
    BudgetPriority {
        uuid id PK
        uuid priority_budget_id FK
        string name
        decimal amount
        integer priority_order
        string priority_type
        boolean is_fixed
    }
    
    ValueBasedBudget {
        uuid id PK
        uuid budget_id FK
        decimal monthly_budget
    }
    
    PersonalValue {
        uuid id PK
        uuid value_budget_id FK
        string name
        string description
        integer importance
        decimal target_percentage
    }
    
    ValueMapping {
        uuid value_id FK
        uuid category_id FK
        decimal weight
    }
    
    DebtPayoffPlan {
        uuid id PK
        uuid user_id FK
        string name
        string strategy
        decimal monthly_budget
        datetime start_date
        boolean is_active
    }
    
    Debt {
        uuid id PK
        uuid plan_id FK
        uuid account_id FK
        string name
        decimal current_balance
        decimal interest_rate
        decimal minimum_payment
        integer priority
    }
    
    Bill {
        uuid id PK
        uuid user_id FK
        string name
        decimal amount
        string frequency
        date next_due_date
        boolean auto_pay
        boolean is_variable
    }
    
    EventStore {
        bigint sequence_number PK
        uuid event_id UK
        uuid aggregate_id
        string event_type
        integer event_version
        jsonb event_data
        timestamp occurred_at
        uuid user_id
    }
    
    AuditLog {
        uuid id PK
        string entity_type
        string entity_id
        string action
        jsonb old_values
        jsonb new_values
        uuid user_id FK
        timestamp created_at
    }
    
    MLModel {
        uuid id PK
        string model_name UK
        string model_type
        string version
        json hyperparameters
        decimal accuracy_score
        datetime trained_at
        boolean is_active
    }
    
    MLPrediction {
        uuid id PK
        uuid transaction_id FK
        uuid model_id FK
        uuid predicted_category_id FK
        decimal confidence_score
        json features_used
        boolean accepted_by_user
    }
    
    SpendingInsight {
        uuid id PK
        uuid user_id FK
        string insight_type
        string title
        text description
        string priority
        json recommendations
        datetime generated_at
    }
    
    SpendingAnomaly {
        uuid id PK
        uuid user_id FK
        uuid transaction_id FK
        string anomaly_type
        decimal severity_score
        text description
        datetime detected_at
    }
    
    Household {
        uuid id PK
        string name
        uuid created_by FK
        datetime created_at
    }
    
    HouseholdMember {
        uuid id PK
        uuid household_id FK
        uuid user_id FK
        string role
        json permissions
        datetime joined_at
    }
```

The ERD should now render correctly. The main corrections I made were:

1. **Fixed inheritance relationships** - Changed budget type inheritance to use `}|--||` notation
2. **Simplified relationship descriptions** - Removed complex punctuation that might cause parsing issues
3. **Corrected self-referential relationships** - Fixed the Transfer entity relationships
4. **Streamlined entity attributes** - Removed some attributes that might have caused issues with special characters

The diagram now shows:

- **Core Financial Entities**: User, Account, Transaction, Category
- **Budget System**: Multiple budget types inheriting from base Budget entity
- **Event Sourcing**: EventStore and DomainEvent hierarchy
- **ML/Intelligence**: MLModel, MLPrediction, SpendingInsight, SpendingAnomaly
- **Personal Finance**: Household, Bill, DebtPayoffPlan
- **Audit Trail**: AuditLog and EventStore for complete history

This comprehensive ERD represents a full-featured personal finance application with advanced budgeting strategies, machine learning capabilities, family sharing, and complete audit trails.

**human** (Aug 29, 2025, 10:21 PM)

You also showed how GraphQL and Postgres could work together previously in three examples you created: ```When using GraphQL with PostgreSQL, you're essentially mapping between three layers: your GraphQL schema (what clients see), your SQL schema (how data is stored), and the resolver layer that connects them. Let me show you how this works for a finance app.```

**assistant** (Aug 29, 2025, 10:27 PM)

 Absolutely! Let me update those three examples to include all the new features we've added - audit trails, ML intelligence, flexible budgeting strategies, and debt management. This will show how the complete system works together:

#### create Complete PostgreSQL Schema with All Features

```sql
-- ============================================
-- COMPLETE POSTGRESQL SCHEMA FOR PERSONAL FINANCE APP
-- Includes: Event Sourcing, ML, Flexible Budgets, Debt Management
-- ============================================

-- Enable required extensions
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "pgcrypto";
CREATE EXTENSION IF NOT EXISTS "pg_trgm"; -- For fuzzy text search

-- ============================================
-- CORE TABLES
-- ============================================

-- Users table with ML preferences
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    email VARCHAR(255) UNIQUE NOT NULL,
    name VARCHAR(255) NOT NULL,
    currency VARCHAR(3) DEFAULT 'USD',
    preferences JSONB DEFAULT '{}',
    ml_preferences JSONB DEFAULT '{
        "auto_categorize": true,
        "anomaly_detection": true,
        "insights_enabled": true,
        "min_confidence": 0.85
    }',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Households for family management
CREATE TABLE households (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL,
    created_by UUID NOT NULL REFERENCES users(id),
    settings JSONB DEFAULT '{}',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_households_creator (created_by)
);

-- Household members with roles and permissions
CREATE TABLE household_members (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    household_id UUID NOT NULL REFERENCES households(id) ON DELETE CASCADE,
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    role VARCHAR(50) NOT NULL CHECK (role IN ('admin', 'parent', 'member', 'child', 'view_only')),
    permissions JSONB DEFAULT '{}',
    joined_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    UNIQUE(household_id, user_id),
    INDEX idx_household_members_user (user_id),
    INDEX idx_household_members_household (household_id)
);

-- Accounts with audit fields
CREATE TABLE accounts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    household_id UUID REFERENCES households(id) ON DELETE SET NULL,
    name VARCHAR(255) NOT NULL,
    account_type VARCHAR(50) NOT NULL,
    balance DECIMAL(15,2) NOT NULL DEFAULT 0,
    institution VARCHAR(255),
    currency VARCHAR(3) DEFAULT 'USD',
    interest_rate DECIMAL(5,2),
    credit_limit DECIMAL(15,2),
    is_active BOOLEAN DEFAULT true,
    is_shared BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    created_by UUID REFERENCES users(id),
    updated_by UUID REFERENCES users(id),
    
    INDEX idx_accounts_user_id (user_id),
    INDEX idx_accounts_household_id (household_id),
    INDEX idx_accounts_type (account_type),
    INDEX idx_accounts_active (is_active) WHERE is_active = true
);

-- Categories with ML training support
CREATE TABLE categories (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    parent_id UUID REFERENCES categories(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    icon VARCHAR(50),
    color VARCHAR(7),
    is_income BOOLEAN DEFAULT false,
    is_system BOOLEAN DEFAULT false,
    ml_keywords TEXT[], -- Keywords for ML training
    ml_patterns JSONB DEFAULT '{}', -- Patterns for auto-categorization
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_categories_user_id (user_id),
    INDEX idx_categories_parent_id (parent_id),
    INDEX idx_categories_ml_keywords USING GIN(ml_keywords),
    UNIQUE(user_id, name, parent_id)
);

-- Payees with ML patterns
CREATE TABLE payees (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    default_category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    ml_patterns TEXT[], -- Patterns to identify this payee
    notes TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_payees_user_id (user_id),
    INDEX idx_payees_ml_patterns USING GIN(ml_patterns),
    UNIQUE(user_id, name)
);

-- Transactions with ML predictions
CREATE TABLE transactions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    account_id UUID NOT NULL REFERENCES accounts(id) ON DELETE CASCADE,
    category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    payee_id UUID REFERENCES payees(id) ON DELETE SET NULL,
    amount DECIMAL(15,2) NOT NULL,
    transaction_date DATE NOT NULL,
    description TEXT,
    transaction_type VARCHAR(20) NOT NULL,
    status VARCHAR(20) DEFAULT 'cleared',
    notes TEXT,
    metadata JSONB DEFAULT '{}',
    
    -- ML fields
    ml_predicted_category_id UUID REFERENCES categories(id),
    ml_confidence_score DECIMAL(3,2),
    auto_categorized BOOLEAN DEFAULT false,
    user_verified BOOLEAN DEFAULT false,
    
    -- Audit fields
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    created_by UUID REFERENCES users(id),
    updated_by UUID REFERENCES users(id),
    
    INDEX idx_transactions_account_id (account_id),
    INDEX idx_transactions_category_id (category_id),
    INDEX idx_transactions_date (transaction_date DESC),
    INDEX idx_transactions_account_date (account_id, transaction_date DESC),
    INDEX idx_transactions_ml_confidence (ml_confidence_score) WHERE ml_confidence_score IS NOT NULL,
    INDEX idx_transactions_description_trgm ON transactions USING GIST (description gist_trgm_ops)
);

-- ============================================
-- EVENT SOURCING & AUDIT TABLES
-- ============================================

-- Event store for complete audit trail
CREATE TABLE event_store (
    sequence_number BIGSERIAL PRIMARY KEY,
    event_id UUID NOT NULL UNIQUE DEFAULT gen_random_uuid(),
    aggregate_id UUID NOT NULL,
    aggregate_type VARCHAR(100) NOT NULL,
    event_type VARCHAR(255) NOT NULL,
    event_version INT NOT NULL DEFAULT 1,
    event_data JSONB NOT NULL,
    event_metadata JSONB DEFAULT '{}',
    occurred_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    -- Audit context
    user_id UUID REFERENCES users(id),
    user_email VARCHAR(255),
    ip_address INET,
    user_agent TEXT,
    correlation_id UUID,
    
    INDEX idx_event_store_aggregate (aggregate_id, occurred_at),
    INDEX idx_event_store_type (event_type, occurred_at),
    INDEX idx_event_store_user (user_id, occurred_at),
    INDEX idx_event_store_date (occurred_at DESC),
    INDEX idx_event_store_correlation (correlation_id) WHERE correlation_id IS NOT NULL
);

-- Snapshots for performance optimization
CREATE TABLE event_snapshots (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    aggregate_id UUID NOT NULL,
    aggregate_type VARCHAR(100) NOT NULL,
    version INT NOT NULL,
    snapshot_data JSONB NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_snapshots_aggregate (aggregate_id, version DESC),
    UNIQUE(aggregate_id, version)
);

-- Simple audit log for non-event-sourced changes
CREATE TABLE audit_logs (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    entity_type VARCHAR(100) NOT NULL,
    entity_id VARCHAR(100) NOT NULL,
    action VARCHAR(50) NOT NULL,
    old_values JSONB,
    new_values JSONB,
    changed_properties TEXT[],
    user_id UUID REFERENCES users(id),
    user_email VARCHAR(255),
    ip_address INET,
    user_agent TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_audit_entity (entity_type, entity_id),
    INDEX idx_audit_user (user_id),
    INDEX idx_audit_date (created_at DESC)
);

-- ============================================
-- FLEXIBLE BUDGETING SYSTEM
-- ============================================

-- Base budgets table supporting multiple strategies
CREATE TABLE budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    household_id UUID REFERENCES households(id) ON DELETE SET NULL,
    name VARCHAR(255) NOT NULL,
    budget_type VARCHAR(50) NOT NULL CHECK (budget_type IN (
        'envelope', 'traditional', 'percentage', 'priority', 
        'rolling', 'value_based', 'hybrid'
    )),
    period_type VARCHAR(20) NOT NULL,
    settings JSONB DEFAULT '{}',
    is_active BOOLEAN DEFAULT true,
    is_shared BOOLEAN DEFAULT false,
    start_date DATE NOT NULL,
    end_date DATE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_budgets_user (user_id),
    INDEX idx_budgets_type (budget_type),
    INDEX idx_budgets_active (is_active) WHERE is_active = true,
    INDEX idx_budgets_household (household_id) WHERE household_id IS NOT NULL
);

-- Envelope budgeting
CREATE TABLE envelope_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    strict_mode BOOLEAN DEFAULT true,
    allow_borrowing BOOLEAN DEFAULT false,
    total_allocated DECIMAL(15,2) DEFAULT 0,
    
    UNIQUE(budget_id)
);

CREATE TABLE envelopes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    envelope_budget_id UUID NOT NULL REFERENCES envelope_budgets(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    allocated_amount DECIMAL(15,2) NOT NULL,
    spent_amount DECIMAL(15,2) DEFAULT 0,
    envelope_category VARCHAR(50),
    rollover_enabled BOOLEAN DEFAULT false,
    color VARCHAR(7),
    icon VARCHAR(50),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_envelopes_budget (envelope_budget_id)
);

CREATE TABLE envelope_allocations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    envelope_id UUID NOT NULL REFERENCES envelopes(id) ON DELETE CASCADE,
    transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    amount DECIMAL(15,2) NOT NULL,
    allocated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    UNIQUE(transaction_id),
    INDEX idx_envelope_allocations_envelope (envelope_id)
);

-- Traditional budgeting
CREATE TABLE traditional_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    expected_income DECIMAL(15,2),
    track_income BOOLEAN DEFAULT false,
    
    UNIQUE(budget_id)
);

CREATE TABLE category_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    traditional_budget_id UUID NOT NULL REFERENCES traditional_budgets(id) ON DELETE CASCADE,
    category_id UUID NOT NULL REFERENCES categories(id) ON DELETE CASCADE,
    budgeted_amount DECIMAL(15,2) NOT NULL,
    is_flexible BOOLEAN DEFAULT false,
    warning_threshold DECIMAL(3,2) DEFAULT 0.8,
    
    UNIQUE(traditional_budget_id, category_id),
    INDEX idx_category_budgets_category (category_id)
);

-- Percentage-based budgeting
CREATE TABLE percentage_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    monthly_income DECIMAL(15,2) NOT NULL,
    strategy_type VARCHAR(50) DEFAULT 'standard_50_30_20',
    
    UNIQUE(budget_id)
);

CREATE TABLE percentage_rules (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    percentage_budget_id UUID NOT NULL REFERENCES percentage_budgets(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    percentage DECIMAL(5,2) NOT NULL,
    description TEXT,
    categories TEXT[],
    
    INDEX idx_percentage_rules_budget (percentage_budget_id)
);

-- Priority-based budgeting
CREATE TABLE priority_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    monthly_income DECIMAL(15,2) NOT NULL,
    auto_allocate BOOLEAN DEFAULT true,
    
    UNIQUE(budget_id)
);

CREATE TABLE budget_priorities (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    priority_budget_id UUID NOT NULL REFERENCES priority_budgets(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    amount DECIMAL(15,2) NOT NULL,
    priority_order INT NOT NULL,
    priority_type VARCHAR(50),
    is_fixed BOOLEAN DEFAULT true,
    linked_account_id UUID REFERENCES accounts(id),
    linked_categories UUID[],
    
    INDEX idx_priorities_budget (priority_budget_id),
    INDEX idx_priorities_order (priority_order)
);

-- Value-based budgeting
CREATE TABLE value_based_budgets (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    monthly_budget DECIMAL(15,2) NOT NULL,
    
    UNIQUE(budget_id)
);

CREATE TABLE personal_values (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    value_budget_id UUID NOT NULL REFERENCES value_based_budgets(id) ON DELETE CASCADE,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    importance INT CHECK (importance BETWEEN 1 AND 10),
    target_percentage DECIMAL(5,2),
    
    INDEX idx_values_budget (value_budget_id)
);

CREATE TABLE value_category_mappings (
    value_id UUID NOT NULL REFERENCES personal_values(id) ON DELETE CASCADE,
    category_id UUID NOT NULL REFERENCES categories(id) ON DELETE CASCADE,
    weight DECIMAL(3,2) DEFAULT 1.0,
    
    PRIMARY KEY (value_id, category_id)
);

-- Budget periods and performance tracking
CREATE TABLE budget_periods (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    budget_id UUID NOT NULL REFERENCES budgets(id) ON DELETE CASCADE,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    total_budgeted DECIMAL(15,2),
    total_spent DECIMAL(15,2) DEFAULT 0,
    performance_data JSONB DEFAULT '{}',
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_budget_periods_budget (budget_id),
    INDEX idx_budget_periods_dates (start_date, end_date),
    UNIQUE(budget_id, start_date, end_date)
);

-- ============================================
-- DEBT MANAGEMENT
-- ============================================

CREATE TABLE debt_payoff_plans (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    strategy VARCHAR(50) NOT NULL CHECK (strategy IN ('snowball', 'avalanche', 'custom')),
    monthly_budget DECIMAL(15,2) NOT NULL,
    start_date DATE NOT NULL,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_debt_plans_user (user_id),
    INDEX idx_debt_plans_active (is_active) WHERE is_active = true
);

CREATE TABLE debts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    plan_id UUID NOT NULL REFERENCES debt_payoff_plans(id) ON DELETE CASCADE,
    account_id UUID REFERENCES accounts(id) ON DELETE SET NULL,
    name VARCHAR(255) NOT NULL,
    original_balance DECIMAL(15,2) NOT NULL,
    current_balance DECIMAL(15,2) NOT NULL,
    interest_rate DECIMAL(5,2) NOT NULL,
    minimum_payment DECIMAL(15,2) NOT NULL,
    priority INT,
    is_paid_off BOOLEAN DEFAULT false,
    paid_off_date DATE,
    
    INDEX idx_debts_plan (plan_id),
    INDEX idx_debts_account (account_id)
);

-- ============================================
-- BILLS & SCHEDULED TRANSACTIONS
-- ============================================

CREATE TABLE bills (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    name VARCHAR(255) NOT NULL,
    amount DECIMAL(15,2) NOT NULL,
    frequency VARCHAR(50) NOT NULL,
    next_due_date DATE NOT NULL,
    payee_id UUID REFERENCES payees(id) ON DELETE SET NULL,
    category_id UUID REFERENCES categories(id) ON DELETE SET NULL,
    linked_account_id UUID REFERENCES accounts(id) ON DELETE SET NULL,
    auto_pay BOOLEAN DEFAULT false,
    is_variable BOOLEAN DEFAULT false,
    average_amount DECIMAL(15,2),
    reminder_days_before INT DEFAULT 3,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_bills_user (user_id),
    INDEX idx_bills_due_date (next_due_date),
    INDEX idx_bills_active (is_active) WHERE is_active = true
);

CREATE TABLE bill_payments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    bill_id UUID NOT NULL REFERENCES bills(id) ON DELETE CASCADE,
    transaction_id UUID REFERENCES transactions(id) ON DELETE SET NULL,
    amount_paid DECIMAL(15,2) NOT NULL,
    payment_date DATE NOT NULL,
    status VARCHAR(50) DEFAULT 'completed',
    
    INDEX idx_bill_payments_bill (bill_id),
    INDEX idx_bill_payments_date (payment_date DESC)
);

-- ============================================
-- MACHINE LEARNING TABLES
-- ============================================

CREATE TABLE ml_models (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    model_name VARCHAR(100) NOT NULL,
    model_type VARCHAR(50) NOT NULL,
    version VARCHAR(20) NOT NULL,
    model_data BYTEA, -- Serialized model
    hyperparameters JSONB DEFAULT '{}',
    metrics JSONB DEFAULT '{}',
    accuracy_score DECIMAL(5,4),
    training_data_count INT,
    trained_at TIMESTAMP WITH TIME ZONE,
    deployed_at TIMESTAMP WITH TIME ZONE,
    is_active BOOLEAN DEFAULT false,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    UNIQUE(model_name, version),
    INDEX idx_ml_models_active (model_name, is_active) WHERE is_active = true
);

CREATE TABLE ml_predictions (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    transaction_id UUID NOT NULL REFERENCES transactions(id) ON DELETE CASCADE,
    model_id UUID NOT NULL REFERENCES ml_models(id),
    predicted_category_id UUID REFERENCES categories(id),
    confidence_score DECIMAL(3,2),
    features_used JSONB,
    accepted_by_user BOOLEAN,
    feedback VARCHAR(50), -- 'correct', 'incorrect', 'partial'
    predicted_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_ml_predictions_transaction (transaction_id),
    INDEX idx_ml_predictions_confidence (confidence_score DESC),
    INDEX idx_ml_predictions_feedback (feedback) WHERE feedback IS NOT NULL
);

CREATE TABLE ml_training_queue (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    model_type VARCHAR(50) NOT NULL,
    training_data JSONB NOT NULL,
    priority INT DEFAULT 5,
    status VARCHAR(50) DEFAULT 'pending',
    error_message TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    started_at TIMESTAMP WITH TIME ZONE,
    completed_at TIMESTAMP WITH TIME ZONE,
    
    INDEX idx_training_queue_status (status, priority)
);

-- ============================================
-- INSIGHTS & ANALYTICS
-- ============================================

CREATE TABLE spending_insights (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    insight_type VARCHAR(50) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    priority VARCHAR(20) DEFAULT 'medium',
    actionable_advice TEXT,
    related_transactions UUID[],
    related_categories UUID[],
    metadata JSONB DEFAULT '{}',
    generated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    valid_until TIMESTAMP WITH TIME ZONE,
    dismissed BOOLEAN DEFAULT false,
    dismissed_at TIMESTAMP WITH TIME ZONE,
    
    INDEX idx_insights_user (user_id),
    INDEX idx_insights_type (insight_type),
    INDEX idx_insights_priority (priority),
    INDEX idx_insights_dismissed (dismissed) WHERE dismissed = false
);

CREATE TABLE spending_anomalies (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    transaction_id UUID REFERENCES transactions(id) ON DELETE CASCADE,
    anomaly_type VARCHAR(50) NOT NULL,
    severity_score DECIMAL(3,2),
    expected_value DECIMAL(15,2),
    actual_value DECIMAL(15,2),
    deviation_percentage DECIMAL(5,2),
    description TEXT,
    detected_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    acknowledged BOOLEAN DEFAULT false,
    false_positive BOOLEAN DEFAULT false,
    
    INDEX idx_anomalies_user (user_id),
    INDEX idx_anomalies_severity (severity_score DESC),
    INDEX idx_anomalies_unacknowledged (acknowledged) WHERE acknowledged = false
);

CREATE TABLE cash_flow_forecasts (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    forecast_date DATE NOT NULL,
    predicted_income DECIMAL(15,2),
    predicted_expenses DECIMAL(15,2),
    predicted_balance DECIMAL(15,2),
    confidence_level DECIMAL(3,2),
    actual_income DECIMAL(15,2),
    actual_expenses DECIMAL(15,2),
    actual_balance DECIMAL(15,2),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    INDEX idx_forecasts_user_date (user_id, forecast_date),
    UNIQUE(user_id, forecast_date)
);

-- ============================================
-- MATERIALIZED VIEWS FOR PERFORMANCE
-- ============================================

-- Account balance history for quick queries
CREATE MATERIALIZED VIEW account_balance_history AS
SELECT 
    a.id as account_id,
    a.user_id,
    DATE(t.transaction_date) as balance_date,
    SUM(CASE 
        WHEN t.transaction_type = 'credit' THEN t.amount 
        ELSE -t.amount 
    END) OVER (
        PARTITION BY a.id 
        ORDER BY t.transaction_date, t.created_at
    ) as running_balance
FROM accounts a
JOIN transactions t ON a.id = t.account_id
WHERE t.status = 'cleared';

CREATE INDEX idx_balance_history_account_date ON account_balance_history(account_id, balance_date DESC);

-- Category spending summary
CREATE MATERIALIZED VIEW category_spending_summary AS
SELECT 
    c.id as category_id,
    c.user_id,
    c.name as category_name,
    DATE_TRUNC('month', t.transaction_date) as month,
    COUNT(t.id) as transaction_count,
    SUM(t.amount) as total_spent,
    AVG(t.amount) as average_amount,
    STDDEV(t.amount) as stddev_amount
FROM categories c
LEFT JOIN transactions t ON c.id = t.category_id
WHERE t.transaction_type = 'debit'
GROUP BY c.id, c.user_id, c.name, DATE_TRUNC('month', t.transaction_date);

CREATE INDEX idx_category_summary_user_month ON category_spending_summary(user_id, month DESC);

-- ML training data view
CREATE MATERIALIZED VIEW ml_training_data AS
SELECT 
    t.id as transaction_id,
    t.description,
    t.amount,
    t.transaction_date,
    EXTRACT(DOW FROM t.transaction_date) as day_of_week,
    EXTRACT(DAY FROM t.transaction_date) as day_of_month,
    t.category_id,
    c.name as category_name,
    p.name as payee_name,
    t.user_verified,
    t.ml_confidence_score
FROM transactions t
LEFT JOIN categories c ON t.category_id = c.id
LEFT JOIN payees p ON t.payee_id = p.id
WHERE t.category_id IS NOT NULL 
  AND (t.user_verified = true OR t.ml_confidence_score > 0.9);

CREATE INDEX idx_ml_training_category ON ml_training_data(category_id);

-- ============================================
-- FUNCTIONS & TRIGGERS
-- ============================================

-- Update timestamp trigger
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Apply update trigger to relevant tables
CREATE TRIGGER update_users_updated_at BEFORE UPDATE ON users
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_accounts_updated_at BEFORE UPDATE ON accounts
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
CREATE TRIGGER update_transactions_updated_at BEFORE UPDATE ON transactions
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

-- Event store immutability trigger
CREATE OR REPLACE FUNCTION prevent_event_modification()
RETURNS TRIGGER AS $$
BEGIN
    RAISE EXCEPTION 'Event store records cannot be modified or deleted';
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER prevent_event_updates
    BEFORE UPDATE OR DELETE ON event_store
    FOR EACH ROW EXECUTE FUNCTION prevent_event_modification();

-- Automatic audit logging
CREATE OR REPLACE FUNCTION audit_trigger_function()
RETURNS TRIGGER AS $$
DECLARE
    audit_user_id UUID;
    audit_user_email VARCHAR(255);
    old_data JSONB;
    new_data JSONB;
    changed_fields TEXT[];
BEGIN
    -- Get user context from session
    audit_user_id := current_setting('app.current_user_id', true)::UUID;
    audit_user_email := current_setting('app.current_user_email', true);
    
    IF TG_OP = 'UPDATE' THEN
        old_data := to_jsonb(OLD);
        new_data := to_jsonb(NEW);
        
        -- Find changed fields
        SELECT array_agg(key) INTO changed_fields
        FROM jsonb_each(old_data) o
        JOIN jsonb_each(new_data) n ON o.key = n.key
        WHERE o.value IS DISTINCT FROM n.value;
        
        INSERT INTO audit_logs (
            entity_type, entity_id, action, 
            old_values, new_values, changed_properties,
            user_id, user_email
        ) VALUES (
            TG_TABLE_NAME, NEW.id::TEXT, TG_OP,
            old_data, new_data, changed_fields,
            audit_user_id, audit_user_email
        );
    ELSIF TG_OP = 'INSERT' THEN
        INSERT INTO audit_logs (
            entity_type, entity_id, action,
            new_values, user_id, user_email
        ) VALUES (
            TG_TABLE_NAME, NEW.id::TEXT, TG_OP,
            to_jsonb(NEW), audit_user_id, audit_user_email
        );
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO audit_logs (
            entity_type, entity_id, action,
            old_values, user_id, user_email
        ) VALUES (
            TG_TABLE_NAME, OLD.id::TEXT, TG_OP,
            to_jsonb(OLD), audit_user_id, audit_user_email
        );
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Apply audit triggers to critical tables
CREATE TRIGGER audit_accounts AFTER INSERT OR UPDATE OR DELETE ON accounts
    FOR EACH ROW EXECUTE FUNCTION audit_trigger_function();
CREATE TRIGGER audit_transactions AFTER INSERT OR UPDATE OR DELETE ON transactions
    FOR EACH ROW EXECUTE FUNCTION audit_trigger_function();

-- Function to calculate budget performance
CREATE OR REPLACE FUNCTION calculate_budget_performance(
    p_budget_id UUID,
    p_start_date DATE,
    p_end_date DATE
)
RETURNS TABLE (
    category_id UUID,
    category_name VARCHAR,
    budgeted DECIMAL,
    spent DECIMAL,
    remaining DECIMAL,
    percentage_used DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    WITH budget_data AS (
        SELECT 
            b.id as budget_id,
            b.budget_type
        FROM budgets b
        WHERE b.id = p_budget_id
    ),
    spending_data AS (
        SELECT 
            t.category_id,
            SUM(t.amount) as total_spent
        FROM transactions t
        JOIN accounts a ON t.account_id = a.id
        JOIN budget_data bd ON true
        WHERE a.user_id = (SELECT user_id FROM budgets WHERE id = p_budget_id)
          AND t.transaction_date BETWEEN p_start_date AND p_end_date
          AND t.transaction_type = 'debit'
        GROUP BY t.category_id
    )
    -- Different logic based on budget type
    SELECT 
        c.id,
        c.name,
        COALESCE(cb.budgeted_amount, 0),
        COALESCE(sd.total_spent, 0),
        COALESCE(cb.budgeted_amount, 0) - COALESCE(sd.total_spent, 0),
        CASE 
            WHEN cb.budgeted_amount > 0 
            THEN (COALESCE(sd.total_spent, 0) / cb.budgeted_amount * 100)
            ELSE 0
        END
    FROM categories c
    LEFT JOIN category_budgets cb ON c.id = cb.category_id
    LEFT JOIN spending_data sd ON c.id = sd.category_id
    WHERE c.user_id = (SELECT user_id FROM budgets WHERE id = p_budget_id);
END;
$$ LANGUAGE plpgsql;

-- Function to detect recurring transactions
CREATE OR REPLACE FUNCTION detect_recurring_transactions(
    p_user_id UUID,
    p_lookback_months INT DEFAULT 6
)
RETURNS TABLE (
    payee_id UUID,
    payee_name VARCHAR,
    amount DECIMAL,
    frequency_days INT,
    next_expected_date DATE,
    confidence DECIMAL
) AS $$
BEGIN
    RETURN QUERY
    WITH transaction_patterns AS (
        SELECT 
            t.payee_id,
            p.name as payee_name,
            t.amount,
            t.transaction_date,
            LAG(t.transaction_date) OVER (
                PARTITION BY t.payee_id, t.amount 
                ORDER BY t.transaction_date
            ) as prev_date
        FROM transactions t
        JOIN payees p ON t.payee_id = p.id
        JOIN accounts a ON t.account_id = a.id
        WHERE a.user_id = p_user_id
          AND t.transaction_date >= CURRENT_DATE - INTERVAL '1 month' * p_lookback_months
    ),
    recurring_candidates AS (
        SELECT 
            payee_id,
            payee_name,
            amount,
            AVG(transaction_date - prev_date) as avg_frequency,
            STDDEV(EXTRACT(EPOCH FROM (transaction_date - prev_date)) / 86400) as frequency_stddev,
            COUNT(*) as occurrence_count,
            MAX(transaction_date) as last_date
        FROM transaction_patterns
        WHERE prev_date IS NOT NULL
        GROUP BY payee_id, payee_name, amount
        HAVING COUNT(*) >= 3  -- At least 3 occurrences
    )
    SELECT 
        payee_id,
        payee_name,
        amount,
        EXTRACT(DAY FROM avg_frequency)::INT as frequency_days,
        last_date + avg_frequency as next_expected_date,
        CASE 
            WHEN frequency_stddev < 3 THEN 0.95
            WHEN frequency_stddev < 7 THEN 0.85
            WHEN frequency_stddev < 14 THEN 0.75
            ELSE 0.65
        END as confidence
    FROM recurring_candidates
    WHERE EXTRACT(DAY FROM avg_frequency) BETWEEN 7 AND 365;
END;
$$ LANGUAGE plpgsql;

-- ============================================
-- INDEXES FOR PERFORMANCE
-- ============================================

-- Composite indexes for common queries
CREATE INDEX idx_transactions_user_date ON transactions 
    USING btree (account_id, transaction_date DESC, created_at DESC);

CREATE INDEX idx_transactions_category_date ON transactions 
    USING btree (category_id, transaction_date DESC) 
    WHERE category_id IS NOT NULL;

CREATE INDEX idx_budget_performance ON transactions 
    USING btree (category_id, transaction_date, transaction_type)
    WHERE transaction_type = 'debit';

-- Partial indexes for active records
CREATE INDEX idx_active_budgets_user ON budgets (user_id, budget_type) 
    WHERE is_active = true;

CREATE INDEX idx_pending_ml_predictions ON transactions (account_id, created_at DESC)
    WHERE category_id IS NULL AND ml_predicted_category_id IS NOT NULL;

-- Full text search indexes
CREATE INDEX idx_transaction_search ON transactions 
    USING gin(to_tsvector('english', description || ' ' || COALESCE(notes, '')));

CREATE INDEX idx_payee_search ON payees 
    USING gin(to_tsvector('english', name));

-- ============================================
-- ROW LEVEL SECURITY
-- ============================================

ALTER TABLE users ENABLE ROW LEVEL SECURITY;
ALTER TABLE accounts ENABLE ROW LEVEL SECURITY;
ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;
ALTER TABLE budgets ENABLE ROW LEVEL SECURITY;

-- Users can only see their own data
CREATE POLICY users_isolation ON users
    FOR ALL USING (id = current_setting('app.current_user_id')::UUID);

CREATE POLICY accounts_isolation ON accounts
    FOR ALL USING (
        user_id = current_setting('app.current_user_id')::UUID
        OR household_id IN (
            SELECT household_id FROM household_members 
            WHERE user_id = current_setting('app.current_user_id')::UUID
        )
    );

CREATE POLICY transactions_isolation ON transactions
    FOR ALL USING (
        account_id IN (
            SELECT id FROM accounts 
            WHERE user_id = current_setting('app.current_user_id')::UUID
                OR household_id IN (
                    SELECT household_id FROM household_members 
                    WHERE user_id = current_setting('app.current_user_id')::UUID
                )
        )
    );

-- ============================================
-- SAMPLE DATA INSERTION (Optional)
-- ============================================

-- Sample event for audit demonstration
/*
INSERT INTO event_store (
    aggregate_id,
    aggregate_type,
    event_type,
    event_data,
    user_id
) VALUES (
    gen_random_uuid(),
    'Account',
    'AccountCreated',
    '{"name": "Main Checking", "balance": 1000.00, "type": "checking"}'::jsonb,
    (SELECT id FROM users LIMIT 1)
);
*/
```

#### create Complete GraphQL Schema with All Features

```graphql
# ============================================
# COMPLETE GRAPHQL SCHEMA FOR PERSONAL FINANCE APP
# Includes: Event Sourcing, ML, Flexible Budgets, Debt Management
# ============================================

# ============================================
# SCALARS & ENUMS
# ============================================

scalar Date
scalar DateTime
scalar Decimal
scalar JSON
scalar UUID

enum AccountType {
  CHECKING
  SAVINGS
  CREDIT_CARD
  INVESTMENT
  LOAN
  CASH
}

enum TransactionType {
  DEBIT
  CREDIT
}

enum TransactionStatus {
  PENDING
  CLEARED
  RECONCILED
  VOID
}

enum BudgetType {
  ENVELOPE
  TRADITIONAL
  PERCENTAGE
  PRIORITY
  ROLLING
  VALUE_BASED
  HYBRID
}

enum BudgetPeriod {
  DAILY
  WEEKLY
  BIWEEKLY
  MONTHLY
  QUARTERLY
  YEARLY
  CUSTOM
}

enum PayoffStrategy {
  SNOWBALL
  AVALANCHE
  CUSTOM
}

enum HouseholdRole {
  ADMIN
  PARENT
  MEMBER
  CHILD
  VIEW_ONLY
}

enum InsightType {
  SPENDING_TREND
  ANOMALY
  SAVING_OPPORTUNITY
  SUBSCRIPTION_DETECTED
  BUDGET_ALERT
  GOAL_PROGRESS
}

enum InsightPriority {
  LOW
  MEDIUM
  HIGH
  CRITICAL
}

# ============================================
# CORE TYPES
# ============================================

type User {
  id: ID!
  email: String!
  name: String!
  currency: String!
  preferences: JSON!
  mlPreferences: MLPreferences!
  createdAt: DateTime!
  
  # Relationships
  accounts(
    filter: AccountFilter
    first: Int
    after: String
  ): AccountConnection!
  
  categories: [Category!]!
  budgets(active: Boolean): [Budget!]!
  goals: [Goal!]!
  household: Household
  
  # Analytics
  netWorth: NetWorth!
  insights(
    from: Date
    to: Date
    type: InsightType
  ): [SpendingInsight!]!
  
  # ML Features
  autoCategorizationEnabled: Boolean!
  anomalyDetectionEnabled: Boolean!
}

type MLPreferences {
  autoCategorizationEnabled: Boolean!
  anomalyDetectionEnabled: Boolean!
  insightsEnabled: Boolean!
  minConfidenceThreshold: Float!
}

type Account {
  id: ID!
  name: String!
  accountType: AccountType!
  balance: Decimal!
  institution: String
  currency: String!
  interestRate: Float
  creditLimit: Decimal
  isActive: Boolean!
  isShared: Boolean!
  household: Household
  
  # Relationships
  transactions(
    filter: TransactionFilter
    first: Int = 50
    after: String
    from: Date
    to: Date
  ): TransactionConnection!
  
  # Analytics
  monthlyAverage(months: Int = 6): Decimal!
  balanceHistory(days: Int = 30): [BalancePoint!]!
  projectedBalance(daysAhead: Int = 30): Decimal!
  
  # Event sourcing
  auditTrail(first: Int = 20): [AuditEntry!]!
  snapshotAt(timestamp: DateTime!): AccountSnapshot
}

type Transaction {
  id: ID!
  amount: Decimal!
  transactionDate: Date!
  description: String!
  transactionType: TransactionType!
  status: TransactionStatus!
  notes: String
  metadata: JSON
  
  # Relationships
  account: Account!
  category: Category
  payee: Payee
  tags: [Tag!]!
  attachments: [Attachment!]!
  
  # ML Fields
  predictedCategory: Category
  mlConfidenceScore: Float
  autoCategorized: Boolean!
  userVerified: Boolean!
  
  # Budgeting
  envelopeAllocation: EnvelopeAllocation
  budgetImpact: BudgetImpact!
  
  # Audit
  createdAt: DateTime!
  updatedAt: DateTime!
  createdBy: User
}

type Category {
  id: ID!
  name: String!
  icon: String
  color: String
  isIncome: Boolean!
  isSystem: Boolean!
  
  # Hierarchy
  parent: Category
  children: [Category!]!
  
  # ML Training
  mlKeywords: [String!]!
  mlPatterns: JSON
  
  # Analytics
  currentMonthSpending: Decimal!
  averageMonthlySpending: Decimal!
  trendDirection: TrendDirection!
  
  # Budget relationships
  budgetAllocations: [CategoryBudget!]!
  valuesMapped: [PersonalValue!]!
}

# ============================================
# HOUSEHOLD MANAGEMENT
# ============================================

type Household {
  id: ID!
  name: String!
  createdBy: User!
  createdAt: DateTime!
  
  members: [HouseholdMember!]!
  sharedAccounts: [Account!]!
  sharedBudgets: [Budget!]!
  
  # Analytics
  totalNetWorth: Decimal!
  monthlySpending: Decimal!
  savingsRate: Float!
}

type HouseholdMember {
  id: ID!
  user: User!
  role: HouseholdRole!
  permissions: JSON!
  joinedAt: DateTime!
  
  # Contribution tracking
  monthlyContribution: Decimal
  spendingLimit: Decimal
}

# ============================================
# FLEXIBLE BUDGETING SYSTEM
# ============================================

interface Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  
  # Projections
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type EnvelopeBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Envelope specific
  strictMode: Boolean!
  allowBorrowing: Boolean!
  totalAllocated: Decimal!
  totalRemaining: Decimal!
  
  envelopes: [Envelope!]!
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type Envelope {
  id: ID!
  name: String!
  allocatedAmount: Decimal!
  spentAmount: Decimal!
  remainingAmount: Decimal!
  percentageUsed: Float!
  category: EnvelopeCategory!
  rolloverEnabled: Boolean!
  color: String
  icon: String
  
  # Transactions
  allocations: [EnvelopeAllocation!]!
  lastTransaction: Transaction
}

type TraditionalBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Traditional specific
  expectedIncome: Decimal
  trackIncome: Boolean!
  
  categoryBudgets: [CategoryBudget!]!
  uncategorizedSpending: Decimal!
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type CategoryBudget {
  category: Category!
  budgetedAmount: Decimal!
  spentAmount: Decimal!
  remainingAmount: Decimal!
  percentageUsed: Float!
  isFlexible: Boolean!
  warningThreshold: Float!
  isOverBudget: Boolean!
}

type PercentageBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Percentage specific
  monthlyIncome: Decimal!
  strategy: PercentageStrategy!
  
  rules: [PercentageRule!]!
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type PercentageRule {
  name: String!
  percentage: Float!
  allocatedAmount: Decimal!
  spentAmount: Decimal!
  categories: [String!]!
  description: String
}

type PriorityBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Priority specific
  monthlyIncome: Decimal!
  autoAllocate: Boolean!
  
  priorities: [BudgetPriority!]!
  allocationPlan: AllocationPlan!
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type BudgetPriority {
  id: ID!
  name: String!
  amount: Decimal!
  order: Int!
  priorityType: PriorityType!
  isFixed: Boolean!
  isFunded: Boolean!
  fundedAmount: Decimal!
  linkedAccount: Account
  linkedCategories: [Category!]!
}

type ValueBasedBudget implements Budget {
  id: ID!
  name: String!
  budgetType: BudgetType!
  periodType: BudgetPeriod!
  isActive: Boolean!
  isShared: Boolean!
  startDate: Date!
  endDate: Date
  
  # Value-based specific
  monthlyBudget: Decimal!
  
  values: [PersonalValue!]!
  valueAlignment: ValueAlignment!
  
  # Performance
  currentPerformance: BudgetPerformance!
  historicalPerformance(periods: Int = 6): [BudgetPerformance!]!
  projectedEndOfPeriod: BudgetProjection!
  recommendations: [BudgetRecommendation!]!
}

type PersonalValue {
  id: ID!
  name: String!
  description: String!
  importance: Int!
  targetPercentage: Float!
  actualPercentage: Float!
  alignmentScore: Float!
  linkedCategories: [Category!]!
  spendingAmount: Decimal!
}

# ============================================
# DEBT MANAGEMENT
# ============================================

type DebtPayoffPlan {
  id: ID!
  name: String!
  strategy: PayoffStrategy!
  monthlyBudget: Decimal!
  startDate: Date!
  isActive: Boolean!
  
  debts: [Debt!]!
  projection: PayoffProjection!
  
  # Analytics
  totalDebt: Decimal!
  totalPaidOff: Decimal!
  progressPercentage: Float!
  estimatedPayoffDate: Date!
  totalInterestSaved: Decimal!
}

type Debt {
  id: ID!
  name: String!
  originalBalance: Decimal!
  currentBalance: Decimal!
  interestRate: Float!
  minimumPayment: Decimal!
  priority: Int
  isPaidOff: Boolean!
  paidOffDate: Date
  
  account: Account
  paymentHistory: [DebtPayment!]!
  projectedPayoffDate: Date!
}

type PayoffProjection {
  strategy: PayoffStrategy!
  totalPayoffDate: Date!
  totalInterestPaid: Decimal!
  totalPaid: Decimal!
  monthsToPayoff: Int!
  
  monthlyProjections: [MonthlyPayoffProjection!]!
  debtPayoffOrder: [Debt!]!
}

# ============================================
# BILLS & RECURRING
# ============================================

type Bill {
  id: ID!
  name: String!
  amount: Decimal!
  frequency: BillFrequency!
  nextDueDate: Date!
  payee: Payee
  category: Category
  linkedAccount: Account
  autoPay: Boolean!
  isVariable: Boolean!
  averageAmount: Decimal
  reminderDaysBefore: Int!
  isActive: Boolean!
  
  # History
  paymentHistory: [BillPayment!]!
  missedPayments: Int!
  onTimePaymentRate: Float!
  
  # Predictions
  predictedAmount: Decimal
  dueSoon: Boolean!
  isOverdue: Boolean!
}

type BillPayment {
  id: ID!
  bill: Bill!
  amountPaid: Decimal!
  paymentDate: Date!
  transaction: Transaction
  status: PaymentStatus!
}

# ============================================
# MACHINE LEARNING & INSIGHTS
# ============================================

type MLModel {
  id: ID!
  modelName: String!
  modelType: MLModelType!
  version: String!
  accuracy: Float!
  trainedAt: DateTime!
  isActive: Boolean!
  
  metrics: ModelMetrics!
  predictions(last: Int = 100): [MLPrediction!]!
}

type MLPrediction {
  id: ID!
  transaction: Transaction!
  predictedCategory: Category!
  confidenceScore: Float!
  accepted: Boolean
  feedback: PredictionFeedback
  featuresUsed: JSON!
  model: MLModel!
}

type SpendingInsight {
  id: ID!
  type: InsightType!
  title: String!
  description: String!
  priority: InsightPriority!
  actionableAdvice: String!
  relatedTransactions: [Transaction!]!
  relatedCategories: [Category!]!
  generatedAt: DateTime!
  validUntil: DateTime
  dismissed: Boolean!
  
  # Impact if acted upon
  potentialSavings: Decimal
  confidenceLevel: Float!
}

type SpendingAnomaly {
  id: ID!
  transaction: Transaction
  anomalyType: AnomalyType!
  severityScore: Float!
  expectedValue: Decimal!
  actualValue: Decimal!
  deviationPercentage: Float!
  description: String!
  detectedAt: DateTime!
  acknowledged: Boolean!
  falsePositive: Boolean!
}

type CashFlowForecast {
  forecastDate: Date!
  predictedIncome: Decimal!
  predictedExpenses: Decimal!
  predictedBalance: Decimal!
  confidenceLevel: Float!
  
  # Actual vs predicted (if date has passed)
  actualIncome: Decimal
  actualExpenses: Decimal
  actualBalance: Decimal
  accuracy: Float
  
  # Breakdown
  incomeBreakdown: [ForecastComponent!]!
  expenseBreakdown: [ForecastComponent!]!
  scheduledTransactions: [ScheduledTransaction!]!
  warnings: [CashFlowWarning!]!
}

type ForecastComponent {
  category: String!
  amount: Decimal!
  confidence: Float!
  basedOn: String! # "historical", "scheduled", "ml_prediction"
}

# ============================================
# EVENT SOURCING & AUDIT
# ============================================

type EventStore {
  sequenceNumber: Int!
  eventId: ID!
  aggregateId: ID!
  aggregateType: String!
  eventType: String!
  eventData: JSON!
  occurredAt: DateTime!
  userId: ID
  userEmail: String
  correlationId: ID
}

type AuditEntry {
  id: ID!
  entityType: String!
  entityId: String!
  action: String!
  oldValues: JSON
  newValues: JSON
  changedProperties: [String!]!
  user: User
  timestamp: DateTime!
  ipAddress: String
}

type AuditTrail {
  entries(
    first: Int = 20
    after: String
    filter: AuditFilter
  ): AuditEntryConnection!
  
  summary: AuditSummary!
}

type AccountSnapshot {
  accountId: ID!
  timestamp: DateTime!
  balance: Decimal!
  transactionCount: Int!
  name: String!
  isActive: Boolean!
}

# ============================================
# ANALYTICS & REPORTS
# ============================================

type NetWorth {
  total: Decimal!
  assets: Decimal!
  liabilities: Decimal!
  
  byAccount: [AccountNetWorth!]!
  trend: [NetWorthPoint!]!
  projectedIn30Days: Decimal!
  projectedIn90Days: Decimal!
}

type SpendingAnalysis {
  period: DateRange!
  totalIncome: Decimal!
  totalExpenses: Decimal!
  netIncome: Decimal!
  savingsRate: Float!
  
  byCategory: [CategorySpending!]!
  byPayee: [PayeeSpending!]!
  byDay: [DailySpending!]!
  
  trends: SpendingTrends!
  compareToPrevious: SpendingComparison!
}

type BudgetComparison {
  strategies: [BudgetStrategyComparison!]!
  recommendation: BudgetRecommendation!
}

type BudgetStrategyComparison {
  budgetType: BudgetType!
  projectedMonthlySavings: Decimal!
  difficultyLevel: DifficultyLevel!
  flexibilityScore: Float!
  bestFor: String!
  pros: [String!]!
  cons: [String!]!
}

# ============================================
# QUERIES
# ============================================

type Query {
  # User & Profile
  me: User!
  myHousehold: Household
  
  # Accounts
  account(id: ID!): Account
  accounts(
    filter: AccountFilter
    includeInactive: Boolean = false
  ): [Account!]!
  
  # Transactions
  transaction(id: ID!): Transaction
  transactions(
    filter: TransactionFilter
    first: Int = 50
    after: String
  ): TransactionConnection!
  searchTransactions(
    query: String!
    first: Int = 20
  ): [Transaction!]!
  
  # Budgets (polymorphic)
  budget(id: ID!): Budget
  budgets(
    active: Boolean
    type: BudgetType
  ): [Budget!]!
  compareBudgetStrategies(
    monthlyIncome: Decimal!
  ): BudgetComparison!
  
  # Specific budget types
  envelopeBudget(id: ID!): EnvelopeBudget
  traditionalBudget(id: ID!): TraditionalBudget
  percentageBudget(id: ID!): PercentageBudget
  
  # Bills & Recurring
  upcomingBills(daysAhead: Int = 30): [Bill!]!
  recurringTransactions: [RecurringTransaction!]!
  
  # Debt
  debtPayoffPlan(id: ID!): DebtPayoffPlan
  calculatePayoffScenarios(
    debts: [DebtInput!]!
    monthlyBudget: Decimal!
  ): [PayoffProjection!]!
  
  # Analytics
  netWorth: NetWorth!
  spendingAnalysis(
    from: Date!
    to: Date!
  ): SpendingAnalysis!
  cashFlowForecast(
    daysAhead: Int = 30
  ): [CashFlowForecast!]!
  
  # ML & Insights
  insights(
    type: InsightType
    priority: InsightPriority
    dismissed: Boolean = false
  ): [SpendingInsight!]!
  anomalies(
    acknowledged: Boolean = false
    severity: Float
  ): [SpendingAnomaly!]!
  
  # Audit & History
  auditTrail(entityId: ID!): AuditTrail!
  accountSnapshot(
    accountId: ID!
    timestamp: DateTime!
  ): AccountSnapshot
  
  # Event Sourcing
  events(
    aggregateId: ID!
    from: DateTime
    to: DateTime
  ): [EventStore!]!
}

# ============================================
# MUTATIONS
# ============================================

type Mutation {
  # Account Management
  createAccount(input: CreateAccountInput!): Account!
  updateAccount(id: ID!, input: UpdateAccountInput!): Account!
  closeAccount(id: ID!, reason: String): Account!
  
  # Transaction Management
  createTransaction(input: CreateTransactionInput!): Transaction!
  updateTransaction(
    id: ID!
    input: UpdateTransactionInput!
  ): Transaction!
  categorizeTransaction(
    id: ID!
    categoryId: ID!
    trainML: Boolean = true
  ): Transaction!
  splitTransaction(
    id: ID!
    splits: [TransactionSplitInput!]!
  ): [Transaction!]!
  
  # Transfers
  createTransfer(input: CreateTransferInput!): TransferResult!
  
  # Budget Management (type-specific)
  createEnvelopeBudget(
    input: CreateEnvelopeBudgetInput!
  ): EnvelopeBudget!
  createTraditionalBudget(
    input: CreateTraditionalBudgetInput!
  ): TraditionalBudget!
  createPercentageBudget(
    input: CreatePercentageBudgetInput!
  ): PercentageBudget!
  createPriorityBudget(
    input: CreatePriorityBudgetInput!
  ): PriorityBudget!
  createValueBasedBudget(
    input: CreateValueBasedBudgetInput!
  ): ValueBasedBudget!
  
  # Budget Operations
  switchBudgetStrategy(
    currentBudgetId: ID!
    newType: BudgetType!
    settings: JSON
  ): Budget!
  
  # Envelope Operations
  transferBetweenEnvelopes(
    fromEnvelopeId: ID!
    toEnvelopeId: ID!
    amount: Decimal!
  ): EnvelopeTransferResult!
  allocateToEnvelope(
    transactionId: ID!
    envelopeId: ID!
  ): EnvelopeAllocation!
  
  # Bill Management
  createBill(input: CreateBillInput!): Bill!
  recordBillPayment(
    billId: ID!
    input: RecordPaymentInput!
  ): BillPayment!
  
  # Debt Management
  createDebtPayoffPlan(
    input: CreateDebtPlanInput!
  ): DebtPayoffPlan!
  updateDebtBalance(
    debtId: ID!
    newBalance: Decimal!
  ): Debt!
  
  # Household Management
  createHousehold(name: String!): Household!
  inviteToHousehold(
    householdId: ID!
    email: String!
    role: HouseholdRole!
  ): HouseholdInvitation!
  shareAccount(
    accountId: ID!
    householdId: ID!
  ): Account!
  
  # ML Feedback
  confirmMLPrediction(
    predictionId: ID!
    isCorrect: Boolean!
  ): MLPrediction!
  dismissInsight(insightId: ID!): SpendingInsight!
  acknowledgeAnomaly(
    anomalyId: ID!
    isFalsePositive: Boolean = false
  ): SpendingAnomaly!
  
  # User Preferences
  updateMLPreferences(
    input: UpdateMLPreferencesInput!
  ): MLPreferences!
}

# ============================================
# SUBSCRIPTIONS
# ============================================

type Subscription {
  # Real-time updates
  accountBalanceChanged(accountId: ID!): Account!
  transactionAdded(accountId: ID): Transaction!
  
  # Budget alerts
  budgetThresholdReached(budgetId: ID!): BudgetAlert!
  envelopeNearEmpty(budgetId: ID!): EnvelopeAlert!
  
  # Bill reminders
  billDueSoon(daysAhead: Int = 3): BillReminder!
  
  # ML & Insights
  newInsightGenerated: SpendingInsight!
  anomalyDetected: SpendingAnomaly!
  
  # Household updates
  householdActivityStream(
    householdId: ID!
  ): Hous
```

**human** (Aug 29, 2025, 10:28 PM)

Continue

**assistant** (Aug 29, 2025, 10:33 PM)

#### update complete-graphql-schema

Find this:

```
  # Household updates
  householdActivityStream(
    householdId: ID!
  ): Hous
```

Replace with this:

```
  # Household updates
  householdActivityStream(
    householdId: ID!
  ): HouseholdActivity!
```

#### update complete-graphql-schema

Find this:

```
  # Household updates
  householdActivityStream(
    householdId: ID!
  ): HouseholdActivity!
```

Replace with this:

```
  # Household updates
  householdActivityStream(
    householdId: ID!
  ): HouseholdActivity!
}

# ============================================
# INPUT TYPES
# ============================================

input CreateAccountInput {
  name: String!
  accountType: AccountType!
  initialBalance: Decimal!
  institution: String
  currency: String = "USD"
  interestRate: Float
  creditLimit: Decimal
}

input UpdateAccountInput {
  name: String
  institution: String
  interestRate: Float
  creditLimit: Decimal
  isActive: Boolean
}

input CreateTransactionInput {
  accountId: ID!
  amount: Decimal!
  transactionDate: Date!
  description: String!
  transactionType: TransactionType!
  categoryId: ID
  payeeId: ID
  notes: String
  tags: [ID!]
}

input UpdateTransactionInput {
  amount: Decimal
  transactionDate: Date
  description: String
  categoryId: ID
  payeeId: ID
  notes: String
  status: TransactionStatus
}

input CreateTransferInput {
  fromAccountId: ID!
  toAccountId: ID!
  amount: Decimal!
  date: Date!
  description: String!
}

input TransactionSplitInput {
  amount: Decimal!
  categoryId: ID!
  description: String
}

# Budget Input Types
input CreateEnvelopeBudgetInput {
  name: String!
  periodType: BudgetPeriod!
  strictMode: Boolean = true
  allowBorrowing: Boolean = false
  envelopes: [CreateEnvelopeInput!]!
}

input CreateEnvelopeInput {
  name: String!
  allocatedAmount: Decimal!
  category: String!
  rolloverEnabled: Boolean = false
  color: String
  icon: String
}

input CreateTraditionalBudgetInput {
  name: String!
  periodType: BudgetPeriod!
  expectedIncome: Decimal
  categoryBudgets: [CategoryBudgetInput!]!
}

input CategoryBudgetInput {
  categoryId: ID!
  amount: Decimal!
  isFlexible: Boolean = false
  warningThreshold: Float = 0.8
}

input CreatePercentageBudgetInput {
  name: String!
  monthlyIncome: Decimal!
  strategy: String = "standard_50_30_20"
  customRules: [PercentageRuleInput!]
}

input PercentageRuleInput {
  name: String!
  percentage: Float!
  categories: [String!]!
  description: String
}

input CreatePriorityBudgetInput {
  name: String!
  monthlyIncome: Decimal!
  autoAllocate: Boolean = true
  priorities: [PriorityInput!]!
}

input PriorityInput {
  name: String!
  amount: Decimal!
  order: Int!
  priorityType: String!
  isFixed: Boolean = true
  linkedAccountId: ID
  linkedCategoryIds: [ID!]
}

input CreateValueBasedBudgetInput {
  name: String!
  monthlyBudget: Decimal!
  values: [PersonalValueInput!]!
}

input PersonalValueInput {
  name: String!
  description: String!
  importance: Int!
  targetPercentage: Float!
  linkedCategoryIds: [ID!]
}

# Bill & Debt Input Types
input CreateBillInput {
  name: String!
  amount: Decimal!
  frequency: String!
  firstDueDate: Date!
  payeeId: ID
  categoryId: ID
  linkedAccountId: ID
  autoPay: Boolean = false
  isVariable: Boolean = false
  reminderDaysBefore: Int = 3
}

input RecordPaymentInput {
  amountPaid: Decimal!
  paymentDate: Date!
  transactionId: ID
}

input CreateDebtPlanInput {
  name: String!
  strategy: PayoffStrategy!
  monthlyBudget: Decimal!
  debts: [DebtInput!]!
}

input DebtInput {
  name: String!
  currentBalance: Decimal!
  interestRate: Float!
  minimumPayment: Decimal!
  accountId: ID
  priority: Int
}

input UpdateMLPreferencesInput {
  autoCategorizationEnabled: Boolean
  anomalyDetectionEnabled: Boolean
  insightsEnabled: Boolean
  minConfidenceThreshold: Float
}

# Filter Input Types
input AccountFilter {
  accountTypes: [AccountType!]
  isActive: Boolean
  isShared: Boolean
  minBalance: Decimal
  maxBalance: Decimal
}

input TransactionFilter {
  accountIds: [ID!]
  categoryIds: [ID!]
  payeeIds: [ID!]
  transactionTypes: [TransactionType!]
  minAmount: Decimal
  maxAmount: Decimal
  dateFrom: Date
  dateTo: Date
  searchTerm: String
  autoCategorized: Boolean
  hasAttachments: Boolean
}

input AuditFilter {
  entityTypes: [String!]
  actions: [String!]
  userId: ID
  dateFrom: DateTime
  dateTo: DateTime
}

# ============================================
# ADDITIONAL TYPES
# ============================================

type TransactionConnection {
  edges: [TransactionEdge!]!
  pageInfo: PageInfo!
  totalCount: Int!
  totalAmount: Decimal!
}

type TransactionEdge {
  node: Transaction!
  cursor: String!
}

type AccountConnection {
  edges: [AccountEdge!]!
  pageInfo: PageInfo!
  totalCount: Int!
  totalBalance: Decimal!
}

type AccountEdge {
  node: Account!
  cursor: String!
}

type AuditEntryConnection {
  edges: [AuditEntryEdge!]!
  pageInfo: PageInfo!
  totalCount: Int!
}

type AuditEntryEdge {
  node: AuditEntry!
  cursor: String!
}

type PageInfo {
  hasNextPage: Boolean!
  hasPreviousPage: Boolean!
  startCursor: String
  endCursor: String
}

type TransferResult {
  fromTransaction: Transaction!
  toTransaction: Transaction!
  transfer: Transfer!
  success: Boolean!
  message: String
}

type EnvelopeTransferResult {
  fromEnvelope: Envelope!
  toEnvelope: Envelope!
  amount: Decimal!
  success: Boolean!
  message: String
}

type EnvelopeAllocation {
  id: ID!
  envelope: Envelope!
  transaction: Transaction!
  amount: Decimal!
  allocatedAt: DateTime!
}

type BudgetPerformance {
  period: DateRange!
  budgetType: BudgetType!
  totalBudgeted: Decimal!
  totalSpent: Decimal!
  totalRemaining: Decimal!
  percentageUsed: Float!
  onTrack: Boolean!
  projectedOverage: Decimal
  categories: [CategoryPerformance!]!
}

type CategoryPerformance {
  category: Category!
  budgeted: Decimal!
  spent: Decimal!
  remaining: Decimal!
  percentageUsed: Float!
  trend: TrendDirection!
  isOverBudget: Boolean!
}

type BudgetProjection {
  projectedEndDate: Date!
  projectedTotalSpent: Decimal!
  projectedRemaining: Decimal!
  willExceedBudget: Boolean!
  recommendedAdjustment: Decimal
  confidence: Float!
}

type BudgetRecommendation {
  recommendedType: BudgetType!
  reasoning: String!
  projectedSavings: Decimal!
  confidenceScore: Float!
  alternativeOptions: [BudgetType!]!
}

type AllocationPlan {
  totalIncome: Decimal!
  allocations: [PlannedAllocation!]!
  unallocatedAmount: Decimal!
  fullyFunded: Boolean!
}

type PlannedAllocation {
  priorityId: ID!
  name: String!
  plannedAmount: Decimal!
  allocatedAmount: Decimal!
  isFullyFunded: Boolean!
  order: Int!
}

type ValueAlignment {
  values: [ValueScore!]!
  overallAlignmentScore: Float!
  recommendations: [String!]!
}

type ValueScore {
  value: PersonalValue!
  targetPercentage: Float!
  actualPercentage: Float!
  alignmentScore: Float!
  spendingAmount: Decimal!
}

type HouseholdActivity {
  id: ID!
  household: Household!
  activityType: String!
  description: String!
  user: User!
  timestamp: DateTime!
  metadata: JSON
}

type HouseholdInvitation {
  id: ID!
  household: Household!
  email: String!
  role: HouseholdRole!
  status: InvitationStatus!
  invitedBy: User!
  invitedAt: DateTime!
  expiresAt: DateTime!
}

type BudgetAlert {
  id: ID!
  budget: Budget!
  alertType: String!
  message: String!
  severity: AlertSeverity!
  category: Category
  currentSpending: Decimal!
  budgetLimit: Decimal!
  percentageUsed: Float!
}

type EnvelopeAlert {
  id: ID!
  envelope: Envelope!
  alertType: String!
  message: String!
  remainingAmount: Decimal!
  percentageRemaining: Float!
  suggestedAction: String!
}

type BillReminder {
  id: ID!
  bill: Bill!
  daysUntilDue: Int!
  amount: Decimal!
  message: String!
  urgency: UrgencyLevel!
}

type RecurringTransaction {
  payeeId: ID!
  payee: Payee!
  amount: Decimal!
  frequency: Int!
  nextExpectedDate: Date!
  confidence: Float!
  transactions: [Transaction!]!
  suggestedBill: CreateBillInput
}

type MonthlyPayoffProjection {
  month: String!
  payments: [DebtPayment!]!
  totalPaid: Decimal!
  totalPrincipal: Decimal!
  totalInterest: Decimal!
  remainingBalance: Decimal!
}

type DebtPayment {
  debt: Debt!
  payment: Decimal!
  principal: Decimal!
  interest: Decimal!
  remainingBalance: Decimal!
}

type ModelMetrics {
  accuracy: Float!
  precision: Float!
  recall: Float!
  f1Score: Float!
  confusionMatrix: [[Int!]!]!
  lastTrainingSize: Int!
  lastTrainingDate: DateTime!
}

type CashFlowWarning {
  date: Date!
  warningType: String!
  message: String!
  projectedShortfall: Decimal!
  suggestedActions: [String!]!
}

type AuditSummary {
  totalEntries: Int!
  dateRange: DateRange!
  mostActiveUsers: [User!]!
  mostModifiedEntities: [String!]!
  actionBreakdown: JSON!
}

type BalancePoint {
  date: Date!
  balance: Decimal!
  transactions: Int!
}

type NetWorthPoint {
  date: Date!
  netWorth: Decimal!
  assets: Decimal!
  liabilities: Decimal!
}

type AccountNetWorth {
  account: Account!
  value: Decimal!
  percentageOfTotal: Float!
}

type CategorySpending {
  category: Category!
  amount: Decimal!
  transactionCount: Int!
  percentageOfTotal: Float!
  averageTransaction: Decimal!
}

type PayeeSpending {
  payee: Payee!
  amount: Decimal!
  transactionCount: Int!
  averageTransaction: Decimal!
}

type DailySpending {
  date: Date!
  income: Decimal!
  expenses: Decimal!
  net: Decimal!
  transactionCount: Int!
}

type SpendingTrends {
  direction: TrendDirection!
  percentageChange: Float!
  projectedNextMonth: Decimal!
  seasonalPattern: String
}

type SpendingComparison {
  previousPeriod: SpendingAnalysis!
  percentageChange: Float!
  biggestIncreases: [CategoryChange!]!
  biggestDecreases: [CategoryChange!]!
}

type CategoryChange {
  category: Category!
  previousAmount: Decimal!
  currentAmount: Decimal!
  percentageChange: Float!
}

type DateRange {
  from: Date!
  to: Date!
}

type Goal {
  id: ID!
  name: String!
  targetAmount: Decimal!
  currentAmount: Decimal!
  targetDate: Date
  account: Account
  percentageComplete: Float!
  onTrack: Boolean!
  projectedCompletionDate: Date
}

type Payee {
  id: ID!
  name: String!
  defaultCategory: Category
  notes: String
  transactionCount: Int!
  totalSpent: Decimal!
  averageTransaction: Decimal!
  lastTransaction: Transaction
}

type Tag {
  id: ID!
  name: String!
  color: String
  transactionCount: Int!
  totalAmount: Decimal!
}

type Attachment {
  id: ID!
  fileName: String!
  fileSize: Int!
  mimeType: String!
  uploadedAt: DateTime!
  url: String!
}

type Transfer {
  id: ID!
  fromAccount: Account!
  toAccount: Account!
  amount: Decimal!
  date: Date!
  fromTransaction: Transaction!
  toTransaction: Transaction!
}

type ScheduledTransaction {
  id: ID!
  account: Account!
  amount: Decimal!
  description: String!
  category: Category
  payee: Payee
  frequency: String!
  nextDate: Date!
  endDate: Date
  isActive: Boolean!
}

# ============================================
# ENUMS (Additional)
# ============================================

enum TrendDirection {
  UP
  DOWN
  STABLE
}

enum EnvelopeCategory {
  FIXED
  VARIABLE
  SAVINGS
  SINKING_FUND
  DISCRETIONARY
}

enum PercentageStrategy {
  STANDARD_50_30_20
  AGGRESSIVE_30_20_50
  BALANCED_40_30_30
  DEBT_FOCUS_35_5_60
  FIRE_25_5_70
  CUSTOM
}

enum PriorityType {
  SAVINGS
  DEBT_PAYMENT
  INVESTMENT
  GOAL
  FIXED_EXPENSE
  VARIABLE_EXPENSE
}

enum BillFrequency {
  WEEKLY
  BIWEEKLY
  MONTHLY
  QUARTERLY
  ANNUALLY
  CUSTOM
}

enum PaymentStatus {
  PENDING
  COMPLETED
  FAILED
  CANCELLED
}

enum MLModelType {
  CATEGORY_PREDICTION
  ANOMALY_DETECTION
  CASH_FLOW_FORECAST
  RECURRING_DETECTION
}

enum PredictionFeedback {
  CORRECT
  INCORRECT
  PARTIAL
}

enum AnomalyType {
  UNUSUAL_AMOUNT
  UNUSUAL_FREQUENCY
  NEW_PAYEE
  CATEGORY_DEVIATION
  TIME_PATTERN
}

enum InvitationStatus {
  PENDING
  ACCEPTED
  DECLINED
  EXPIRED
}

enum AlertSeverity {
  INFO
  WARNING
  CRITICAL
}

enum UrgencyLevel {
  LOW
  MEDIUM
  HIGH
  CRITICAL
}

enum DifficultyLevel {
  EASY
  MODERATE
  HARD
  EXPERT
}
```

#### create Complete GraphQL Resolver Layer with All Features

```javascript
// ============================================
// COMPLETE GRAPHQL RESOLVERS WITH ALL FEATURES
// Connects GraphQL Schema to PostgreSQL Database
// ============================================

import DataLoader from 'dataloader';
import { Pool } from 'pg';
import { PubSub } from 'graphql-subscriptions';
import * as tf from '@tensorflow/tfjs-node';
import { DateTime } from 'luxon';

const pool = new Pool({
  connectionString: process.env.DATABASE_URL
});

const pubsub = new PubSub();

// ============================================
// DATA LOADERS - Batch & Cache Database Queries
// ============================================

// Account loader with household support
const accountLoader = new DataLoader(async (accountIds) => {
  const query = `
    SELECT a.*, 
           h.id as household_id, 
           h.name as household_name,
           COUNT(t.id) as transaction_count,
           MAX(t.transaction_date) as last_transaction_date
    FROM accounts a
    LEFT JOIN households h ON a.household_id = h.id
    LEFT JOIN transactions t ON a.id = t.account_id
    WHERE a.id = ANY($1::uuid[])
    GROUP BY a.id, h.id, h.name
  `;
  const result = await pool.query(query, [accountIds]);
  
  const accountMap = {};
  result.rows.forEach(row => {
    accountMap[row.id] = row;
  });
  
  return accountIds.map(id => accountMap[id]);
});

// Category loader with ML patterns
const categoryLoader = new DataLoader(async (categoryIds) => {
  const query = `
    SELECT c.*,
           array_agg(DISTINCT t.description) FILTER (WHERE t.description IS NOT NULL) as sample_descriptions,
           COUNT(DISTINCT t.id) as usage_count
    FROM categories c
    LEFT JOIN transactions t ON c.id = t.category_id
    WHERE c.id = ANY($1::uuid[])
    GROUP BY c.id
  `;
  const result = await pool.query(query, [categoryIds]);
  
  const categoryMap = {};
  result.rows.forEach(row => {
    categoryMap[row.id] = row;
  });
  
  return categoryIds.map(id => categoryMap[id]);
});

// Budget loader (polymorphic)
const budgetLoader = new DataLoader(async (budgetIds) => {
  const query = `
    SELECT b.*,
           eb.strict_mode, eb.allow_borrowing, eb.total_allocated,
           tb.expected_income, tb.track_income,
           pb.monthly_income as pb_income, pb.strategy_type,
           prb.monthly_income as prb_income, prb.auto_allocate,
           vb.monthly_budget
    FROM budgets b
    LEFT JOIN envelope_budgets eb ON b.id = eb.budget_id
    LEFT JOIN traditional_budgets tb ON b.id = tb.budget_id
    LEFT JOIN percentage_budgets pb ON b.id = pb.budget_id
    LEFT JOIN priority_budgets prb ON b.id = prb.budget_id
    LEFT JOIN value_based_budgets vb ON b.id = vb.budget_id
    WHERE b.id = ANY($1::uuid[])
  `;
  const result = await pool.query(query, [budgetIds]);
  
  const budgetMap = {};
  result.rows.forEach(row => {
    // Determine budget type and structure accordingly
    budgetMap[row.id] = constructBudgetObject(row);
  });
  
  return budgetIds.map(id => budgetMap[id]);
});

// ML Prediction loader
const mlPredictionLoader = new DataLoader(async (transactionIds) => {
  const query = `
    SELECT p.*, 
           m.model_name, m.model_type, m.version, m.accuracy_score,
           c.name as predicted_category_name
    FROM ml_predictions p
    JOIN ml_models m ON p.model_id = m.id
    LEFT JOIN categories c ON p.predicted_category_id = c.id
    WHERE p.transaction_id = ANY($1::uuid[])
      AND m.is_active = true
    ORDER BY p.confidence_score DESC
  `;
  const result = await pool.query(query, [transactionIds]);
  
  const predictionMap = {};
  result.rows.forEach(row => {
    if (!predictionMap[row.transaction_id] || 
        row.confidence_score > predictionMap[row.transaction_id].confidence_score) {
      predictionMap[row.transaction_id] = row;
    }
  });
  
  return transactionIds.map(id => predictionMap[id] || null);
});

// ============================================
// QUERY RESOLVERS
// ============================================

const resolvers = {
  Query: {
    // User & Profile
    me: async (parent, args, context) => {
      const query = `
        SELECT u.*,
               h.id as household_id,
               hm.role as household_role
        FROM users u
        LEFT JOIN household_members hm ON u.id = hm.user_id
        LEFT JOIN households h ON hm.household_id = h.id
        WHERE u.id = $1
      `;
      const result = await pool.query(query, [context.userId]);
      return result.rows[0];
    },

    // Accounts with filtering
    accounts: async (parent, { filter, includeInactive }, context) => {
      let query = `
        SELECT DISTINCT a.*
        FROM accounts a
        LEFT JOIN household_members hm ON a.household_id = hm.household_id
        WHERE (a.user_id = $1 OR hm.user_id = $1)
      `;
      
      const params = [context.userId];
      let paramCount = 1;
      
      if (!includeInactive) {
        query += ` AND a.is_active = true`;
      }
      
      if (filter) {
        if (filter.accountTypes?.length > 0) {
          paramCount++;
          query += ` AND a.account_type = ANY($${paramCount}::varchar[])`;
          params.push(filter.accountTypes);
        }
        
        if (filter.minBalance !== undefined) {
          paramCount++;
          query += ` AND a.balance >= $${paramCount}`;
          params.push(filter.minBalance);
        }
      }
      
      query += ` ORDER BY a.created_at DESC`;
      
      const result = await pool.query(query, params);
      return result.rows;
    },

    // Polymorphic budget query
    budget: async (parent, { id }, context) => {
      const budget = await budgetLoader.load(id);
      
      // Verify access
      if (budget.user_id !== context.userId && !budget.is_shared) {
        throw new Error('Access denied');
      }
      
      return budget;
    },

    // ML-powered transaction search
    searchTransactions: async (parent, { query: searchQuery, first }, context) => {
      const query = `
        SELECT t.*, 
               ts_rank(
                 to_tsvector('english', t.description || ' ' || COALESCE(t.notes, '')), 
                 plainto_tsquery('english', $1)
               ) as rank,
               c.name as category_name,
               p.name as payee_name
        FROM transactions t
        JOIN accounts a ON t.account_id = a.id
        LEFT JOIN categories c ON t.category_id = c.id
        LEFT JOIN payees p ON t.payee_id = p.id
        WHERE a.user_id = $2
          AND to_tsvector('english', t.description || ' ' || COALESCE(t.notes, '')) 
              @@ plainto_tsquery('english', $1)
        ORDER BY rank DESC, t.transaction_date DESC
        LIMIT $3
      `;
      
      const result = await pool.query(query, [searchQuery, context.userId, first]);
      return result.rows;
    },

    // Upcoming bills with predictions
    upcomingBills: async (parent, { daysAhead }, context) => {
      const query = `
        WITH bill_predictions AS (
          SELECT b.*,
                 AVG(bp.amount_paid) OVER (
                   PARTITION BY b.id 
                   ORDER BY bp.payment_date 
                   ROWS BETWEEN 3 PRECEDING AND 1 PRECEDING
                 ) as predicted_amount
          FROM bills b
          LEFT JOIN bill_payments bp ON b.id = bp.bill_id
          WHERE b.user_id = $1 
            AND b.is_active = true
        )
        SELECT DISTINCT ON (id) *,
               CASE 
                 WHEN next_due_date < CURRENT_DATE THEN true 
                 ELSE false 
               END as is_overdue,
               CASE 
                 WHEN next_due_date <= CURRENT_DATE + INTERVAL '1 day' * $2 THEN true 
                 ELSE false 
               END as due_soon
        FROM bill_predictions
        WHERE next_due_date <= CURRENT_DATE + INTERVAL '1 day' * $2
        ORDER BY id, next_due_date
      `;
      
      const result = await pool.query(query, [context.userId, daysAhead]);
      return result.rows;
    },

    // Debt payoff scenarios with different strategies
    calculatePayoffScenarios: async (parent, { debts, monthlyBudget }, context) => {
      const scenarios = [];
      
      // Calculate for each strategy
      for (const strategy of ['SNOWBALL', 'AVALANCHE']) {
        const projection = await calculatePayoffProjection(debts, monthlyBudget, strategy);
        scenarios.push(projection);
      }
      
      return scenarios;
    },

    // ML-powered insights
    insights: async (parent, { type, priority, dismissed }, context) => {
      let query = `
        SELECT si.*,
               array_agg(DISTINCT t.id) FILTER (WHERE t.id IS NOT NULL) as related_transaction_ids,
               array_agg(DISTINCT c.id) FILTER (WHERE c.id IS NOT NULL) as related_category_ids
        FROM spending_insights si
        LEFT JOIN transactions t ON t.id = ANY(si.related_transactions)
        LEFT JOIN categories c ON c.id = ANY(si.related_categories)
        WHERE si.user_id = $1
          AND si.dismissed = $2
          AND (si.valid_until IS NULL OR si.valid_until > CURRENT_TIMESTAMP)
      `;
      
      const params = [context.userId, dismissed];
      let paramCount = 2;
      
      if (type) {
        paramCount++;
        query += ` AND si.insight_type = $${paramCount}`;
        params.push(type);
      }
      
      if (priority) {
        paramCount++;
        query += ` AND si.priority = $${paramCount}`;
        params.push(priority);
      }
      
      query += ` GROUP BY si.id ORDER BY si.priority DESC, si.generated_at DESC`;
      
      const result = await pool.query(query, params);
      return result.rows;
    },

    // Cash flow forecast with ML
    cashFlowForecast: async (parent, { daysAhead }, context) => {
      const forecasts = [];
      
      for (let i = 0; i < daysAhead; i++) {
        const forecastDate = DateTime.now().plus({ days: i }).toISODate();
        
        // Get ML prediction
        const forecast = await generateCashFlowForecast(
          context.userId,
          forecastDate,
          pool
        );
        
        forecasts.push(forecast);
      }
      
      return forecasts;
    },

    // Event sourcing query
    events: async (parent, { aggregateId, from, to }, context) => {
      const query = `
        SELECT * FROM event_store
        WHERE aggregate_id = $1
          AND ($2::timestamp IS NULL OR occurred_at >= $2)
          AND ($3::timestamp IS NULL OR occurred_at <= $3)
        ORDER BY sequence_number
      `;
      
      const result = await pool.query(query, [aggregateId, from, to]);
      return result.rows;
    }
  },

  // ============================================
  // TYPE RESOLVERS
  // ============================================

  User: {
    accounts: async (user, { filter, first, after }, context) => {
      // Implementation with cursor-based pagination
      return getAccountConnection(user.id, filter, first, after);
    },

    netWorth: async (user, args, context) => {
      const query = `
        WITH account_values AS (
          SELECT 
            CASE 
              WHEN account_type IN ('checking', 'savings', 'investment') THEN balance
              ELSE 0
            END as assets,
            CASE 
              WHEN account_type IN ('credit_card', 'loan') THEN ABS(balance)
              ELSE 0
            END as liabilities
          FROM accounts
          WHERE user_id = $1 AND is_active = true
        )
        SELECT 
          SUM(assets) as total_assets,
          SUM(liabilities) as total_liabilities,
          SUM(assets) - SUM(liabilities) as net_worth
        FROM account_values
      `;
      
      const result = await pool.query(query, [user.id]);
      const data = result.rows[0];
      
      // Get historical trend
      const trendQuery = `
        SELECT 
          DATE(occurred_at) as date,
          (event_data->>'balance')::decimal as net_worth
        FROM event_store
        WHERE aggregate_type = 'NetWorth'
          AND user_id = $1
          AND occurred_at >= CURRENT_DATE - INTERVAL '30 days'
        ORDER BY occurred_at
      `;
      
      const trendResult = await pool.query(trendQuery, [user.id]);
      
      return {
        total: data.net_worth || 0,
        assets: data.total_assets || 0,
        liabilities: data.total_liabilities || 0,
        trend: trendResult.rows
      };
    },

    insights: async (user, { from, to, type }, context) => {
      return resolvers.Query.insights(null, { type, dismissed: false }, { userId: user.id });
    }
  },

  Account: {
    transactions: async (account, { filter, first, after, from, to }, context) => {
      return getTransactionConnection(account.id, filter, first, after, from, to);
    },

    monthlyAverage: async (account, { months }, context) => {
      const query = `
        SELECT AVG(monthly_total) as average
        FROM (
          SELECT DATE_TRUNC('month', transaction_date) as month,
                 SUM(CASE 
                   WHEN transaction_type = 'debit' THEN amount 
                   ELSE 0 
                 END) as monthly_total
          FROM transactions
          WHERE account_id = $1
            AND transaction_date >= CURRENT_DATE - INTERVAL '1 month' * $2
            AND status = 'cleared'
          GROUP BY DATE_TRUNC('month', transaction_date)
        ) monthly_totals
      `;
      
      const result = await pool.query(query, [account.id, months]);
      return result.rows[0].average || 0;
    },

    projectedBalance: async (account, { daysAhead }, context) => {
      // Use ML model for projection
      const projection = await mlProjectBalance(account, daysAhead);
      return projection;
    },

    auditTrail: async (account, { first }, context) => {
      const query = `
        SELECT * FROM audit_logs
        WHERE entity_type = 'accounts'
          AND entity_id = $1
        ORDER BY created_at DESC
        LIMIT $2
      `;
      
      const result = await pool.query(query, [account.id, first]);
      return result.rows;
    },

    snapshotAt: async (account, { timestamp }, context) => {
      const query = `
        SELECT * FROM event_store
        WHERE aggregate_id = $1
          AND aggregate_type = 'Account'
          AND occurred_at <= $2
        ORDER BY occurred_at DESC
      `;
      
      const events = await pool.query(query, [account.id, timestamp]);
      
      // Rebuild state from events
      const snapshot = rebuildAccountFromEvents(events.rows);
      return snapshot;
    }
  },

  Transaction: {
    account: async (transaction) => {
      return accountLoader.load(transaction.account_id);
    },

    category: async (transaction) => {
      if (!transaction.category_id) return null;
      return categoryLoader.load(transaction.category_id);
    },

    predictedCategory: async (transaction) => {
      const prediction = await mlPredictionLoader.load(transaction.id);
      if (!prediction) return null;
      return categoryLoader.load(prediction.predicted_category_id);
    },

    mlConfidenceScore: async (transaction) => {
      const prediction = await mlPredictionLoader.load(transaction.id);
      return prediction?.confidence_score || null;
    },

    envelopeAllocation: async (transaction) => {
      const query = `
        SELECT ea.*, e.name as envelope_name
        FROM envelope_allocations ea
        JOIN envelopes e ON ea.envelope_id = e.id
        WHERE ea.transaction_id = $1
      `;
      
      const result = await pool.query(query, [transaction.id]);
      return result.rows[0];
    },

    budgetImpact: async (transaction) => {
      // Calculate how this transaction affects budgets
      const query = `
        WITH active_budgets AS (
          SELECT b.*, cb.budgeted_amount
          FROM budgets b
          LEFT JOIN category_budgets cb ON cb.category_id = $2
          WHERE b.user_id = (
            SELECT user_id FROM accounts WHERE id = $1
          )
          AND b.is_active = true
          AND $3 BETWEEN b.start_date AND COALESCE(b.end_date, '9999-12-31')
        )
        SELECT 
          COUNT(*) as affected_budgets,
          SUM(CASE WHEN cb.budgeted_amount < spent.total THEN 1 ELSE 0 END) as over_budget_count
        FROM active_budgets ab
        LEFT JOIN (
          SELECT category_id, SUM(amount) as total
          FROM transactions
          WHERE category_id = $2
            AND transaction_date >= (SELECT start_date FROM active_budgets LIMIT 1)
          GROUP BY category_id
        ) spent ON true
      `;
      
      const result = await pool.query(query, [
        transaction.account_id,
        transaction.category_id,
        transaction.transaction_date
      ]);
      
      return result.rows[0];
    }
  },

  // Polymorphic Budget resolver
  Budget: {
    __resolveType(budget) {
      switch (budget.budget_type) {
        case 'envelope': return 'EnvelopeBudget';
        case 'traditional': return 'TraditionalBudget';
        case 'percentage': return 'PercentageBudget';
        case 'priority': return 'PriorityBudget';
        case 'value_based': return 'ValueBasedBudget';
        case 'hybrid': return 'HybridBudget';
        default: return 'TraditionalBudget';
      }
    }
  },

  EnvelopeBudget: {
    envelopes: async (budget) => {
      const query = `
        SELECT e.*,
               COALESCE(SUM(ea.amount), 0) as spent_amount
        FROM envelopes e
        LEFT JOIN envelope_allocations ea ON e.id = ea.envelope_id
        WHERE e.envelope_budget_id = (
          SELECT id FROM envelope_budgets WHERE budget_id = $1
        )
        GROUP BY e.id
        ORDER BY e.name
      `;
      
      const result = await pool.query(query, [budget.id]);
      return result.rows.map(envelope => ({
        ...envelope,
        remainingAmount: envelope.allocated_amount - envelope.spent_amount,
        percentageUsed: envelope.allocated_amount > 0 
          ? (envelope.spent_amount / envelope.allocated_amount) * 100 
          : 0
      }));
    },

    currentPerformance: async (budget) => {
      return calculateBudgetPerformance(budget.id, 'envelope');
    }
  },

  // ============================================
  // MUTATION RESOLVERS
  // ============================================

  Mutation: {
    createTransaction: async (parent, { input }, context) => {
      const client = await pool.connect();
      
      try {
        await client.query('BEGIN');
        
        // Set user context for audit
        await client.query(`SET LOCAL app.current_user_id = $1`, [context.userId]);
        
        // Insert transaction
        const insertQuery = `
          INSERT INTO transactions (
            account_id, amount, transaction_date, description,
            transaction_type, category_id, payee_id, notes
          ) VALUES ($1, $2, $3, $4, $5, $6, $7, $8)
          RETURNING *
        `;
        
        const result = await client.query(insertQuery, [
          input.accountId,
          input.amount,
          input.transactionDate,
          input.description,
          input.transactionType,
          input.categoryId,
          input.payeeId,
          input.notes
        ]);
        
        const transaction = result.rows[0];
        
        // Update account balance
        const balanceUpdate = input.transactionType === 'DEBIT' 
          ? -input.amount 
          : input.amount;
        
        await client.query(
          `UPDATE accounts SET balance = balance + $1 WHERE id = $2`,
          [balanceUpdate, input.accountId]
        );
        
        // Create event for event store
        await client.query(`
          INSERT INTO event_store (
            aggregate_id, aggregate_type, event_type, event_data, user_id
          ) VALUES ($1, $2, $3, $4, $5)
        `, [
          transaction.id,
          'Transaction',
          'TransactionCreated',
          JSON.stringify(transaction),
          context.userId
        ]);
        
        // Trigger ML categorization asynchronously
        await client.query(`
          INSERT INTO ml_training_queue (model_type, training_data, priority)
          VALUES ('category_prediction', $1, 5)
        `, [JSON.stringify({ transactionId: transaction.id })]);
        
        await client.query('COMMIT');
        
        // Publish subscription event
        pubsub.publish('TRANSACTION_ADDED', {
          transactionAdded: transaction,
          accountId: input.accountId
        });
        
        return transaction;
      } catch (error) {
        await client.query('ROLLBACK');
        throw error;
      } finally {
        client.release();
      }
    },

    createEnvelopeBudget: async (parent, { input }, context) => {
      const client = await pool.connect();
      
      try {
        await client.query('BEGIN');
        
        // Create base budget
        const budgetResult = await client.query(`
          INSERT INTO budgets (user_id, name, budget_type, period_type, start_date, is_active)
          VALUES ($1, $2, 'envelope', $3, CURRENT_DATE, true)
          RETURNING *
        `, [context.userId, input.name, input.periodType]);
        
        const budget = budgetResult.rows[0];
        
        // Create envelope budget specifics
        await client.query(`
          INSERT INTO envelope_budgets (budget_id, strict_mode, allow_borrowing)
          VALUES ($1, $2, $3)
        `, [budget.id, input.strictMode, input.allowBorrowing]);
        
        // Create envelopes
        let totalAllocated = 0;
        for (const envelopeInput of input.envelopes) {
          await client.query(`
            INSERT INTO envelopes (
              envelope_budget_id, name, allocated_amount, 
              envelope_category, rollover_enabled, color, icon
            ) VALUES (
              (SELECT id FROM envelope_budgets WHERE budget_id = $1),
              $2, $3, $4, $5, $6, $7
            )
          `, [
            budget.id,
            envelopeInput.name,
            envelopeInput.allocatedAmount,
            envelopeInput.category,
            envelopeInput.rolloverEnabled,
            envelopeInput.color,
            envelopeInput.icon
          ]);
          
          totalAllocated += parseFloat(envelopeInput.allocatedAmount);
        }
        
        // Update total allocated
        await client.query(`
          UPDATE envelope_budgets SET total_allocated = $1 WHERE budget_id = $2
        `, [totalAllocated, budget.id]);
        
        await client.query('COMMIT');
        
        return { ...budget, budget_type: 'envelope' };
      } catch (error) {
        await client.query('ROLLBACK');
        throw error;
      } finally {
        client.release();
      }
    },

    confirmMLPrediction: async (parent, { predictionId, isCorrect }, context) => {
      const query = `
        UPDATE ml_predictions
        SET accepted_by_user = $1,
            feedback = $2
        WHERE id = $3
        RETURNING *
      `;
      
      const feedback = isCorrect ? 'correct' : 'incorrect';
      const result = await pool.query(query, [isCorrect, feedback, predictionId]);
      
      // Queue model retraining if needed
      if (!isCorrect) {
        await pool.query(`
          INSERT INTO ml_training_queue (model_type, training_data, priority)
          VALUES ('category_prediction', $1, 3)
        `, [JSON.stringify({ predictionId, feedback })]);
      }
      
      return result.rows[0];
    }
  },

  // ============================================
  // SUBSCRIPTION RESOLVERS
  // ============================================

  Subscription: {
    transactionAdded: {
      subscribe: (parent, { accountId }, context) => {
        const channel = accountId 
          ? `TRANSACTION_ADDED_${accountId}`
          : 'TRANSACTION_ADDED';
        
        return pubsub.asyncIterator(channel);
      }
    },

    budgetThresholdReached: {
      subscribe: (parent, { budgetId }, context) => {
        return pubsub.asyncIterator(`BUDGET_ALERT_${budgetId}`);
      }
    },

    anomalyDetected: {
      subscribe: (parent, args, context) => {
        return pubsub.asyncIterator(`ANOMALY_${context.userId}`);
      }
    }
  }
};

// ============================================
// HELPER FUNCTIONS
// ============================================

async function calculatePayoffProjection(debts, monthlyBudget, strategy) {
  // Complex calculation for debt payoff
  // Implementation depends on strategy (snowball vs avalanche)
  const orderedDebts = strategy === 'SNOWBALL' 
    ? debts.sort((a, b) => a.currentBalance - b.currentBalance)
    : debts.sort((a, b) => b.interestRate - a.interestRate);
  
  let projections = [];
  let currentMonth = 0;
  let totalInterest = 0;
  
  // Calculate payoff timeline
  // ... complex calculation logic ...
  
  return {
    strategy,
    totalPayoffDate: '2025-12-31', // calculated
    totalInterestPaid: totalInterest,
    monthsToPayoff: currentMonth,
    monthlyProjections: projections
  };
}

async function generateCashFlowForecast(userId, forecastDate, pool) {
  // Load ML model
  const modelQuery = `
    SELECT model_data FROM ml_models 
    WHERE model_type = 'cash_flow_forecast' 
      AND is_active = true 
    LIMIT 1
  `;
  
  const modelResult = await pool.query(modelQuery);
  
  if (modelResult.rows.length > 0) {
    // Use TensorFlow.js for prediction
    const model = await tf.loadLayersModel(modelResult.rows[0].model_data);
    
    // Prepare features
    const features = await prepareCashFlowFeatures(userId, forecastDate, pool);
    
    // Make prediction
    const prediction = model.predict(features);
    
    return {
      forecastDate,
      predictedIncome: prediction.income,
      predictedExpenses: prediction.expenses,
      predictedBalance: prediction.balance,
      confidenceLevel: prediction.confidence
    };
  }
  
  // Fallback to statistical prediction
  return statisticalCashFlowForecast(userId, forecastDate, pool);
}

async function calculateBudgetPerformance(budgetId, budgetType) {
  // Different calculation based on budget type
  const query = `
    SELECT * FROM calculate_budget_performance($1, 
      (SELECT start_date FROM budgets WHERE id = $1),
      CURRENT_DATE
    )
  `;
  
  const result = await pool.query(query, [budgetId]);
  
  return {
    totalBudgeted: result.rows.reduce((sum, r) => sum + r.budgeted, 0),
    totalSpent: result.rows.reduce((sum, r) => sum + r.spent, 0),
    totalRemaining: result.rows.reduce((sum, r) => sum + r.remaining, 0),
    categories: result.rows
  };
}

export default resolvers;
```

Perfect! I've now created all three layers showing how GraphQL and PostgreSQL work together with all the new features:

## The Three-Layer Architecture

### **1. PostgreSQL Schema (Database Layer)**

- **Event Store**: Immutable audit trail with complete context
- **Flexible Budget Tables**: Supports 7 different budget strategies with polymorphic design
- **ML Tables**: Stores models, predictions, and training data
- **Household Management**: Multi-user access with permissions
- **Debt & Bills**: Comprehensive tracking with payment history
- **Materialized Views**: Pre-computed aggregations for performance
- **Functions & Triggers**: Automatic audit logging, immutability enforcement
- **Row-Level Security**: Multi-tenant data isolation

### **2. GraphQL Schema (API Layer)**

- **Polymorphic Types**: `Budget` interface with 7 implementations
- **Rich Type System**: 100+ types covering all features
- **Connections**: Cursor-based pagination for large datasets
- **Computed Fields**: Real-time calculations like `percentageUsed`, `projectedBalance`
- **Subscriptions**: Real-time updates for transactions, alerts, anomalies
- **Input Types**: Structured mutations with validation

### **3. Resolver Layer (Business Logic)**

- **DataLoaders**: Batch database queries to prevent N+1 problems
- **ML Integration**: TensorFlow.js for predictions and insights
- **Event Publishing**: Real-time updates via GraphQL subscriptions
- **Transaction Management**: ACID compliance for financial operations
- **Polymorphic Resolution**: Dynamic type resolution for budgets
- **Performance Optimization**: Caching, query optimization, connection pooling

## Key Integration Points

### **Event Sourcing Flow**

```
User Action → Mutation → PostgreSQL Transaction → Event Store → Audit Log → Subscription
```

### **ML Pipeline**

```
Transaction Created → ML Queue → Model Prediction → Auto-Categorization → User Feedback → Model Retraining
```

### **Budget Flexibility**

```
GraphQL Interface → Resolver Type Check → Specific Budget Table → Performance Calculation
```

This complete architecture provides:

- **Complete audit trail** via event sourcing
- **Intelligent automation** via ML
- **Flexible budgeting** with 7 strategies
- **Family collaboration** via households
- **Real-time updates** via subscriptions
- **Performance at scale** via DataLoaders and materialized views

The beauty is that each layer can evolve independently while maintaining clean contracts through the GraphQL schema!

**human** (Aug 29, 2025, 10:36 PM)

Could we re-do the resolvers section in C# with HotChocolate, and go through how all this would work on a hexagonal architecture using mediatr, defining the entities and operations needed at each layer?

**assistant** (Aug 29, 2025, 10:42 PM)

 Absolutely! Let me show you how to implement this complete system in C# with HotChocolate, MediatR, and hexagonal architecture:

#### create Complete .NET Hexagonal Architecture with HotChocolate

```csharp
// ============================================
// COMPLETE HEXAGONAL ARCHITECTURE FOR PERSONAL FINANCE APP
// .NET 8, HotChocolate, MediatR, Event Sourcing, ML.NET
// ============================================

// ============================================
// SOLUTION STRUCTURE
// ============================================
/*
FinanceTracker/
├── Core/
│   ├── FinanceTracker.Domain/              # Enterprise Business Rules
│   │   ├── Aggregates/
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Events/
│   │   ├── Exceptions/
│   │   └── Specifications/
│   │
│   └── FinanceTracker.Application/         # Application Business Rules
│       ├── Common/
│       │   ├── Interfaces/
│       │   ├── Behaviors/
│       │   └── Exceptions/
│       ├── Accounts/
│       │   ├── Commands/
│       │   ├── Queries/
│       │   └── EventHandlers/
│       ├── Budgets/
│       ├── Transactions/
│       ├── ML/
│       └── Insights/
│
├── Infrastructure/
│   ├── FinanceTracker.Persistence/         # Database
│   │   ├── Configurations/
│   │   ├── Repositories/
│   │   └── EventStore/
│   │
│   ├── FinanceTracker.ML/                  # Machine Learning
│   │   ├── Models/
│   │   ├── Training/
│   │   └── Prediction/
│   │
│   └── FinanceTracker.External/            # External Services
│       ├── BankingAPIs/
│       └── NotificationServices/
│
├── Presentation/
│   ├── FinanceTracker.GraphQL/             # GraphQL API
│   │   ├── Types/
│   │   ├── Queries/
│   │   ├── Mutations/
│   │   ├── Subscriptions/
│   │   └── DataLoaders/
│   │
│   └── FinanceTracker.WebAPI/              # Host
│       └── Program.cs
│
└── Tests/
    ├── Domain.Tests/
    ├── Application.Tests/
    └── Integration.Tests/
*/

// ============================================
// DOMAIN LAYER - Core Business Logic (Center of Hexagon)
// ============================================

// Domain/SharedKernel/Entity.cs
namespace FinanceTracker.Domain.SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

// Domain/SharedKernel/AggregateRoot.cs
namespace FinanceTracker.Domain.SharedKernel;

public abstract class AggregateRoot : Entity, IEventSourcedAggregate
{
    private readonly List<IDomainEvent> _uncommittedEvents = new();
    
    public int Version { get; protected set; }
    
    public IEnumerable<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents;
    
    public void MarkEventsAsCommitted()
    {
        _uncommittedEvents.Clear();
    }
    
    protected void RaiseEvent(IDomainEvent @event)
    {
        _uncommittedEvents.Add(@event);
        AddDomainEvent(@event);
        ApplyEvent(@event);
        Version++;
    }
    
    public void LoadFromHistory(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyEvent(@event);
            Version++;
        }
    }
    
    protected abstract void ApplyEvent(IDomainEvent @event);
}

// Domain/ValueObjects/Money.cs
namespace FinanceTracker.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    
    public Money(decimal amount, string currency = "USD")
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required", nameof(currency));
        
        Amount = Math.Round(amount, 2);
        Currency = currency.ToUpperInvariant();
    }
    
    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }
    
    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount - other.Amount, Currency);
    }
    
    public Money Multiply(decimal factor)
    {
        return new Money(Amount * factor, Currency);
    }
    
    public static Money Zero(string currency = "USD") => new(0, currency);
    
    public bool IsNegative => Amount < 0;
    public bool IsPositive => Amount > 0;
    public bool IsZero => Amount == 0;
    
    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot operate on different currencies: {Currency} and {other.Currency}");
    }
    
    public static implicit operator decimal(Money money) => money.Amount;
}

// Domain/Aggregates/Account.cs
namespace FinanceTracker.Domain.Aggregates;

public class Account : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid? HouseholdId { get; private set; }
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public Money Balance { get; private set; }
    public string? Institution { get; private set; }
    public decimal? InterestRate { get; private set; }
    public Money? CreditLimit { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsShared { get; private set; }
    
    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
    
    private Account() { } // For EF Core
    
    public static Account Create(
        Guid userId,
        string name,
        AccountType type,
        Money initialBalance,
        string? institution = null)
    {
        var account = new Account();
        
        var @event = new AccountCreatedEvent(
            Guid.NewGuid(),
            userId,
            name,
            type,
            initialBalance,
            institution);
        
        account.RaiseEvent(@event);
        return account;
    }
    
    public Transaction AddTransaction(
        Money amount,
        DateTime date,
        string description,
        TransactionType type,
        Guid? categoryId = null)
    {
        if (!IsActive)
            throw new DomainException("Cannot add transactions to inactive account");
        
        var newBalance = type == TransactionType.Debit 
            ? Balance.Subtract(amount)
            : Balance.Add(amount);
        
        ValidateBalance(newBalance);
        
        var transaction = Transaction.Create(
            Id,
            amount,
            date,
            description,
            type,
            categoryId);
        
        _transactions.Add(transaction);
        
        var @event = new TransactionAddedEvent(
            Id,
            transaction.Id,
            amount,
            type,
            newBalance,
            date);
        
        RaiseEvent(@event);
        
        return transaction;
    }
    
    public void ShareWithHousehold(Guid householdId)
    {
        if (IsShared)
            throw new DomainException("Account is already shared");
        
        RaiseEvent(new AccountSharedEvent(Id, householdId));
    }
    
    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case AccountCreatedEvent e:
                Id = e.AccountId;
                UserId = e.UserId;
                Name = e.Name;
                Type = e.AccountType;
                Balance = e.InitialBalance;
                Institution = e.Institution;
                IsActive = true;
                IsShared = false;
                break;
                
            case TransactionAddedEvent e:
                Balance = e.NewBalance;
                UpdatedAt = e.OccurredAt;
                break;
                
            case AccountSharedEvent e:
                HouseholdId = e.HouseholdId;
                IsShared = true;
                break;
        }
    }
    
    private void ValidateBalance(Money newBalance)
    {
        if (Type == AccountType.CreditCard && CreditLimit != null)
        {
            if (newBalance.Amount < -CreditLimit.Amount)
                throw new InsufficientFundsException("Transaction would exceed credit limit");
        }
        else if (Type == AccountType.Checking && newBalance.IsNegative)
        {
            throw new InsufficientFundsException("Insufficient funds");
        }
    }
}

// Domain/Aggregates/Budget.cs (Base for all budget types)
namespace FinanceTracker.Domain.Aggregates;

public abstract class Budget : AggregateRoot
{
    public Guid UserId { get; protected set; }
    public Guid? HouseholdId { get; protected set; }
    public string Name { get; protected set; }
    public BudgetType BudgetType { get; protected set; }
    public BudgetPeriod Period { get; protected set; }
    public DateTime StartDate { get; protected set; }
    public DateTime? EndDate { get; protected set; }
    public bool IsActive { get; protected set; }
    public bool IsShared { get; protected set; }
    
    public abstract BudgetAllocation AllocateTransaction(Transaction transaction);
    public abstract BudgetPerformance CalculatePerformance(IEnumerable<Transaction> transactions);
    public abstract IEnumerable<BudgetRecommendation> GenerateRecommendations();
    public abstract void MigrateToStrategy(BudgetType newType, Dictionary<string, object> settings);
}

// Domain/Aggregates/EnvelopeBudget.cs
namespace FinanceTracker.Domain.Aggregates;

public class EnvelopeBudget : Budget
{
    private readonly List<Envelope> _envelopes = new();
    public IReadOnlyCollection<Envelope> Envelopes => _envelopes.AsReadOnly();
    
    public bool StrictMode { get; private set; }
    public bool AllowBorrowing { get; private set; }
    public Money TotalAllocated { get; private set; }
    
    private EnvelopeBudget() { } // For EF Core
    
    public static EnvelopeBudget Create(
        Guid userId,
        string name,
        BudgetPeriod period,
        bool strictMode = true)
    {
        var budget = new EnvelopeBudget();
        
        var @event = new EnvelopeBudgetCreatedEvent(
            Guid.NewGuid(),
            userId,
            name,
            period,
            strictMode);
        
        budget.RaiseEvent(@event);
        return budget;
    }
    
    public Envelope AddEnvelope(
        string name,
        Money allocatedAmount,
        EnvelopeCategory category,
        bool rolloverEnabled = false)
    {
        var envelope = Envelope.Create(Id, name, allocatedAmount, category, rolloverEnabled);
        _envelopes.Add(envelope);
        
        TotalAllocated = TotalAllocated.Add(allocatedAmount);
        
        RaiseEvent(new EnvelopeAddedEvent(Id, envelope.Id, name, allocatedAmount));
        
        return envelope;
    }
    
    public void TransferBetweenEnvelopes(Guid fromId, Guid toId, Money amount)
    {
        var fromEnvelope = _envelopes.FirstOrDefault(e => e.Id == fromId);
        var toEnvelope = _envelopes.FirstOrDefault(e => e.Id == toId);
        
        if (fromEnvelope == null || toEnvelope == null)
            throw new DomainException("Envelope not found");
        
        if (!AllowBorrowing && fromEnvelope.RemainingAmount < amount)
            throw new InsufficientFundsException($"Insufficient funds in envelope {fromEnvelope.Name}");
        
        fromEnvelope.Withdraw(amount);
        toEnvelope.Deposit(amount);
        
        RaiseEvent(new EnvelopeTransferEvent(fromId, toId, amount));
    }
    
    public override BudgetAllocation AllocateTransaction(Transaction transaction)
    {
        var envelope = FindBestEnvelope(transaction);
        
        if (envelope == null)
            return BudgetAllocation.Unallocated(transaction.Id);
        
        if (StrictMode && envelope.RemainingAmount < transaction.Amount)
        {
            return BudgetAllocation.Rejected(
                transaction.Id,
                $"Insufficient funds in envelope {envelope.Name}");
        }
        
        envelope.AllocateTransaction(transaction.Id, transaction.Amount);
        
        RaiseEvent(new TransactionAllocatedToEnvelopeEvent(
            transaction.Id,
            envelope.Id,
            transaction.Amount));
        
        return BudgetAllocation.Allocated(
            transaction.Id,
            envelope.Id,
            envelope.RemainingAmount);
    }
    
    public override BudgetPerformance CalculatePerformance(IEnumerable<Transaction> transactions)
    {
        var performance = new EnvelopeBudgetPerformance
        {
            BudgetId = Id,
            Period = new DateRange(StartDate, EndDate ?? DateTime.UtcNow),
            TotalAllocated = TotalAllocated,
            TotalSpent = _envelopes.Sum(e => e.SpentAmount),
            TotalRemaining = _envelopes.Sum(e => e.RemainingAmount),
            EnvelopePerformances = _envelopes.Select(e => new EnvelopePerformance
            {
                EnvelopeId = e.Id,
                Name = e.Name,
                Allocated = e.AllocatedAmount,
                Spent = e.SpentAmount,
                Remaining = e.RemainingAmount,
                PercentageUsed = e.PercentageUsed
            }).ToList()
        };
        
        return performance;
    }
    
    protected override void ApplyEvent(IDomainEvent @event)
    {
        switch (@event)
        {
            case EnvelopeBudgetCreatedEvent e:
                Id = e.BudgetId;
                UserId = e.UserId;
                Name = e.Name;
                BudgetType = BudgetType.Envelope;
                Period = e.Period;
                StrictMode = e.StrictMode;
                AllowBorrowing = !e.StrictMode;
                StartDate = e.OccurredAt;
                IsActive = true;
                TotalAllocated = Money.Zero();
                break;
                
            case EnvelopeAddedEvent e:
                TotalAllocated = TotalAllocated.Add(e.AllocatedAmount);
                break;
        }
    }
    
    private Envelope? FindBestEnvelope(Transaction transaction)
    {
        // ML-powered envelope matching
        return _envelopes.FirstOrDefault(e => 
            e.LinkedCategories.Contains(transaction.CategoryId ?? Guid.Empty));
    }
}

// Domain/Entities/Envelope.cs
namespace FinanceTracker.Domain.Entities;

public class Envelope : Entity
{
    public Guid BudgetId { get; private set; }
    public string Name { get; private set; }
    public Money AllocatedAmount { get; private set; }
    public Money SpentAmount { get; private set; }
    public Money RemainingAmount => AllocatedAmount.Subtract(SpentAmount);
    public decimal PercentageUsed => AllocatedAmount.Amount > 0 
        ? (SpentAmount.Amount / AllocatedAmount.Amount) * 100 
        : 0;
    public EnvelopeCategory Category { get; private set; }
    public bool RolloverEnabled { get; private set; }
    public List<Guid> LinkedCategories { get; private set; } = new();
    
    private readonly List<EnvelopeTransaction> _transactions = new();
    public IReadOnlyCollection<EnvelopeTransaction> Transactions => _transactions.AsReadOnly();
    
    internal static Envelope Create(
        Guid budgetId,
        string name,
        Money allocatedAmount,
        EnvelopeCategory category,
        bool rolloverEnabled)
    {
        return new Envelope
        {
            Id = Guid.NewGuid(),
            BudgetId = budgetId,
            Name = name,
            AllocatedAmount = allocatedAmount,
            SpentAmount = Money.Zero(allocatedAmount.Currency),
            Category = category,
            RolloverEnabled = rolloverEnabled
        };
    }
    
    public void AllocateTransaction(Guid transactionId, Money amount)
    {
        if (amount > RemainingAmount)
            throw new InsufficientFundsException($"Insufficient funds in envelope {Name}");
        
        SpentAmount = SpentAmount.Add(amount);
        _transactions.Add(new EnvelopeTransaction(transactionId, amount));
    }
    
    public void Withdraw(Money amount)
    {
        AllocatedAmount = AllocatedAmount.Subtract(amount);
    }
    
    public void Deposit(Money amount)
    {
        AllocatedAmount = AllocatedAmount.Add(amount);
    }
}

// Domain/Events/AccountEvents.cs
namespace FinanceTracker.Domain.Events;

public record AccountCreatedEvent(
    Guid AccountId,
    Guid UserId,
    string Name,
    AccountType AccountType,
    Money InitialBalance,
    string? Institution) : IDomainEvent;

public record TransactionAddedEvent(
    Guid AccountId,
    Guid TransactionId,
    Money Amount,
    TransactionType Type,
    Money NewBalance,
    DateTime TransactionDate) : IDomainEvent;

public record AccountSharedEvent(
    Guid AccountId,
    Guid HouseholdId) : IDomainEvent;

// Domain/Events/BudgetEvents.cs
public record EnvelopeBudgetCreatedEvent(
    Guid BudgetId,
    Guid UserId,
    string Name,
    BudgetPeriod Period,
    bool StrictMode) : IDomainEvent;

public record EnvelopeAddedEvent(
    Guid BudgetId,
    Guid EnvelopeId,
    string Name,
    Money AllocatedAmount) : IDomainEvent;

public record EnvelopeTransferEvent(
    Guid FromEnvelopeId,
    Guid ToEnvelopeId,
    Money Amount) : IDomainEvent;

public record TransactionAllocatedToEnvelopeEvent(
    Guid TransactionId,
    Guid EnvelopeId,
    Money Amount) : IDomainEvent;

// ============================================
// APPLICATION LAYER - Use Cases (Ports)
// ============================================

// Application/Common/Interfaces/IEventStore.cs
namespace FinanceTracker.Application.Common.Interfaces;

public interface IEventStore
{
    Task SaveEventAsync<T>(T @event, CancellationToken cancellationToken = default) 
        where T : IDomainEvent;
    
    Task SaveEventsAsync<T>(IEnumerable<T> events, CancellationToken cancellationToken = default) 
        where T : IDomainEvent;
    
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(
        Guid aggregateId, 
        CancellationToken cancellationToken = default);
    
    Task<T?> GetAggregateAsync<T>(
        Guid aggregateId, 
        CancellationToken cancellationToken = default) 
        where T : class, IEventSourcedAggregate, new();
}

// Application/Common/Interfaces/IMLPredictionService.cs
public interface IMLPredictionService
{
    Task<CategoryPrediction> PredictCategoryAsync(
        Transaction transaction,
        CancellationToken cancellationToken = default);
    
    Task<AnomalyDetectionResult> DetectAnomaliesAsync(
        IEnumerable<Transaction> transactions,
        CancellationToken cancellationToken = default);
    
    Task<CashFlowForecast> ForecastCashFlowAsync(
        Guid userId,
        int daysAhead,
        CancellationToken cancellationToken = default);
}

// Application/Common/Behaviors/LoggingBehavior.cs
namespace FinanceTracker.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUser;
    
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.UserId;
        
        _logger.LogInformation(
            "Handling {RequestName} for user {UserId} with data {@Request}",
            requestName, userId, request);
        
        var stopwatch = Stopwatch.StartNew();
        var response = await next();
        stopwatch.Stop();
        
        _logger.LogInformation(
            "Handled {RequestName} in {ElapsedMilliseconds}ms",
            requestName, stopwatch.ElapsedMilliseconds);
        
        return response;
    }
}

// Application/Common/Behaviors/ValidationBehavior.cs
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();
            
            if (failures.Count != 0)
                throw new ValidationException(failures);
        }
        
        return await next();
    }
}

// Application/Accounts/Commands/CreateAccountCommand.cs
namespace FinanceTracker.Application.Accounts.Commands;

public record CreateAccountCommand(
    string Name,
    AccountType AccountType,
    decimal InitialBalance,
    string Currency,
    string? Institution) : IRequest<AccountDto>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IEventStore _eventStore;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    
    public CreateAccountCommandHandler(
        IEventStore eventStore,
        ICurrentUserService currentUser,
        IMapper mapper,
        IPublisher publisher)
    {
        _eventStore = eventStore;
        _currentUser = currentUser;
        _mapper = mapper;
        _publisher = publisher;
    }
    
    public async Task<AccountDto> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        var money = new Money(request.InitialBalance, request.Currency);
        
        var account = Account.Create(
            _currentUser.UserId,
            request.Name,
            request.AccountType,
            money,
            request.Institution);
        
        // Save events to event store
        await _eventStore.SaveEventsAsync(
            account.GetUncommittedEvents(),
            cancellationToken);
        
        // Publish domain events for other handlers
        foreach (var @event in account.DomainEvents)
        {
            await _publisher.Publish(@event, cancellationToken);
        }
        
        return _mapper.Map<AccountDto>(account);
    }
}

// Application/Accounts/Commands/CreateAccountCommandValidator.cs
public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Account name is required")
            .MaximumLength(255).WithMessage("Account name must not exceed 255 characters");
        
        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0).When(x => x.AccountType != AccountType.CreditCard)
            .WithMessage("Initial balance must be non-negative for this account type");
        
        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3)
            .Matches("^[A-Z]{3}$").WithMessage("Currency must be a valid 3-letter ISO code");
    }
}

// Application/Transactions/Commands/CreateTransactionCommand.cs
namespace FinanceTracker.Application.Transactions.Commands;

public record CreateTransactionCommand(
    Guid AccountId,
    decimal Amount,
    DateTime TransactionDate,
    string Description,
    TransactionType Type,
    Guid? CategoryId,
    Guid? PayeeId,
    string? Notes) : IRequest<TransactionDto>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly IEventStore _eventStore;
    private readonly IMLPredictionService _mlService;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    
    public CreateTransactionCommandHandler(
        IEventStore eventStore,
        IMLPredictionService mlService,
        IMapper mapper,
        IPublisher publisher)
    {
        _eventStore = eventStore;
        _mlService = mlService;
        _mapper = mapper;
        _publisher = publisher;
    }
    
    public async Task<TransactionDto> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        // Load account aggregate from event store
        var account = await _eventStore.GetAggregateAsync<Account>(
            request.AccountId,
            cancellationToken);
        
        if (account == null)
            throw new NotFoundException($"Account {request.AccountId} not found");
        
        var money = new Money(request.Amount, account.Balance.Currency);
        
        // Add transaction to account
        var transaction = account.AddTransaction(
            money,
            request.TransactionDate,
            request.Description,
            request.Type,
            request.CategoryId);
        
        // Save events
        await _eventStore.SaveEventsAsync(
            account.GetUncommittedEvents(),
            cancellationToken);
        
        // ML Category Prediction (async, non-blocking)
        _ = Task.Run(async () =>
        {
            if (!request.CategoryId.HasValue)
            {
                var prediction = await _mlService.PredictCategoryAsync(
                    transaction,
                    cancellationToken);
                
                if (prediction.Confidence > 0.85)
                {
                    await _publisher.Publish(
                        new TransactionAutoCategorizedNotification(
                            transaction.Id,
                            prediction.CategoryId,
                            prediction.Confidence),
                        cancellationToken);
                }
            }
        }, cancellationToken);
        
        // Publish events
        foreach (var @event in account.DomainEvents)
        {
            await _publisher.Publish(@event, cancellationToken);
        }
        
        return _mapper.Map<TransactionDto>(transaction);
    }
}

// Application/Budgets/Commands/CreateEnvelopeBudgetCommand.cs
namespace FinanceTracker.Application.Budgets.Commands;

public record CreateEnvelopeBudgetCommand(
    string Name,
    BudgetPeriod Period,
    bool StrictMode,
    List<CreateEnvelopeDto> Envelopes) : IRequest<EnvelopeBudgetDto>;

public record CreateEnvelopeDto(
    string Name,
    decimal AllocatedAmount,
    string Currency,
    EnvelopeCategory Category,
    bool RolloverEnabled);

public class CreateEnvelopeBudgetCommandHandler : IRequestHandler<CreateEnvelopeBudgetCommand, EnvelopeBudgetDto>
{
    private readonly IEventStore _eventStore;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;
    
    public CreateEnvelopeBudgetCommandHandler(
        IEventStore eventStore,
        ICurrentUserService currentUser,
        IMapper mapper)
    {
        _eventStore = eventStore;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    
    public async Task<EnvelopeBudgetDto> Handle(
        CreateEnvelopeBudgetCommand request,
        CancellationToken cancellationToken)
    {
        var budget = EnvelopeBudget.Create(
            _currentUser.UserId,
            request.Name,
            request.Period,
            request.StrictMode);
        
        // Add envelopes
        foreach (var envelopeDto in request.Envelopes)
        {
            var money = new Money(envelopeDto.AllocatedAmount, envelopeDto.Currency);
            budget.AddEnvelope(
                envelopeDto.Name,
                money,
                envelopeDto.Category,
                envelopeDto.RolloverEnabled);
        }
        
        // Save events
        await _eventStore.SaveEventsAsync(
            budget.GetUncommittedEvents(),
            cancellationToken);
        
        return _mapper.Map<EnvelopeBudgetDto>(budget);
    }
}

// Application/Insights/Queries/GetSpendingInsightsQuery.cs
namespace FinanceTracker.Application.Insights.Queries;

public record GetSpendingInsightsQuery(
    DateTime? From,
    DateTime? To,
    InsightType? Type) : IRequest<List<SpendingInsightDto>>;

public class GetSpendingInsightsQueryHandler : IRequestHandler<GetSpendingInsightsQuery, List<SpendingInsightDto>>
{
    private readonly IInsightService _insightService;
    private readonly ICurrentUserService _currentUser;
    
    public GetSpendingInsightsQueryHandler(
        IInsightService insightService,
        ICurrentUserService currentUser)
    {
        _insightService = insightService;
        _currentUser = currentUser;
    }
    
    public async Task<List<SpendingInsightDto>> Handle(
        GetSpendingInsightsQuery request,
        CancellationToken cancellationToken)
    {
        var insights = await _insightService.GenerateInsightsAsync(
            _currentUser.UserId,
            request.From ?? DateTime.UtcNow.AddMonths(-1),
            request.To ?? DateTime.UtcNow,
            request.Type,
            cancellationToken);
        
        return insights;
    }
}

// ============================================
// INFRASTRUCTURE LAYER - Adapters (Outside of Hexagon)
// ============================================

// Infrastructure/Persistence/EventStore/PostgresEventStore.cs
namespace FinanceTracker.Infrastructure.Persistence.EventStore;

public class PostgresEventStore : IEventStore
{
    private readonly FinanceContext _context;
    private readonly IEventSerializer _serializer;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PostgresEventStore(
        FinanceContext context,
        IEventSerializer serializer,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _serializer = serializer;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task SaveEventAsync<T>(T @event, CancellationToken cancellationToken = default) 
        where T : IDomainEvent
    {
        var eventEntity = new EventStoreEntity
        {
            EventId = Guid.NewGuid(),
            AggregateId = ExtractAggregateId(@event),
            AggregateType = ExtractAggregateType(@event),
            EventType = @event.GetType().Name,
            EventData = _serializer.Serialize(@event),
            EventMetadata = CaptureMetadata(),
            OccurredAt = @event.OccurredAt,
            UserId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
        };
        
        _context.EventStore.Add(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<T?> GetAggregateAsync<T>(
        Guid aggregateId,
        CancellationToken cancellationToken = default) 
        where T : class, IEventSourcedAggregate, new()
    {
        var events = await _context.EventStore
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.SequenceNumber)
            .Select(e => _serializer.Deserialize(e.EventType, e.EventData))
            .ToListAsync(cancellationToken);
        
        if (!events.Any())
            return null;
        
        var aggregate = new T();
        aggregate.LoadFromHistory(events);
        return aggregate;
    }
    
    private Dictionary<string, object> CaptureMetadata()
    {
        var metadata = new Dictionary<string, object>();
        var context = _httpContextAccessor.HttpContext;
        
        if (context != null)
        {
            metadata["IpAddress"] = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            metadata["UserAgent"] = context.Request.Headers["User-Agent"].ToString();
            metadata["CorrelationId"] = context.TraceIdentifier;
        }
        
        return metadata;
    }
}

// Infrastructure/ML/MLPredictionService.cs
namespace FinanceTracker.Infrastructure.ML;

public class MLPredictionService : IMLPredictionService
{
    private readonly MLContext _mlContext;
    private readonly IModelStore _modelStore;
    private readonly ILogger<MLPredictionService> _logger;
    
    public MLPredictionService(
        MLContext mlContext,
        IModelStore modelStore,
        ILogger<MLPredictionService> logger)
    {
        _mlContext = mlContext;
        _modelStore = modelStore;
        _logger = logger;
    }
    
    public async Task<CategoryPrediction> PredictCategoryAsync(
        Transaction transaction,
        CancellationToken cancellationToken = default)
    {
        var model = await _modelStore.GetActiveModelAsync(
            ModelType.CategoryPrediction,
            cancellationToken);
        
        if (model == null)
        {
            _logger.LogWarning("No active category prediction model found");
            return CategoryPrediction.None;
        }
        
        var features = ExtractFeatures(transaction);
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<TransactionFeatures, CategoryPrediction>(model);
        var prediction = predictionEngine.Predict(features);
        
        _logger.LogInformation(
            "Predicted category {CategoryId} for transaction {TransactionId} with {Confidence:P} confidence",
            prediction.CategoryId, transaction.Id, prediction.Confidence);
        
        return prediction;
    }
    
    public async Task<AnomalyDetectionResult> DetectAnomaliesAsync(
        IEnumerable<Transaction> transactions,
        CancellationToken cancellationToken = default)
    {
        var model = await _modelStore.GetActiveModelAsync(
            ModelType.AnomalyDetection,
            cancellationToken);
        
        if (model == null)
            return new AnomalyDetectionResult { AnomaliesDetected = 0 };
        
        var dataView = _mlContext.Data.LoadFromEnumerable(transactions);
        var predictions = model.Transform(dataView);
        
        var anomalies = _mlContext.Data
            .CreateEnumerable<AnomalyPrediction>(predictions, reuseRowObject: false)
            .Where(p => p.IsAnomaly)
            .ToList();
        
        return new AnomalyDetectionResult
        {
            AnomaliesDetected = anomalies.Count,
            Anomalies = anomalies.Select(a => new SpendingAnomaly
            {
                TransactionId = a.TransactionId,
                Severity = a.Score,
                Description = GenerateAnomalyDescription(a)
            }).ToList()
        };
    }
    
    private TransactionFeatures ExtractFeatures(Transaction transaction)
    {
        return new TransactionFeatures
        {
            Description = NormalizeText(transaction.Description),
            Amount = (float)transaction.Amount.Amount,
            DayOfWeek = (int)transaction.TransactionDate.DayOfWeek,
            DayOfMonth = transaction.TransactionDate.Day,
            IsWeekend = transaction.TransactionDate.DayOfWeek >= DayOfWeek.Saturday ? 1 : 0
        };
    }
}

// ============================================
// GRAPHQL LAYER - Primary Adapter (Port)
// ============================================

// GraphQL/Types/AccountType.cs
namespace FinanceTracker.GraphQL.Types;

[ExtendObjectType(typeof(Account))]
public class AccountTypeExtensions
{
    [UseDbContext(typeof(FinanceContext))]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Transaction> GetTransactions(
        [Parent] Account account,
        [ScopedService] FinanceContext context)
    {
        return context.Transactions
            .Where(t => t.AccountId == account.Id)
            .OrderByDescending(t => t.TransactionDate);
    }
    
    [DataLoader]
    internal static async Task<ILookup<Guid, Transaction>> GetTransactionsByAccountIdAsync(
        IReadOnlyList<Guid> accountIds,
        FinanceContext context,
        CancellationToken cancellationToken)
    {
        var transactions = await context.Transactions
            .Where(t => accountIds.Contains(t.AccountId))
            .ToListAsync(cancellationToken);
        
        return transactions.ToLookup(t => t.AccountId);
    }
    
    public async Task<decimal> GetMonthlyAverage(
        [Parent] Account account,
        [Service] IMediator mediator,
        int months = 6)
    {
        var query = new GetAccountMonthlyAverageQuery(account.Id, months);
        return await mediator.Send(query);
    }
    
    public async Task<decimal> GetProjectedBalance(
        [Parent] Account account,
        [Service] IMLPredictionService mlService,
        int daysAhead = 30)
    {
        var forecast = await mlService.ForecastCashFlowAsync(
            account.UserId,
            daysAhead);
        
        return account.Balance.Amount + forecast.NetChange;
    }
    
    [UseDbContext(typeof(FinanceContext))]
    public async Task<IEnumerable<AuditEntry>> GetAuditTrail(
        [Parent] Account account,
        [ScopedService] FinanceContext context,
        int first = 20)
    {
        return await context.AuditLogs
            .Where(a => a.EntityType == "Account" && a.EntityId == account.Id.ToString())
            .OrderByDescending(a => a.CreatedAt)
            .Take(first)
            .ToListAsync();
    }
}

// GraphQL/Queries/AccountQueries.cs
namespace FinanceTracker.GraphQL.Queries;

[ExtendObjectType("Query")]
public class AccountQueries
{
    [Authorize]
    [UseDbContext(typeof(FinanceContext))]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Account> GetAccount(
        Guid id,
        [ScopedService] FinanceContext context,
        [Service] ICurrentUserService currentUser)
    {
        return context.Accounts
            .Where(a => a.Id == id && 
                   (a.UserId == currentUser.UserId || 
                    a.HouseholdId != null && context.HouseholdMembers
                        .Any(hm => hm.HouseholdId == a.HouseholdId && 
                              hm.UserId == currentUser.UserId)));
    }
    
    [Authorize]
    [UseDbContext(typeof(FinanceContext))]
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    [UseProjection]
    public IQueryable<Account> GetAccounts(
        [ScopedService] FinanceContext context,
        [Service] ICurrentUserService currentUser)
    {
        return context.Accounts
            .Where(a => a.UserId == currentUser.UserId || 
                   (a.HouseholdId != null && context.HouseholdMembers
                       .Any(hm => hm.HouseholdId == a.HouseholdId && 
                            hm.UserId == currentUser.UserId)));
    }
    
    [Authorize]
    public async Task<NetWorth> GetNetWorth(
        [Service] IMediator mediator)
    {
        return await mediator.Send(new GetNetWorthQuery());
    }
}

// GraphQL/Queries/BudgetQueries.cs
[ExtendObjectType("Query")]
public class BudgetQueries
{
    [Authorize]
    public async Task<Budget?> GetBudget(
        Guid id,
        [Service] IEventStore eventStore)
    {
        // Try each budget type
        var envelopeBudget = await eventStore.GetAggregateAsync<EnvelopeBudget>(id);
        if (envelopeBudget != null) return envelopeBudget;
        
        // Check other budget types...
        // This could be optimized with a budget type lookup table
        
        return null;
    }
    
    [Authorize]
    [UseDbContext(typeof(FinanceContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BudgetProjection> GetBudgets(
        [ScopedService] FinanceContext context,
        [Service] ICurrentUserService currentUser,
        bool? active = null,
        BudgetType? type = null)
    {
        var query = context.Budgets
            .Where(b => b.UserId == currentUser.UserId);
        
        if (active.HasValue)
            query = query.Where(b => b.IsActive == active.Value);
        
        if (type.HasValue)
            query = query.Where(b => b.BudgetType == type.Value);
        
        return query;
    }
    
    [Authorize]
    public async Task<BudgetComparison> CompareBudgetStrategies(
        decimal monthlyIncome,
        [Service] IBudgetComparisonService comparisonService,
        [Service] ICurrentUserService currentUser)
    {
        return await comparisonService.CompareStrategiesAsync(
            currentUser.UserId,
            new Money(monthlyIncome, "USD"));
    }
}

// GraphQL/Queries/InsightQueries.cs
[ExtendObjectType("Query")]
public class InsightQueries
{
    [Authorize]
    public async Task<IEnumerable<SpendingInsight>> GetInsights(
        [Service] IMediator mediator,
        DateTime? from = null,
        DateTime? to = null,
        InsightType? type = null)
    {
        var query = new GetSpendingInsightsQuery(from, to, type);
        return await mediator.Send(query);
    }
    
    [Authorize]
    public async Task<IEnumerable<SpendingAnomaly>> GetAnomalies(
        [Service] IMLPredictionService mlService,
        [Service] ITransactionRepository transactionRepo,
        [Service] ICurrentUserService currentUser,
        bool acknowledged = false)
    {
        var transactions = await transactionRepo.GetUserTransactionsAsync(
            currentUser.UserId,
            DateTime.UtcNow.AddMonths(-3),
            DateTime.UtcNow);
        
        var result = await mlService.DetectAnomaliesAsync(transactions);
        
        return acknowledged 
            ? result.Anomalies 
            : result.Anomalies.Where(a => !a.Acknowledged);
    }
    
    [Authorize]
    public async Task<IEnumerable<CashFlowForecast>> GetCashFlowForecast(
        [Service] IMLPredictionService mlService,
        [Service] ICurrentUserService currentUser,
        int daysAhead = 30)
    {
        return await mlService.ForecastCashFlowAsync(
            currentUser.UserId,
            daysAhead);
    }
}

// GraphQL/Mutations/AccountMutations.cs
namespace FinanceTracker.GraphQL.Mutations;

[ExtendObjectType("Mutation")]
public class AccountMutations
{
    [Authorize]
    [Error(typeof(ValidationException))]
    [Error(typeof(DomainException))]
    public async Task<Account> CreateAccount(
        CreateAccountInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateAccountCommand(
            input.Name,
            input.AccountType,
            input.InitialBalance,
            input.Currency ?? "USD",
            input.Institution);
        
        return await mediator.Send(command);
    }
    
    [Authorize]
    [Error(typeof(InsufficientFundsException))]
    public async Task<TransferResult> CreateTransfer(
        CreateTransferInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateTransferCommand(
            input.FromAccountId,
            input.ToAccountId,
            input.Amount,
            input.Date ?? DateTime.UtcNow,
            input.Description);
        
        return await mediator.Send(command);
    }
}

// GraphQL/Mutations/BudgetMutations.cs
[ExtendObjectType("Mutation")]
public class BudgetMutations
{
    [Authorize]
    [Error(typeof(ValidationException))]
    public async Task<EnvelopeBudget> CreateEnvelopeBudget(
        CreateEnvelopeBudgetInput input,
        [Service] IMediator mediator)
    {
        var envelopes = input.Envelopes.Select(e => new CreateEnvelopeDto(
            e.Name,
            e.AllocatedAmount,
            e.Currency ?? "USD",
            e.Category,
            e.RolloverEnabled)).ToList();
        
        var command = new CreateEnvelopeBudgetCommand(
            input.Name,
            input.PeriodType,
            input.StrictMode,
            envelopes);
        
        return await mediator.Send(command);
    }
    
    [Authorize]
    public async Task<EnvelopeTransferResult> TransferBetweenEnvelopes(
        Guid fromEnvelopeId,
        Guid toEnvelopeId,
        decimal amount,
        [Service] IMediator mediator)
    {
        var command = new TransferBetweenEnvelopesCommand(
            fromEnvelopeId,
            toEnvelopeId,
            new Money(amount, "USD"));
        
        return await mediator.Send(command);
    }
}

// GraphQL/Subscriptions/TransactionSubscriptions.cs
namespace FinanceTracker.GraphQL.Subscriptions;

[ExtendObjectType("Subscription")]
public class TransactionSubscriptions
{
    [Authorize]
    [Subscribe]
    [Topic("{accountId}")]
    public Transaction OnTransactionAdded(
        [EventMessage] Transaction transaction,
        Guid accountId)
    {
        return transaction;
    }
}

[ExtendObjectType("Subscription")]
public class BudgetSubscriptions
{
    [Authorize]
    [Subscribe]
    [Topic("budget-alert-{budgetId}")]
    public BudgetAlert OnBudgetThresholdReached(
        [EventMessage] BudgetAlert alert,
        Guid budgetId)
    {
        return alert;
    }
}

[ExtendObjectType("Subscription")]
public class InsightSubscriptions
{
    [Authorize]
    [Subscribe]
    [Topic("anomaly-{userId}")]
    public SpendingAnomaly OnAnomalyDetected(
        [EventMessage] SpendingAnomaly anomaly,
        [Service] ICurrentUserService currentUser)
    {
        return anomaly;
    }
    
    [Authorize]
    [Subscribe]
    [Topic("insight-{userId}")]
    public SpendingInsight OnNewInsightGenerated(
        [EventMessage] SpendingInsight insight,
        [Service] ICurrentUserService currentUser)
    {
        return insight;
    }
}

// GraphQL/DataLoaders/CategoryDataLoader.cs
namespace FinanceTracker.GraphQL.DataLoaders;

public class CategoryDataLoader : BatchDataLoader<Guid, Category>
{
    private readonly IDbContextFactory<FinanceContext> _contextFactory;
    
    public CategoryDataLoader(
        IDbContextFactory<FinanceContext> contextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) 
        : base(batchScheduler, options)
    {
        _contextFactory = contextFactory;
    }
    
    protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        
        return await context.Categories
            .Where(c => keys.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id, cancellationToken);
    }
}

// ============================================
// HOST CONFIGURATION
// ============================================

// WebAPI/Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add Core Services
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>();
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        });
        
        builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        // Add Infrastructure
        builder.Services.AddDbContext<FinanceContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly(typeof(FinanceContext).Assembly.FullName));
        });
        
        builder.Services.AddPooledDbContextFactory<FinanceContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
        });
        
        // Add Event Store
        builder.Services.AddScoped<IEventStore, PostgresEventStore>();
        builder.Services.AddScoped<IEventSerializer, JsonEventSerializer>();
        
        // Add ML Services
        builder.Services.AddSingleton<MLContext>();
        builder.Services.AddScoped<IMLPredictionService, MLPredictionService>();
        builder.Services.AddScoped<IModelStore, PostgresModelStore>();
        
        // Add Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Auth:Authority"];
                options.Audience = builder.Configuration["Auth:Audience"];
            });
        
        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        // Add GraphQL
        builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<AccountQueries>()
                .AddTypeExtension<BudgetQueries>()
                .AddTypeExtension<InsightQueries>()
            .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<AccountMutations>()
                .AddTypeExtension<BudgetMutations>()
            .AddSubscriptionType(d => d.Name("Subscription"))
                .AddTypeExtension<TransactionSubscriptions>()
                .AddTypeExtension<BudgetSubscriptions>()
                .AddTypeExtension<InsightSubscriptions>()
            .AddType<AccountType>()
            .AddType<EnvelopeBudgetType>()
            .AddTypeExtension<AccountTypeExtensions>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<FinanceContext>(DbContextKind.Pooled)
            .AddDataLoader<CategoryDataLoader>()
            .AddInMemorySubscriptions()
            .AddErrorFilter<GraphQLErrorFilter>();
        
        // Add Health Checks
        builder.Services.AddHealthChecks()
            .AddDbContextCheck<FinanceContext>()
            .AddCheck<EventStoreHealthCheck>("event-store");
        
        var app = builder.Build();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseWebSockets();
        app.MapGraphQL();
        app.MapHealthChecks("/health");
        
        app.Run();
    }
}
```

This complete implementation shows how the hexagonal architecture works with .NET, HotChocolate, and MediatR:

## Architecture Layers

### **Domain Layer (Core/Center)**

- **Pure business logic** with no dependencies
- **Event-sourced aggregates** (Account, EnvelopeBudget)
- **Value Objects** (Money) for type safety
- **Domain Events** for audit trail
- **Domain Exceptions** for business rule violations

### **Application Layer (Ports)**

- **Commands & Queries** (CQRS with MediatR)
- **Use Case orchestration**
- **Cross-cutting behaviors** (Validation, Logging)
- **Port interfaces** (IEventStore, IMLPredictionService)
- **DTOs** for data transfer

### **Infrastructure Layer (Adapters)**

- **PostgreSQL Event Store** implementation
- **ML.NET** for predictions
- **Entity Framework Core** for projections
- **External service integrations**

### **GraphQL Layer (Primary Adapter)**

- **HotChocolate** for GraphQL
- **DataLoaders** for N+1 prevention
- **Subscriptions** for real-time updates
- **Error handling** with unions
- **Authorization** policies

## Key Design Patterns

### **Event Sourcing Flow**

```csharp
Command → Handler → Aggregate → Domain Events → Event Store → Projections
```

### **CQRS Pattern**

- **Commands**: Modify state through aggregates
- **Queries**: Read from optimized projections
- **Events**: Bridge between write and read models

### **MediatR Pipeline**

```csharp
Request → Validation → Logging → Transaction → Handler → Response
```

### **GraphQL Resolution**

```csharp
GraphQL Query → HotChocolate → MediatR → Application Service → Domain/Infrastructure
```

## Benefits of This Architecture

1. **Testability**: Each layer can be tested in isolation
2. **Flexibility**: Swap implementations without changing business logic
3. **Maintainability**: Clear separation of concerns
4. **Scalability**: Event sourcing enables temporal queries and CQRS
5. **Type Safety**: Strong typing from database to GraphQL
6. **Audit Trail**: Complete history via event sourcing

The hexagonal architecture ensures your business logic (budgeting strategies, financial rules) remains pure and testable, while adapters handle the messy details of databases, ML models, and external services. The GraphQL layer provides a flexible, strongly-typed API that can evolve independently of your domain model.

**human** (Aug 29, 2025, 10:43 PM)

Based on all of this information, could you create a PRD?

**assistant** (Aug 29, 2025, 10:46 PM)

#### create Personal Finance Management Platform - Product Requirements Document

```
# Product Requirements Document (PRD)
## Personal Finance Management Platform

**Version:** 1.0  
**Date:** January 2025  
**Status:** Draft

---

## 1. Executive Summary

### 1.1 Product Vision
Build a comprehensive personal finance management platform that empowers individuals and families to take control of their financial lives through intelligent automation, flexible budgeting strategies, and actionable insights.

### 1.2 Problem Statement
Current personal finance applications force users into rigid budgeting methodologies, lack intelligent automation, provide limited family collaboration features, and fail to offer comprehensive audit trails for financial accountability. Users struggle to find a solution that adapts to their unique financial philosophy while providing the sophistication needed for complex financial planning.

### 1.3 Solution Overview
A modern, GraphQL-powered financial platform that combines:
- **7 flexible budgeting strategies** that users can switch between or combine
- **ML-powered automation** for transaction categorization and anomaly detection
- **Complete audit trail** through event sourcing
- **Family collaboration** with granular permissions
- **Comprehensive debt management** with multiple payoff strategies
- **Predictive insights** for proactive financial management

### 1.4 Success Metrics
- User activation rate: >60% complete initial budget setup
- User retention: >70% monthly active users after 6 months
- Automation accuracy: >85% correct auto-categorization
- User satisfaction: NPS score >50
- Financial outcomes: Users save 20% more within 3 months

---

## 2. Market Analysis

### 2.1 Target Audience

#### Primary Persona: "Financial Optimizer"
- **Demographics:** 25-45 years old, household income $50K-$150K
- **Characteristics:** Tech-savvy, goal-oriented, values automation
- **Pain Points:** Tired of manual categorization, wants flexible budgeting, needs family coordination
- **Goals:** Build wealth, eliminate debt, achieve financial goals

#### Secondary Persona: "Family Financial Manager"  
- **Demographics:** 30-50 years old, managing family finances
- **Characteristics:** Needs visibility across family spending, values security
- **Pain Points:** Difficult to track family member spending, no unified view
- **Goals:** Teach financial responsibility, manage household budget

#### Tertiary Persona: "Debt Eliminator"
- **Demographics:** Any age, carrying significant debt
- **Characteristics:** Motivated to become debt-free, needs structure
- **Pain Points:** Overwhelmed by multiple debts, unclear on best strategy
- **Goals:** Become debt-free as quickly as possible

### 2.2 Competitive Analysis

| Feature | Mint | YNAB | Personal Capital | **Our Platform** |
|---------|------|------|------------------|------------------|
| Multiple Budget Strategies | ❌ | ❌ | ❌ | ✅ (7 types) |
| ML Auto-Categorization | Limited | ❌ | ❌ | ✅ Advanced |
| Event Sourcing/Audit Trail | ❌ | ❌ | ❌ | ✅ Complete |
| Family Collaboration | ❌ | Limited | ❌ | ✅ Full |
| Debt Strategies | Basic | ❌ | ❌ | ✅ Multiple |
| API Access | Limited | ✅ | ❌ | ✅ GraphQL |
| Predictive Analytics | ❌ | ❌ | Limited | ✅ ML-Powered |

### 2.3 Market Opportunity
- **TAM:** $4.8B global personal finance software market
- **Growth:** 12.8% CAGR through 2028
- **Differentiator:** Only platform offering strategy flexibility with ML intelligence

---

## 3. Product Features

### 3.1 Core Features (MVP - Phase 1)

#### 3.1.1 Account Management
- **Multi-account support:** Checking, savings, credit cards, investments, loans
- **Manual account creation** with balance tracking
- **Transaction management:** Add, edit, categorize transactions
- **Account reconciliation:** Match statements with recorded transactions

**Acceptance Criteria:**
- Users can create and manage multiple account types
- Transactions update account balances in real-time
- Support for multiple currencies

#### 3.1.2 Basic Budgeting
- **Traditional budgeting:** Category-based spending limits
- **Budget periods:** Monthly, quarterly, yearly
- **Performance tracking:** Actual vs. budgeted spending
- **Overspending alerts:** Real-time notifications

**Acceptance Criteria:**
- Users can set spending limits per category
- Visual indicators show budget progress
- Alerts trigger at 80% and 100% of budget

#### 3.1.3 Transaction Categorization
- **Manual categorization** with custom categories
- **Category hierarchy:** Parent and sub-categories
- **Split transactions:** Divide single transaction across categories
- **Bulk categorization:** Apply categories to multiple transactions

**Acceptance Criteria:**
- Support unlimited custom categories
- Drag-and-drop for bulk operations
- Category rules for recurring transactions

#### 3.1.4 Basic Reporting
- **Spending by category:** Pie charts and trends
- **Account balances:** Historical balance charts
- **Monthly summary:** Income vs. expenses
- **Export capability:** CSV and PDF reports

**Acceptance Criteria:**
- Reports update in real-time
- Date range selection for all reports
- Mobile-responsive visualizations

### 3.2 Advanced Features (Phase 2)

#### 3.2.1 Family/Household Management
- **Household creation:** Invite family members
- **Role-based access:** Admin, Parent, Member, Child, View-only
- **Shared accounts:** Joint visibility with permissions
- **Spending limits:** Per-member restrictions
- **Unified reporting:** Household-level analytics

**Technical Implementation:**
```graphql
type Household {
  members: [HouseholdMember!]!
  sharedAccounts: [Account!]!
  sharedBudgets: [Budget!]!
  totalNetWorth: Decimal!
}
```

**Acceptance Criteria:**

- Secure invitation system with email verification
- Granular permission controls per account
- Activity feed for household transactions
- Parental controls for child accounts

#### 3.2.2 Flexible Budgeting Strategies

**Envelope Budgeting**

- Virtual envelopes for spending categories
- Strict mode preventing overspending
- Envelope-to-envelope transfers
- Rollover options for unused funds

**Percentage-Based Budgeting**

- 50/30/20 rule and variations
- Custom percentage allocations
- Automatic distribution calculations
- Income-based adjustments

**Priority-Based Budgeting**

- Pay-yourself-first methodology
- Ordered priority funding
- Automatic allocation to priorities
- Goal-linked priorities

**Rolling Budget**

- Based on rolling averages
- Anomaly detection for unusual spending
- Adaptive budget limits
- Seasonal adjustments

**Value-Based Budgeting**

- Link spending to personal values
- Value alignment scoring
- Spending-to-value analysis
- Recommendations for better alignment

**Hybrid Budgeting**

- Combine multiple strategies
- Weighted strategy application
- Strategy performance comparison
- Migration between strategies

**Acceptance Criteria:**

- Users can switch strategies without data loss
- Strategy recommendation engine based on spending patterns
- Comparison tool showing projected outcomes
- Strategy-specific visualizations and reports

#### 3.2.3 Bill Management & Reminders

- **Bill tracking:** Recurring and one-time bills
- **Smart reminders:** Configurable notification timing
- **Variable bill handling:** Average amount predictions
- **Auto-pay tracking:** Payment confirmation
- **Bill calendar:** Visual monthly view

**Acceptance Criteria:**

- Automatic detection of recurring transactions
- Multi-channel notifications (email, push, in-app)
- Integration with account transactions
- Overdue bill escalation

### 3.3 Intelligence Features (Phase 3)

#### 3.3.1 ML-Powered Auto-Categorization

- **Pattern recognition:** Learn from user behavior
- **Merchant identification:** Extract and remember payees
- **Confidence scoring:** Only auto-apply >85% confidence
- **Feedback loop:** Improve with corrections
- **Bulk suggestions:** Review and apply in batches

**Technical Implementation:**

```csharp
public interface IMLPredictionService {
    Task<CategoryPrediction> PredictCategoryAsync(Transaction transaction);
    Task TrainModelAsync(IEnumerable<Transaction> transactions);
}
```

**Acceptance Criteria:**

- 85%+ accuracy after 3 months of use
- Sub-second prediction response time
- User control over automation preferences
- Transparent confidence scores

#### 3.3.2 Spending Insights & Anomaly Detection

- **Trend analysis:** Identify spending patterns
- **Anomaly alerts:** Unusual transaction detection
- **Subscription detection:** Find recurring charges
- **Savings opportunities:** Actionable recommendations
- **Peer comparisons:** Anonymous benchmarking

**Insight Types:**

- Spending trends (up/down)
- Category overspending
- Unusual transactions
- Subscription management
- Cash flow warnings
- Savings opportunities

**Acceptance Criteria:**

- Daily insight generation
- <5% false positive rate for anomalies
- Actionable recommendations with projected savings
- Dismissible insights with learning

#### 3.3.3 Cash Flow Forecasting

- **30-day projections:** ML-based predictions
- **Scheduled transaction inclusion:** Bills and recurring
- **Confidence intervals:** Show prediction uncertainty
- **Warning system:** Potential shortfalls
- **What-if scenarios:** Test financial decisions

**Acceptance Criteria:**

- 80%+ prediction accuracy for 7-day forecast
- Visual forecast with confidence bands
- Proactive alerts for predicted issues
- Scenario comparison tools

### 3.4 Debt Management (Phase 4)

#### 3.4.1 Debt Tracking & Payoff Plans

- **Debt portfolio:** All debts in one place
- **Payoff strategies:** Snowball, Avalanche, Custom
- **Payoff calculator:** Compare strategies
- **Progress tracking:** Visual payoff timeline
- **Interest savings:** Calculate total savings
- **Extra payment allocation:** Optimize additional payments

**Strategy Comparisons:**

```graphql
type PayoffProjection {
  strategy: PayoffStrategy!
  totalPayoffDate: Date!
  totalInterestPaid: Decimal!
  monthsToPayoff: Int!
  interestSaved: Decimal!
}
```

**Acceptance Criteria:**

- Support for all common debt types
- Side-by-side strategy comparison
- Motivational progress visualizations
- Milestone celebrations

### 3.5 Platform Features (Phase 5)

#### 3.5.1 Complete Audit Trail

- **Event sourcing:** Immutable transaction history
- **Change tracking:** Who, what, when, why
- **Time travel:** View account state at any point
- **Compliance reports:** Audit-ready exports
- **Data recovery:** Restore from any point

**Technical Implementation:**

```sql
CREATE TABLE event_store (
    sequence_number BIGSERIAL PRIMARY KEY,
    event_id UUID NOT NULL,
    aggregate_id UUID NOT NULL,
    event_type VARCHAR(255) NOT NULL,
    event_data JSONB NOT NULL,
    occurred_at TIMESTAMP NOT NULL,
    user_id UUID,
    ip_address INET
);
```

**Acceptance Criteria:**

- Zero data loss guarantee
- Sub-second historical queries
- Tamper-proof audit log
- Regulatory compliance (SOC2, GDPR)

#### 3.5.2 GraphQL API

- **Public API access:** Developer-friendly
- **Real-time subscriptions:** Live updates
- **Comprehensive schema:** All features exposed
- **Rate limiting:** Fair usage policies
- **SDK support:** JavaScript, Python, .NET

**Acceptance Criteria:**

- 99.9% API uptime SLA
- <100ms average response time
- Comprehensive API documentation
- Webhook support for events

---

## 4. Technical Requirements

### 4.1 Architecture

#### Technology Stack

- **Backend:** .NET 8, C#
- **API:** GraphQL (HotChocolate)
- **Database:** PostgreSQL with Event Sourcing
- **Caching:** Redis
- **ML Framework:** ML.NET
- **Architecture:** Hexagonal with CQRS/Event Sourcing
- **Messaging:** MediatR for in-process, RabbitMQ for distributed

#### Key Architectural Decisions

- **Event Sourcing:** Complete audit trail and temporal queries
- **CQRS:** Separate read/write models for performance
- **Hexagonal Architecture:** Business logic isolation
- **GraphQL:** Flexible client queries and real-time subscriptions
- **Microservices-ready:** Modular design for future scaling

### 4.2 Performance Requirements

- **Response Time:** <200ms for 95th percentile
- **Throughput:** 10,000 concurrent users
- **Data Processing:** Real-time transaction processing
- **ML Predictions:** <100ms inference time
- **Report Generation:** <2 seconds for yearly reports

### 4.3 Security Requirements

- **Authentication:** OAuth 2.0 / OpenID Connect
- **Authorization:** Role-based + attribute-based access control
- **Encryption:** AES-256 for data at rest, TLS 1.3 in transit
- **PCI Compliance:** No storage of full card numbers
- **Data Privacy:** GDPR/CCPA compliant
- **Audit Logging:** All data access logged
- **2FA:** TOTP and SMS support

### 4.4 Scalability Requirements

- **Horizontal Scaling:** Stateless application servers
- **Database Sharding:** By user_id for multi-tenancy
- **Event Stream Partitioning:** By aggregate_id
- **Cache Strategy:** Multi-level (local + distributed)
- **CDN:** Static assets and API responses

### 4.5 Reliability Requirements

- **Uptime SLA:** 99.9% (43.8 minutes/month downtime)
- **RTO:** 1 hour
- **RPO:** 1 minute
- **Backup Strategy:** Continuous replication + daily snapshots
- **Disaster Recovery:** Multi-region deployment capability

---

## 5. User Experience

### 5.1 Design Principles

- **Progressive Disclosure:** Advanced features don't overwhelm beginners
- **Mobile-First:** Full functionality on mobile devices
- **Data Visualization:** Complex data made simple
- **Accessibility:** WCAG 2.1 AA compliance
- **Personalization:** Adaptive UI based on usage patterns

### 5.2 User Flows

#### Onboarding Flow

1. Email/social signup
2. Household creation (optional)
3. Connect accounts or create manual accounts
4. Initial transaction import/entry
5. Budget strategy selection wizard
6. Category setup with ML suggestions
7. Goal setting (optional)
8. Dashboard tour

#### Daily Usage Flow

1. Dashboard with key metrics
2. New transaction notifications
3. Quick categorization with ML suggestions
4. Budget status check
5. Insight review and actions

#### Monthly Review Flow

1. Monthly summary notification
2. Budget performance review
3. Category analysis
4. Goal progress check
5. Strategy adjustment recommendations
6. Next month planning

### 5.3 Platform Support

- **Web:** Responsive SPA (React)
- **iOS:** Native app (iOS 14+)
- **Android:** Native app (Android 8+)
- **Desktop:** Electron app (Windows, Mac, Linux)
- **API:** GraphQL for third-party integrations

---

## 6. Implementation Roadmap

### Phase 1: MVP (Months 1-3)

- Core account management
- Basic transaction tracking
- Traditional budgeting
- Simple reporting
- User authentication
- **Goal:** Launch beta with 100 users

### Phase 2: Family & Budgets (Months 4-6)

- Household management
- All 7 budgeting strategies
- Budget comparison tools
- Enhanced reporting
- Bill tracking
- **Goal:** 1,000 active users

### Phase 3: Intelligence (Months 7-9)

- ML auto-categorization
- Anomaly detection
- Spending insights
- Cash flow forecasting
- Subscription detection
- **Goal:** 85% automation accuracy

### Phase 4: Debt & Growth (Months 10-12)

- Debt management suite
- Investment tracking
- Goal planning
- Tax categorization
- Financial reports
- **Goal:** 10,000 active users

### Phase 5: Platform & Scale (Months 13-15)

- Public API launch
- Marketplace for integrations
- White-label offering
- Advanced analytics
- B2B features
- **Goal:** 50,000 active users

---

## 7. Success Metrics

### 7.1 User Metrics

- **MAU Growth:** 20% month-over-month
- **DAU/MAU Ratio:** >40%
- **Session Duration:** >5 minutes average
- **Feature Adoption:** >60% use 3+ features
- **Churn Rate:** <5% monthly

### 7.2 Business Metrics

- **Customer Acquisition Cost:** <$50
- **Lifetime Value:** >$500
- **Monthly Recurring Revenue:** $500K by month 12
- **Gross Margin:** >80%
- **Payback Period:** <12 months

### 7.3 Product Metrics

- **Auto-categorization Accuracy:** >85%
- **Insight Actionability:** >30% acted upon
- **Budget Success Rate:** >60% stay within budget
- **API Usage:** >1M calls/month
- **System Uptime:** >99.9%

### 7.4 Quality Metrics

- **Bug Rate:** <5 per 1,000 users
- **Performance:** <200ms p95 latency
- **Security Incidents:** Zero breaches
- **Support Tickets:** <10 per 100 users
- **App Store Rating:** >4.5 stars

---

## 8. Risks & Mitigations

### 8.1 Technical Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| ML model accuracy issues | High | Medium | Extensive training data, feedback loops, manual fallbacks |
| Event store scaling challenges | High | Low | Proper partitioning, snapshots, read model optimization |
| Complex migration between budget types | Medium | Medium | Comprehensive testing, gradual rollout, rollback capability |
| GraphQL query complexity attacks | Medium | Low | Query depth limiting, complexity analysis, rate limiting |

### 8.2 Business Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| Low user adoption | High | Medium | Free tier, referral program, content marketing |
| Competitor feature parity | Medium | High | Faster innovation, unique features, better UX |
| Regulatory compliance changes | High | Low | Modular compliance layer, regular audits |
| Data breach | Critical | Low | Security-first design, regular penetration testing |

### 8.3 User Experience Risks

| Risk | Impact | Probability | Mitigation |
|------|--------|-------------|------------|
| Feature complexity overwhelm | High | Medium | Progressive disclosure, onboarding wizard, tutorials |
| Poor mobile experience | High | Low | Mobile-first design, extensive device testing |
| Inaccurate ML predictions | Medium | Medium | Confidence thresholds, user controls, transparency |

---

## 9. Dependencies

### 9.1 External Dependencies

- **Banking APIs:** Plaid, Yodlee for account aggregation
- **Payment Processing:** Stripe for subscriptions
- **Authentication:** Auth0 or AWS Cognito
- **Cloud Infrastructure:** AWS or Azure
- **Email Service:** SendGrid or AWS SES
- **Push Notifications:** Firebase Cloud Messaging
- **Analytics:** Mixpanel or Amplitude

### 9.2 Internal Dependencies

- **Design System:** Shared component library
- **Data Pipeline:** ETL for analytics
- **ML Pipeline:** Model training infrastructure
- **DevOps:** CI/CD pipeline
- **Monitoring:** Application performance monitoring

---

## 10. Open Questions

1. **Monetization Model:** Freemium vs. subscription vs. usage-based?
2. **Data Retention:** How long to keep detailed transaction history?
3. **International Expansion:** Which markets to target first?
4. **Partnership Strategy:** Banks, employers, or financial advisors?
5. **Regulatory Approach:** Register as financial advisor?
6. **Open Source Strategy:** Which components to open source?
7. **B2B vs. B2C Focus:** Consumer-first or enterprise features?

---

## 11. Appendices

### A. Glossary

- **Aggregate:** Domain-driven design pattern for consistency boundary
- **CQRS:** Command Query Responsibility Segregation
- **Event Sourcing:** Storing state changes as sequence of events
- **Hexagonal Architecture:** Ports and adapters architecture pattern
- **GraphQL:** Query language for APIs
- **ML:** Machine Learning
- **MVP:** Minimum Viable Product

### B. User Stories Examples

```
As a user, I want to:
- Switch between budgeting strategies without losing my data
- See which family member made each transaction
- Get alerts before I exceed my budget
- Understand why a transaction was flagged as unusual
- Compare different debt payoff strategies side-by-side
```

### C. API Examples

```graphql
query MyFinancialDashboard {
  me {
    netWorth { total, trend }
    accounts { name, balance, monthlyAverage }
    activeBudget { 
      type, currentPerformance { percentageUsed }
    }
    insights(priority: HIGH) { 
      title, actionableAdvice 
    }
  }
}

mutation CreateEnvelopeBudget {
  createEnvelopeBudget(input: {
    name: "Monthly Budget"
    envelopes: [
      { name: "Groceries", allocatedAmount: 500 }
      { name: "Transportation", allocatedAmount: 200 }
    ]
  }) {
    id
    envelopes { name, remainingAmount }
  }
}
```

---

**Document Control:**

- **Author:** Product Team
- **Reviewers:** Engineering, Design, Marketing
- **Approval:** CEO, CTO
- **Next Review:** Quarterly

```

I've created a comprehensive Product Requirements Document that brings together all the features and technical details we've discussed. The PRD includes:

## Key Sections:

1. **Executive Summary** - Clear vision, problem statement, and solution overview
2. **Market Analysis** - Target personas, competitive analysis, and market opportunity
3. **Detailed Features** - Organized in 5 phases from MVP to platform features
4. **Technical Requirements** - Architecture, performance, security, and scalability needs
5. **User Experience** - Design principles, user flows, and platform support
6. **Implementation Roadmap** - 15-month phased rollout with clear goals
7. **Success Metrics** - User, business, product, and quality KPIs
8. **Risk Analysis** - Technical, business, and UX risks with mitigations
9. **Dependencies** - External services and internal requirements

## Notable Highlights:

- **7 Flexible Budgeting Strategies** - Unique differentiator in the market
- **ML-Powered Intelligence** - Auto-categorization, anomaly detection, and forecasting
- **Complete Audit Trail** - Event sourcing for regulatory compliance
- **Family Collaboration** - Comprehensive household management with roles
- **GraphQL API** - Modern, flexible API for integrations
- **Phased Approach** - Start with MVP, progressively add advanced features

The PRD provides a clear roadmap for building a comprehensive personal finance platform that stands out through its flexibility, intelligence, and family-friendly features. It balances ambitious goals with practical implementation phases, ensuring the team can deliver value incrementally while building toward the full vision.

**human** (Aug 29, 2025, 10:47 PM)

Are there any additional technical implementation documents we would need to generate before planning this application with full seriousness?

**assistant** (Aug 29, 2025, 10:48 PM)

 Excellent question! For a production-ready financial application, you'd need several additional technical documents. Let me outline the critical ones:

## 1. **Security & Compliance Architecture Document**

Given the sensitive financial data, this is absolutely critical:

```markdown
SECURITY ARCHITECTURE DOCUMENT
├── Threat Model
│   ├── STRIDE analysis for each component
│   ├── Attack surface mapping
│   ├── Data flow diagrams with trust boundaries
│   └── Risk scoring matrix
│
├── Compliance Requirements
│   ├── PCI DSS (if handling cards)
│   ├── SOC 2 Type II requirements
│   ├── GDPR/CCPA data privacy
│   ├── Open Banking (PSD2) compliance
│   └── Financial regulations by region
│
├── Encryption Strategy
│   ├── Data at rest (field-level encryption design)
│   ├── Data in transit (TLS configuration)
│   ├── Key management (HSM/KMS strategy)
│   ├── Secrets management (connection strings, API keys)
│   └── Tokenization strategy for sensitive data
│
└── Access Control Design
    ├── Authentication architecture (OAuth/OIDC flows)
    ├── Authorization model (RBAC + ABAC)
    ├── API security (rate limiting, DDoS protection)
    └── Audit logging requirements
```

## 2. **Data Architecture & Migration Strategy**

Critical for handling financial data correctly:

```markdown
DATA ARCHITECTURE DOCUMENT
├── Data Models
│   ├── Logical data model (complete ERD)
│   ├── Physical data model (indexes, partitions)
│   ├── Event sourcing schema design
│   └── Read model projections
│
├── Data Governance
│   ├── Data classification (PII, financial, public)
│   ├── Retention policies by data type
│   ├── Right to erasure (GDPR) implementation
│   └── Data lineage tracking
│
├── Migration Strategy
│   ├── Bank data import specifications
│   ├── Legacy system migration plans
│   ├── Data validation rules
│   └── Rollback procedures
│
└── Backup & Recovery
    ├── Backup strategy (frequency, retention)
    ├── Point-in-time recovery procedures
    ├── Disaster recovery plan
    └── Data replication architecture
```

## 3. **ML Pipeline & Model Management**

For the intelligent features:

```markdown
ML ENGINEERING DOCUMENT
├── Model Architecture
│   ├── Feature engineering pipeline
│   ├── Model architectures by use case
│   ├── Training data requirements
│   └── Evaluation metrics
│
├── MLOps Pipeline
│   ├── Data pipeline (collection, cleaning, labeling)
│   ├── Training pipeline (automated retraining)
│   ├── Model versioning strategy
│   ├── A/B testing framework
│   └── Model monitoring & drift detection
│
├── Production Deployment
│   ├── Model serving architecture
│   ├── Inference optimization
│   ├── Fallback strategies
│   └── Edge cases handling
│
└── Privacy & Fairness
    ├── Differential privacy implementation
    ├── Bias detection & mitigation
    ├── Explainability requirements
    └── User consent & control
```

## 4. **API Design & Integration Specifications**

Detailed API contracts:

```markdown
API SPECIFICATION DOCUMENT
├── GraphQL Schema Design
│   ├── Complete schema with all types
│   ├── Naming conventions
│   ├── Versioning strategy
│   ├── Deprecation policy
│   └── Breaking change management
│
├── External Integrations
│   ├── Banking API specifications (Plaid/Yodlee)
│   ├── Payment processor integration (Stripe)
│   ├── Notification services (email/SMS/push)
│   ├── Analytics integration
│   └── Error handling & retry strategies
│
├── Webhook Design
│   ├── Event catalog
│   ├── Delivery guarantees
│   ├── Retry logic
│   └── Security (signatures, replay protection)
│
└── Rate Limiting & Quotas
    ├── Rate limit strategies by endpoint
    ├── User tier quotas
    ├── Cost-based throttling
    └── Burst handling
```

## 5. **Infrastructure & DevOps Plan**

Deployment and operations:

```markdown
INFRASTRUCTURE DOCUMENT
├── Cloud Architecture
│   ├── Multi-region deployment strategy
│   ├── Service topology (microservices boundaries)
│   ├── Container orchestration (K8s specs)
│   ├── Service mesh configuration
│   └── CDN strategy
│
├── Observability
│   ├── Metrics collection (Prometheus/Grafana)
│   ├── Distributed tracing (OpenTelemetry)
│   ├── Log aggregation (ELK stack)
│   ├── APM strategy
│   └── Alerting rules & runbooks
│
├── CI/CD Pipeline
│   ├── Build pipeline stages
│   ├── Testing strategy (unit, integration, E2E)
│   ├── Security scanning (SAST, DAST, dependencies)
│   ├── Deployment strategies (blue-green, canary)
│   └── Rollback procedures
│
└── Performance Engineering
    ├── Load testing scenarios
    ├── Performance budgets
    ├── Caching strategy (Redis configuration)
    ├── Database optimization plan
    └── Capacity planning model
```

## 6. **Testing Strategy Document**

Comprehensive testing approach:

```markdown
TESTING STRATEGY
├── Test Pyramid
│   ├── Unit tests (>80% coverage target)
│   ├── Integration tests (API, database)
│   ├── E2E tests (critical user journeys)
│   └── Contract tests (GraphQL schema)
│
├── Specialized Testing
│   ├── Financial calculation accuracy tests
│   ├── ML model validation suite
│   ├── Security testing (penetration, fuzzing)
│   ├── Performance testing scenarios
│   └── Chaos engineering plan
│
├── Test Data Management
│   ├── Synthetic data generation
│   ├── PII masking for test environments
│   ├── Test account management
│   └── Data refresh procedures
│
└── Quality Gates
    ├── Code review requirements
    ├── Coverage thresholds
    ├── Performance benchmarks
    └── Security scan requirements
```

## 7. **Financial Calculations Specification**

Critical for accuracy:

```markdown
FINANCIAL CALCULATIONS SPEC
├── Interest Calculations
│   ├── Simple vs compound interest
│   ├── APR vs APY conversions
│   ├── Daily vs monthly compounding
│   └── Amortization schedules
│
├── Currency Handling
│   ├── Decimal precision rules
│   ├── Rounding strategies (banker's rounding)
│   ├── Multi-currency conversion
│   └── Historical rate storage
│
├── Tax Considerations
│   ├── Tax category mappings
│   ├── Deduction tracking
│   ├── Capital gains calculations
│   └── Report generation
│
└── Regulatory Calculations
    ├── Credit utilization
    ├── Debt-to-income ratios
    ├── Net worth calculations
    └── Savings rate formulas
```

## 8. **Incident Response & Runbooks**

For production operations:

```markdown
INCIDENT RESPONSE PLAN
├── Incident Classification
│   ├── Severity levels (P0-P4)
│   ├── Impact assessment matrix
│   ├── Escalation procedures
│   └── Communication templates
│
├── Common Scenarios
│   ├── Data breach response
│   ├── Service outage procedures
│   ├── Data corruption recovery
│   ├── ML model failure handling
│   └── Third-party service failures
│
├── Runbooks
│   ├── Database failover
│   ├── Cache invalidation
│   ├── Event replay procedures
│   ├── Account reconciliation
│   └── Emergency access procedures
│
└── Post-Mortem Process
    ├── Template structure
    ├── Root cause analysis
    ├── Action items tracking
    └── Knowledge sharing
```

## 9. **Mobile Application Architecture**

If building native apps:

```markdown
MOBILE ARCHITECTURE
├── Platform Strategy
│   ├── Native vs React Native vs Flutter
│   ├── Feature parity matrix
│   ├── Offline capabilities
│   └── Platform-specific features
│
├── Security
│   ├── Biometric authentication
│   ├── Secure storage (Keychain/Keystore)
│   ├── Certificate pinning
│   └── Jailbreak/root detection
│
├── Synchronization
│   ├── Offline-first architecture
│   ├── Conflict resolution
│   ├── Delta sync strategy
│   └── Background sync
│
└── Performance
    ├── App size optimization
    ├── Battery usage guidelines
    ├── Network optimization
    └── Memory management
```

## 10. **SLA & Support Documentation**

For production readiness:

```markdown
SLA DOCUMENTATION
├── Service Levels
│   ├── Uptime targets by tier
│   ├── Response time SLAs
│   ├── Data processing SLAs
│   └── Support response times
│
├── Monitoring & Reporting
│   ├── SLA calculation methods
│   ├── Reporting cadence
│   ├── Penalty structures
│   └── Exclusions
│
└── Support Procedures
    ├── Tier 1/2/3 support structure
    ├── Knowledge base requirements
    ├── Customer communication
    └── Feature request process
```

## Priority Order for Implementation

1. **Security & Compliance Architecture** - Absolutely critical, needed before any code
2. **Data Architecture** - Foundational for everything else
3. **API Specifications** - Needed for parallel frontend/backend development
4. **Infrastructure Plan** - Required for initial deployment
5. **Testing Strategy** - Essential for quality assurance
6. **Financial Calculations Spec** - Core to product accuracy
7. **ML Pipeline** - Can be developed iteratively
8. **Incident Response** - Needed before production launch
9. **Mobile Architecture** - If/when mobile apps are planned
10. **SLA Documentation** - For production/enterprise customers

## Additional Recommendations

**Legal Documentation Needed:**

- Terms of Service
- Privacy Policy
- Data Processing Agreements
- Security Whitepaper
- Compliance Certifications Plan

**Business Documentation:**

- Pricing Model Analysis
- Customer Support Playbook
- Partner Integration Guide
- Marketing Claims Validation

These documents ensure you're building a production-ready, compliant, and scalable financial platform. The security and data architecture documents are particularly critical given the sensitive nature of financial data.

**human** (Aug 29, 2025, 10:50 PM)

Would be there any benefit in splitting some of these APIs into individual graphQL apis and combining them with federation?

**assistant** (Aug 29, 2025, 10:51 PM)

 Excellent question! GraphQL Federation would be highly beneficial for this financial platform. Let me break down how and why to split the APIs:

#### create GraphQL Federation Architecture for Personal Finance Platform

```typescript
// ============================================
// GRAPHQL FEDERATION ARCHITECTURE
// Splitting monolithic GraphQL into federated services
// ============================================

// ============================================
// SERVICE BOUNDARIES & RATIONALE
// ============================================

/*
FEDERATED ARCHITECTURE:

                        ┌─────────────────┐
                        │   API Gateway   │
                        │  (Apollo Router)│
                        └────────┬────────┘
                                 │
                    ┌────────────┴────────────┐
                    │    Federation Gateway   │
                    └────────────┬────────────┘
                                 │
        ┌────────────────────────┼────────────────────────┐
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│ Core Accounts   │    │    Budgeting    │    │   Intelligence   │
│    Service      │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│  Transactions   │    │  Debt & Goals   │    │   Household      │
│    Service      │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│   Audit/Event   │    │   Notifications │    │    Analytics     │
│     Service     │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘
*/

// ============================================
// 1. CORE ACCOUNTS SERVICE
// Owns: Users, Accounts, basic account operations
// ============================================

// accounts-service/schema.graphql
const accountsSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@shareable", "@external", "@requires", "@provides"])

  type User @key(fields: "id") {
    id: ID!
    email: String!
    name: String!
    currency: String!
    preferences: JSON!
    createdAt: DateTime!
    
    # Owned by this service
    accounts: [Account!]!
  }

  type Account @key(fields: "id") {
    id: ID!
    userId: ID!
    name: String!
    accountType: AccountType!
    balance: Decimal!
    institution: String
    currency: String!
    interestRate: Float
    creditLimit: Decimal
    isActive: Boolean!
    isShared: Boolean!
    
    # Reference to Household service
    household: Household
    
    # Computed field
    netChange(period: TimePeriod!): Decimal! @requires(fields: "balance")
  }

  type Query {
    me: User!
    account(id: ID!): Account
    accounts(filter: AccountFilter): [Account!]!
  }

  type Mutation {
    createAccount(input: CreateAccountInput!): Account!
    updateAccount(id: ID!, input: UpdateAccountInput!): Account!
    closeAccount(id: ID!): Account!
  }
`;

// accounts-service/resolvers.ts
export const accountsResolvers = {
  User: {
    __resolveReference(reference: { id: string }) {
      return getUserById(reference.id);
    },
    accounts(user: User) {
      return getAccountsByUserId(user.id);
    }
  },
  
  Account: {
    __resolveReference(reference: { id: string }) {
      return getAccountById(reference.id);
    },
    household(account: Account) {
      // This will be resolved by Household service
      return { __typename: "Household", id: account.householdId };
    }
  },
  
  Query: {
    me: (_, __, context) => getUserById(context.userId),
    account: (_, { id }) => getAccountById(id),
    accounts: (_, { filter }, context) => getAccounts(context.userId, filter)
  },
  
  Mutation: {
    createAccount: async (_, { input }, context) => {
      const account = await createAccount(context.userId, input);
      
      // Publish event for other services
      await publishEvent('AccountCreated', {
        accountId: account.id,
        userId: context.userId,
        accountType: account.accountType
      });
      
      return account;
    }
  }
};

// ============================================
// 2. TRANSACTIONS SERVICE
// Owns: Transactions, Categories, Payees
// ============================================

// transactions-service/schema.graphql
const transactionsSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@shareable", "@external", "@requires"])

  type Transaction @key(fields: "id") {
    id: ID!
    accountId: ID!
    amount: Decimal!
    transactionDate: Date!
    description: String!
    transactionType: TransactionType!
    status: TransactionStatus!
    
    # Owned by this service
    category: Category
    payee: Payee
    tags: [Tag!]!
    
    # Extended by Intelligence service
    mlPrediction: MLPrediction
    
    # Reference to Account
    account: Account! @provides(fields: "balance")
  }

  type Category @key(fields: "id") {
    id: ID!
    name: String!
    parent: Category
    children: [Category!]!
    icon: String
    color: String
    
    # Analytics
    monthlyAverage: Decimal!
    trend: TrendDirection!
  }

  extend type Account @key(fields: "id") {
    id: ID! @external
    # Add transactions to Account
    transactions(
      filter: TransactionFilter
      first: Int
      after: String
    ): TransactionConnection!
  }

  type Query {
    transaction(id: ID!): Transaction
    transactions(filter: TransactionFilter): TransactionConnection!
    categories: [Category!]!
    searchTransactions(query: String!): [Transaction!]!
  }

  type Mutation {
    createTransaction(input: CreateTransactionInput!): Transaction!
    updateTransaction(id: ID!, input: UpdateTransactionInput!): Transaction!
    categorizeTransaction(id: ID!, categoryId: ID!): Transaction!
    splitTransaction(id: ID!, splits: [SplitInput!]!): [Transaction!]!
  }
`;

// transactions-service/resolvers.ts
export const transactionsResolvers = {
  Transaction: {
    __resolveReference(reference: { id: string }) {
      return getTransactionById(reference.id);
    },
    account(transaction: Transaction) {
      return { __typename: "Account", id: transaction.accountId };
    },
    mlPrediction(transaction: Transaction) {
      // Intelligence service will resolve this
      return { __typename: "MLPrediction", transactionId: transaction.id };
    }
  },
  
  Account: {
    transactions(account: { id: string }, { filter, first, after }) {
      return getTransactionConnection(account.id, filter, first, after);
    }
  },
  
  Mutation: {
    createTransaction: async (_, { input }, context) => {
      const transaction = await createTransaction(input);
      
      // Notify other services
      await publishEvent('TransactionCreated', {
        transactionId: transaction.id,
        accountId: input.accountId,
        amount: input.amount,
        needsCategorization: !input.categoryId
      });
      
      return transaction;
    }
  }
};

// ============================================
// 3. BUDGETING SERVICE
// Owns: All budget types, budget performance
// ============================================

// budgeting-service/schema.graphql
const budgetingSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@external", "@requires"])

  interface Budget {
    id: ID!
    name: String!
    budgetType: BudgetType!
    period: BudgetPeriod!
    isActive: Boolean!
    performance: BudgetPerformance!
  }

  type EnvelopeBudget implements Budget @key(fields: "id") {
    id: ID!
    name: String!
    budgetType: BudgetType!
    period: BudgetPeriod!
    isActive: Boolean!
    performance: BudgetPerformance!
    
    # Specific to envelope
    envelopes: [Envelope!]!
    strictMode: Boolean!
    totalAllocated: Decimal!
  }

  type Envelope @key(fields: "id") {
    id: ID!
    name: String!
    allocatedAmount: Decimal!
    spentAmount: Decimal!
    remainingAmount: Decimal!
    
    # Links to transactions
    allocatedTransactions: [Transaction!]!
  }

  extend type User @key(fields: "id") {
    id: ID! @external
    activeBudget: Budget
    budgets(active: Boolean): [Budget!]!
  }

  extend type Transaction @key(fields: "id") {
    id: ID! @external
    # Budget impact analysis
    budgetImpact: BudgetImpact! @requires(fields: "amount categoryId")
    envelopeAllocation: EnvelopeAllocation
  }

  type Query {
    budget(id: ID!): Budget
    budgets(type: BudgetType, active: Boolean): [Budget!]!
    compareBudgetStrategies(income: Decimal!): BudgetComparison!
  }

  type Mutation {
    createEnvelopeBudget(input: EnvelopeBudgetInput!): EnvelopeBudget!
    createTraditionalBudget(input: TraditionalBudgetInput!): TraditionalBudget!
    switchBudgetStrategy(currentId: ID!, newType: BudgetType!): Budget!
    allocateToEnvelope(transactionId: ID!, envelopeId: ID!): EnvelopeAllocation!
  }
`;

// ============================================
// 4. INTELLIGENCE SERVICE
// Owns: ML models, predictions, insights, anomalies
// ============================================

// intelligence-service/schema.graphql
const intelligenceSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@external", "@requires"])

  type MLPrediction @key(fields: "transactionId") {
    transactionId: ID!
    predictedCategory: Category!
    confidence: Float!
    modelVersion: String!
    accepted: Boolean
  }

  type SpendingInsight @key(fields: "id") {
    id: ID!
    userId: ID!
    type: InsightType!
    title: String!
    description: String!
    actionableAdvice: String!
    priority: InsightPriority!
    relatedTransactions: [Transaction!]!
    potentialSavings: Decimal
    validUntil: DateTime
  }

  type SpendingAnomaly @key(fields: "id") {
    id: ID!
    transaction: Transaction!
    anomalyType: AnomalyType!
    severity: Float!
    description: String!
    expectedRange: Range!
    actualValue: Decimal!
  }

  type CashFlowForecast {
    date: Date!
    predictedIncome: Decimal!
    predictedExpenses: Decimal!
    confidence: Float!
    factors: [ForecastFactor!]!
  }

  extend type Transaction @key(fields: "id") {
    id: ID! @external
    amount: Decimal! @external
    description: String! @external
    
    # ML additions
    mlPrediction: MLPrediction
    isAnomaly: Boolean!
    similarTransactions: [Transaction!]!
  }

  extend type User @key(fields: "id") {
    id: ID! @external
    insights(type: InsightType, priority: InsightPriority): [SpendingInsight!]!
    anomalies(acknowledged: Boolean): [SpendingAnomaly!]!
    cashFlowForecast(days: Int!): [CashFlowForecast!]!
  }

  type Query {
    insights(type: InsightType, priority: InsightPriority): [SpendingInsight!]!
    detectAnomalies(transactions: [ID!]!): [SpendingAnomaly!]!
    predictCategory(transactionId: ID!): MLPrediction!
    cashFlowForecast(accountId: ID!, days: Int!): [CashFlowForecast!]!
  }

  type Mutation {
    confirmPrediction(transactionId: ID!, correct: Boolean!): MLPrediction!
    dismissInsight(id: ID!): SpendingInsight!
    acknowledgeAnomaly(id: ID!): SpendingAnomaly!
    trainModel(modelType: ModelType!): TrainingJob!
  }

  type Subscription {
    onAnomalyDetected(userId: ID!): SpendingAnomaly!
    onInsightGenerated(userId: ID!): SpendingInsight!
  }
`;

// intelligence-service/resolvers.ts
export const intelligenceResolvers = {
  Transaction: {
    mlPrediction: async (transaction: { id: string }) => {
      // Check cache first
      const cached = await getCachedPrediction(transaction.id);
      if (cached) return cached;
      
      // Generate prediction
      const prediction = await mlPredict(transaction);
      await cachePrediction(transaction.id, prediction);
      
      return prediction;
    },
    
    isAnomaly: async (transaction: Transaction) => {
      const anomalyScore = await detectAnomaly(transaction);
      return anomalyScore > ANOMALY_THRESHOLD;
    },
    
    similarTransactions: async (transaction: Transaction) => {
      return findSimilarTransactions(transaction, 5);
    }
  },
  
  User: {
    insights: async (user: { id: string }, { type, priority }) => {
      return generateInsights(user.id, type, priority);
    },
    
    cashFlowForecast: async (user: { id: string }, { days }) => {
      return forecastCashFlow(user.id, days);
    }
  },
  
  Mutation: {
    confirmPrediction: async (_, { transactionId, correct }) => {
      // Update training data
      await updateTrainingData(transactionId, correct);
      
      // Retrain if needed
      if (shouldRetrain()) {
        await queueModelRetraining();
      }
      
      return updatePrediction(transactionId, { accepted: correct });
    }
  },
  
  Subscription: {
    onAnomalyDetected: {
      subscribe: (_, { userId }) => {
        return pubsub.asyncIterator(`anomaly.${userId}`);
      }
    }
  }
};

// ============================================
// 5. HOUSEHOLD SERVICE
// Owns: Households, family management, permissions
// ============================================

// household-service/schema.graphql
const householdSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@external", "@provides"])

  type Household @key(fields: "id") {
    id: ID!
    name: String!
    createdBy: User!
    members: [HouseholdMember!]!
    
    # Aggregate data
    totalNetWorth: Decimal!
    monthlySpending: Decimal!
    savingsRate: Float!
    
    # Shared resources
    sharedAccounts: [Account!]!
    sharedBudgets: [Budget!]!
  }

  type HouseholdMember {
    user: User!
    role: HouseholdRole!
    permissions: [Permission!]!
    spendingLimit: Decimal
    joinedAt: DateTime!
  }

  extend type User @key(fields: "id") {
    id: ID! @external
    household: Household
    householdRole: HouseholdRole
  }

  extend type Account @key(fields: "id") {
    id: ID! @external
    householdId: ID! @external
    household: Household @provides(fields: "name members")
    permissions(userId: ID!): [Permission!]!
  }

  type Query {
    household(id: ID!): Household
    myHousehold: Household
  }

  type Mutation {
    createHousehold(name: String!): Household!
    inviteToHousehold(email: String!, role: HouseholdRole!): Invitation!
    updateMemberPermissions(userId: ID!, permissions: [Permission!]!): HouseholdMember!
    shareAccount(accountId: ID!, permissions: [Permission!]!): Account!
  }
`;

// ============================================
// 6. DEBT & GOALS SERVICE
// Owns: Debt tracking, payoff plans, financial goals
// ============================================

// debt-goals-service/schema.graphql
const debtGoalsSchema = `
  extend schema
    @link(url: "https://specs.apollo.dev/federation/v2.3",
          import: ["@key", "@external"])

  type DebtPayoffPlan @key(fields: "id") {
    id: ID!
    userId: ID!
    name: String!
    strategy: PayoffStrategy!
    monthlyBudget: Decimal!
    debts: [Debt!]!
    projection: PayoffProjection!
    totalSaved: Decimal!
  }

  type Debt @key(fields: "id") {
    id: ID!
    name: String!
    currentBalance: Decimal!
    originalBalance: Decimal!
    interestRate: Float!
    minimumPayment: Decimal!
    account: Account
    payoffDate: Date!
    totalInterest: Decimal!
  }

  type Goal @key(fields: "id") {
    id: ID!
    name: String!
    targetAmount: Decimal!
    currentAmount: Decimal!
    targetDate: Date!
    account: Account
    onTrack: Boolean!
    recommendedMonthly: Decimal!
  }

  extend type User @key(fields: "id") {
    id: ID! @external
    debtPayoffPlans: [De
```

{"start_timestamp":"2025-08-30T03:51:59.652061+00:00","stop_timestamp":"2025-08-30T03:51:59.652061+00:00","type":"text","text":"","citations":[]}

**human** (Aug 29, 2025, 10:52 PM)

Sorry, could you generated all of that in c# instead?

**assistant** (Aug 29, 2025, 10:55 PM)

#### rewrite Untitled

```
// ============================================
// GRAPHQL FEDERATION ARCHITECTURE IN C#/.NET
// Using HotChocolate with Apollo Federation
// ============================================

// ============================================
// SERVICE BOUNDARIES & RATIONALE
// ============================================

/*
FEDERATED ARCHITECTURE:

                        ┌─────────────────┐
                        │   API Gateway   │
                        │  (Apollo Router)│
                        └────────┬────────┘
                                 │
                    ┌────────────┴────────────┐
                    │    Federation Gateway   │
                    └────────────┬────────────┘
                                 │
        ┌────────────────────────┼────────────────────────┐
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│ Core Accounts   │    │    Budgeting    │    │   Intelligence   │
│    Service      │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│  Transactions   │    │  Debt & Goals   │    │   Household      │
│    Service      │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘
        │                        │                        │
┌───────▼─────────┐    ┌────────▼────────┐    ┌─────────▼────────┐
│   Audit/Event   │    │   Notifications │    │    Analytics     │
│     Service     │    │     Service     │    │     Service      │
└─────────────────┘    └─────────────────┘    └──────────────────┘

BENEFITS OF FEDERATION:
1. Independent deployment and scaling
2. Team autonomy - different teams own different services
3. Technology flexibility - could use different databases/languages
4. Fault isolation - one service failure doesn't bring down everything
5. Performance optimization - each service can be optimized independently
6. Compliance boundaries - sensitive data can be isolated
*/

// ============================================
// 1. CORE ACCOUNTS SERVICE
// Owns: Users, Accounts, basic account operations
// ============================================

namespace FinanceTracker.Services.Accounts;

// Models/User.cs
[GraphQLKey("id")]
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public Dictionary<string, object> Preferences { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Owned by this service
    public List<Account> Accounts { get; set; }
}

// Models/Account.cs
[GraphQLKey("id")]
public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public string? Institution { get; set; }
    public string Currency { get; set; }
    public decimal? InterestRate { get; set; }
    public decimal? CreditLimit { get; set; }
    public bool IsActive { get; set; }
    public bool IsShared { get; set; }
    
    // Reference to Household service (will be resolved by Household service)
    [GraphQLReference]
    public Guid? HouseholdId { get; set; }
}

// Types/UserType.cs
[ExtendObjectType<User>]
public class UserTypeExtensions
{
    // Reference resolver for federation
    [GraphQLReferenceResolver]
    public static Task<User?> ResolveUserReferenceAsync(
        [Service] IUserRepository repository,
        [GraphQLRepresentation] Representation representation)
    {
        var id = representation.GetValue<Guid>("id");
        return repository.GetByIdAsync(id);
    }
    
    public async Task<IEnumerable<Account>> GetAccounts(
        [Parent] User user,
        [Service] IAccountRepository repository)
    {
        return await repository.GetByUserIdAsync(user.Id);
    }
}

// Types/AccountType.cs
[ExtendObjectType<Account>]
public class AccountTypeExtensions
{
    [GraphQLReferenceResolver]
    public static Task<Account?> ResolveAccountReferenceAsync(
        [Service] IAccountRepository repository,
        [GraphQLRepresentation] Representation representation)
    {
        var id = representation.GetValue<Guid>("id");
        return repository.GetByIdAsync(id);
    }
    
    // This provides the household reference for other services
    [GraphQLReference]
    public Household? GetHousehold([Parent] Account account)
    {
        if (account.HouseholdId == null) return null;
        
        // Return a reference that will be resolved by Household service
        return new Household { Id = account.HouseholdId.Value };
    }
    
    // Computed field
    [GraphQLRequires("balance")]
    public decimal GetNetChange(
        [Parent] Account account,
        TimePeriod period,
        [Service] ITransactionAnalysisService analysisService)
    {
        return analysisService.CalculateNetChange(account.Id, period);
    }
}

// Queries/AccountQueries.cs
[ExtendObjectType("Query")]
public class AccountQueries
{
    public async Task<User> GetMe([Service] ICurrentUserService currentUser, [Service] IUserRepository repository)
    {
        return await repository.GetByIdAsync(currentUser.UserId);
    }
    
    public async Task<Account?> GetAccount(Guid id, [Service] IAccountRepository repository)
    {
        return await repository.GetByIdAsync(id);
    }
    
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Account>> GetAccounts(
        [Service] ICurrentUserService currentUser,
        [Service] IAccountRepository repository,
        AccountFilter? filter = null)
    {
        return await repository.GetUserAccountsAsync(currentUser.UserId, filter);
    }
}

// Mutations/AccountMutations.cs
[ExtendObjectType("Mutation")]
public class AccountMutations
{
    [Error(typeof(ValidationException))]
    public async Task<Account> CreateAccount(
        CreateAccountInput input,
        [Service] IMediator mediator,
        [Service] IEventPublisher eventPublisher)
    {
        var command = new CreateAccountCommand(input);
        var account = await mediator.Send(command);
        
        // Publish event for other services
        await eventPublisher.PublishAsync(new AccountCreatedEvent
        {
            AccountId = account.Id,
            UserId = account.UserId,
            AccountType = account.AccountType,
            InitialBalance = account.Balance
        });
        
        return account;
    }
}

// Program.cs (Accounts Service)
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add MediatR
        builder.Services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());
        
        // Add repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        
        // Add GraphQL with Federation
        builder.Services
            .AddGraphQLServer()
            .AddApolloFederation() // Enable federation
            .AddQueryType()
            .AddTypeExtension<AccountQueries>()
            .AddMutationType()
            .AddTypeExtension<AccountMutations>()
            .AddType<User>()
            .AddType<Account>()
            .AddTypeExtension<UserTypeExtensions>()
            .AddTypeExtension<AccountTypeExtensions>()
            .AddAuthorization();
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}

// ============================================
// 2. TRANSACTIONS SERVICE
// Owns: Transactions, Categories, Payees
// ============================================

namespace FinanceTracker.Services.Transactions;

// Models/Transaction.cs
[GraphQLKey("id")]
public class Transaction
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public TransactionType TransactionType { get; set; }
    public TransactionStatus Status { get; set; }
    
    // Owned by this service
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid? PayeeId { get; set; }
    public Payee? Payee { get; set; }
    public List<Tag> Tags { get; set; }
    
    // Extended by Intelligence service
    [GraphQLIgnore]
    public Guid? MLPredictionId { get; set; }
}

// Models/Category.cs
[GraphQLKey("id")]
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }
    public List<Category> Children { get; set; }
    public string? Icon { get; set; }
    public string? Color { get; set; }
}

// Types/TransactionType.cs
[ExtendObjectType<Transaction>]
public class TransactionTypeExtensions
{
    [GraphQLReferenceResolver]
    public static Task<Transaction?> ResolveTransactionReferenceAsync(
        [Service] ITransactionRepository repository,
        [GraphQLRepresentation] Representation representation)
    {
        var id = representation.GetValue<Guid>("id");
        return repository.GetByIdAsync(id);
    }
    
    // Reference to Account (resolved by Accounts service)
    [GraphQLProvides("balance")]
    public Account GetAccount([Parent] Transaction transaction)
    {
        return new Account { Id = transaction.AccountId };
    }
    
    // Reference for ML prediction (resolved by Intelligence service)
    [GraphQLReference]
    public MLPrediction? GetMlPrediction([Parent] Transaction transaction)
    {
        if (transaction.MLPredictionId == null) return null;
        return new MLPrediction { TransactionId = transaction.Id };
    }
    
    public async Task<Category?> GetCategory(
        [Parent] Transaction transaction,
        [DataLoader] CategoryDataLoader categoryLoader)
    {
        if (transaction.CategoryId == null) return null;
        return await categoryLoader.LoadAsync(transaction.CategoryId.Value);
    }
}

// Extend Account type from Accounts service
[ExtendObjectType<Account>]
public class AccountTransactionExtensions
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Transaction>> GetTransactions(
        [Parent] Account account,
        [Service] ITransactionRepository repository)
    {
        return repository.GetAccountTransactionsQueryable(account.Id);
    }
    
    public async Task<decimal> GetMonthlyAverage(
        [Parent] Account account,
        [Service] ITransactionAnalysisService analysisService,
        int months = 6)
    {
        return await analysisService.CalculateMonthlyAverage(account.Id, months);
    }
}

// DataLoaders/CategoryDataLoader.cs
public class CategoryDataLoader : BatchDataLoader<Guid, Category>
{
    private readonly ICategoryRepository _repository;
    
    public CategoryDataLoader(
        ICategoryRepository repository,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) 
        : base(batchScheduler, options)
    {
        _repository = repository;
    }
    
    protected override async Task<IReadOnlyDictionary<Guid, Category>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var categories = await _repository.GetByIdsAsync(keys, cancellationToken);
        return categories.ToDictionary(c => c.Id);
    }
}

// Mutations/TransactionMutations.cs
[ExtendObjectType("Mutation")]
public class TransactionMutations
{
    [Error(typeof(InsufficientFundsException))]
    public async Task<Transaction> CreateTransaction(
        CreateTransactionInput input,
        [Service] IMediator mediator,
        [Service] IEventPublisher eventPublisher)
    {
        var command = new CreateTransactionCommand(input);
        var transaction = await mediator.Send(command);
        
        // Notify other services
        await eventPublisher.PublishAsync(new TransactionCreatedEvent
        {
            TransactionId = transaction.Id,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount,
            NeedsCategorization = transaction.CategoryId == null
        });
        
        return transaction;
    }
    
    public async Task<Transaction> CategorizeTransaction(
        Guid id,
        Guid categoryId,
        [Service] ITransactionRepository repository,
        [Service] IEventPublisher eventPublisher)
    {
        var transaction = await repository.UpdateCategoryAsync(id, categoryId);
        
        // Notify ML service for training
        await eventPublisher.PublishAsync(new TransactionCategorizedEvent
        {
            TransactionId = id,
            CategoryId = categoryId,
            UserId = transaction.Account.UserId
        });
        
        return transaction;
    }
}

// ============================================
// 3. BUDGETING SERVICE
// Owns: All budget types, budget performance
// ============================================

namespace FinanceTracker.Services.Budgeting;

// Models/Budget.cs (base interface)
[GraphQLInterfaceType]
public interface IBudget
{
    Guid Id { get; }
    string Name { get; }
    BudgetType BudgetType { get; }
    BudgetPeriod Period { get; }
    bool IsActive { get; }
    
    Task<BudgetPerformance> GetPerformance(IPerformanceCalculator calculator);
}

// Models/EnvelopeBudget.cs
[GraphQLKey("id")]
public class EnvelopeBudget : IBudget
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public BudgetType BudgetType => BudgetType.Envelope;
    public BudgetPeriod Period { get; set; }
    public bool IsActive { get; set; }
    
    // Specific to envelope
    public List<Envelope> Envelopes { get; set; }
    public bool StrictMode { get; set; }
    public decimal TotalAllocated { get; set; }
    
    public async Task<BudgetPerformance> GetPerformance(IPerformanceCalculator calculator)
    {
        return await calculator.CalculateEnvelopePerformance(this);
    }
}

// Models/Envelope.cs
[GraphQLKey("id")]
public class Envelope
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public string Name { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal RemainingAmount => AllocatedAmount - SpentAmount;
    
    // Links to transactions
    public List<EnvelopeAllocation> Allocations { get; set; }
}

// Extend User type from Accounts service
[ExtendObjectType<User>]
public class UserBudgetExtensions
{
    public async Task<IBudget?> GetActiveBudget(
        [Parent] User user,
        [Service] IBudgetRepository repository)
    {
        return await repository.GetActiveUserBudgetAsync(user.Id);
    }
    
    [UseFiltering]
    public async Task<IEnumerable<IBudget>> GetBudgets(
        [Parent] User user,
        [Service] IBudgetRepository repository,
        bool? active = null)
    {
        return await repository.GetUserBudgetsAsync(user.Id, active);
    }
}

// Extend Transaction type from Transactions service
[ExtendObjectType<Transaction>]
public class TransactionBudgetExtensions
{
    [GraphQLRequires("amount categoryId")]
    public async Task<BudgetImpact> GetBudgetImpact(
        [Parent] Transaction transaction,
        [Service] IBudgetAnalyzer analyzer)
    {
        return await analyzer.AnalyzeTransactionImpact(transaction);
    }
    
    public async Task<EnvelopeAllocation?> GetEnvelopeAllocation(
        [Parent] Transaction transaction,
        [Service] IEnvelopeRepository repository)
    {
        return await repository.GetTransactionAllocationAsync(transaction.Id);
    }
}

// Mutations/BudgetMutations.cs
[ExtendObjectType("Mutation")]
public class BudgetMutations
{
    public async Task<EnvelopeBudget> CreateEnvelopeBudget(
        CreateEnvelopeBudgetInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateEnvelopeBudgetCommand(input);
        return await mediator.Send(command);
    }
    
    public async Task<IBudget> SwitchBudgetStrategy(
        Guid currentId,
        BudgetType newType,
        [Service] IBudgetMigrationService migrationService)
    {
        return await migrationService.MigrateStrategyAsync(currentId, newType);
    }
    
    [Error(typeof(InsufficientFundsException))]
    public async Task<EnvelopeAllocation> AllocateToEnvelope(
        Guid transactionId,
        Guid envelopeId,
        [Service] IEnvelopeAllocationService allocationService)
    {
        return await allocationService.AllocateTransactionAsync(transactionId, envelopeId);
    }
}

// ============================================
// 4. INTELLIGENCE SERVICE
// Owns: ML models, predictions, insights, anomalies
// ============================================

namespace FinanceTracker.Services.Intelligence;

// Models/MLPrediction.cs
[GraphQLKey("transactionId")]
public class MLPrediction
{
    public Guid TransactionId { get; set; }
    public Guid PredictedCategoryId { get; set; }
    public Category? PredictedCategory { get; set; }
    public double Confidence { get; set; }
    public string ModelVersion { get; set; }
    public bool? Accepted { get; set; }
}

// Models/SpendingInsight.cs
[GraphQLKey("id")]
public class SpendingInsight
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public InsightType Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ActionableAdvice { get; set; }
    public InsightPriority Priority { get; set; }
    public List<Guid> RelatedTransactionIds { get; set; }
    public decimal? PotentialSavings { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool Dismissed { get; set; }
}

// Models/SpendingAnomaly.cs
[GraphQLKey("id")]
public class SpendingAnomaly
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public AnomalyType AnomalyType { get; set; }
    public double Severity { get; set; }
    public string Description { get; set; }
    public decimal ExpectedMin { get; set; }
    public decimal ExpectedMax { get; set; }
    public decimal ActualValue { get; set; }
    public bool Acknowledged { get; set; }
}

// Extend Transaction type from Transactions service
[ExtendObjectType<Transaction>]
public class TransactionIntelligenceExtensions
{
    public async Task<MLPrediction?> GetMlPrediction(
        [Parent] Transaction transaction,
        [Service] IMLPredictionService predictionService)
    {
        // Check cache first
        var cached = await predictionService.GetCachedPredictionAsync(transaction.Id);
        if (cached != null) return cached;
        
        // Generate prediction
        var prediction = await predictionService.PredictCategoryAsync(transaction);
        await predictionService.CachePredictionAsync(transaction.Id, prediction);
        
        return prediction;
    }
    
    public async Task<bool> GetIsAnomaly(
        [Parent] Transaction transaction,
        [Service] IAnomalyDetectionService anomalyService)
    {
        var score = await anomalyService.CalculateAnomalyScoreAsync(transaction);
        return score > AnomalyConstants.THRESHOLD;
    }
    
    public async Task<IEnumerable<Transaction>> GetSimilarTransactions(
        [Parent] Transaction transaction,
        [Service] ISimilarityService similarityService,
        int first = 5)
    {
        return await similarityService.FindSimilarTransactionsAsync(transaction, first);
    }
}

// Extend User type from Accounts service
[ExtendObjectType<User>]
public class UserIntelligenceExtensions
{
    [UseFiltering]
    public async Task<IEnumerable<SpendingInsight>> GetInsights(
        [Parent] User user,
        [Service] IInsightGenerationService insightService,
        InsightType? type = null,
        InsightPriority? priority = null)
    {
        return await insightService.GenerateUserInsightsAsync(user.Id, type, priority);
    }
    
    public async Task<IEnumerable<SpendingAnomaly>> GetAnomalies(
        [Parent] User user,
        [Service] IAnomalyRepository repository,
        bool acknowledged = false)
    {
        return await repository.GetUserAnomaliesAsync(user.Id, acknowledged);
    }
    
    public async Task<IEnumerable<CashFlowForecast>> GetCashFlowForecast(
        [Parent] User user,
        [Service] IForecastingService forecastingService,
        int days = 30)
    {
        return await forecastingService.ForecastCashFlowAsync(user.Id, days);
    }
}

// Mutations/IntelligenceMutations.cs
[ExtendObjectType("Mutation")]
public class IntelligenceMutations
{
    public async Task<MLPrediction> ConfirmPrediction(
        Guid transactionId,
        bool correct,
        [Service] IMLPredictionService predictionService,
        [Service] IMLTrainingQueue trainingQueue)
    {
        var prediction = await predictionService.UpdatePredictionFeedbackAsync(transactionId, correct);
        
        // Queue for retraining if needed
        if (!correct)
        {
            await trainingQueue.QueueRetrainingAsync(new RetrainingRequest
            {
                TransactionId = transactionId,
                CorrectCategoryId = prediction.PredictedCategoryId,
                Priority = TrainingPriority.High
            });
        }
        
        return prediction;
    }
    
    public async Task<SpendingInsight> DismissInsight(
        Guid id,
        [Service] IInsightRepository repository)
    {
        return await repository.DismissInsightAsync(id);
    }
    
    public async Task<TrainingJob> TrainModel(
        ModelType modelType,
        [Service] IMLTrainingService trainingService)
    {
        return await trainingService.StartTrainingJobAsync(modelType);
    }
}

// Subscriptions/IntelligenceSubscriptions.cs
[ExtendObjectType("Subscription")]
public class IntelligenceSubscriptions
{
    [Subscribe]
    [Topic("anomaly-{userId}")]
    public SpendingAnomaly OnAnomalyDetected(
        [EventMessage] SpendingAnomaly anomaly,
        Guid userId)
    {
        return anomaly;
    }
    
    [Subscribe]
    [Topic("insight-{userId}")]
    public SpendingInsight OnInsightGenerated(
        [EventMessage] SpendingInsight insight,
        Guid userId)
    {
        return insight;
    }
}

// ============================================
// 5. HOUSEHOLD SERVICE
// Owns: Households, family management, permissions
// ============================================

namespace FinanceTracker.Services.Household;

// Models/Household.cs
[GraphQLKey("id")]
public class Household
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedById { get; set; }
    public List<HouseholdMember> Members { get; set; }
    
    // Aggregate data
    public decimal TotalNetWorth { get; set; }
    public decimal MonthlySpending { get; set; }
    public decimal SavingsRate { get; set; }
}

// Models/HouseholdMember.cs
public class HouseholdMember
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public HouseholdRole Role { get; set; }
    public List<Permission> Permissions { get; set; }
    public decimal? SpendingLimit { get; set; }
    public DateTime JoinedAt { get; set; }
}

// Extend User type
[ExtendObjectType<User>]
public class UserHouseholdExtensions
{
    public async Task<Household?> GetHousehold(
        [Parent] User user,
        [Service] IHouseholdRepository repository)
    {
        return await repository.GetUserHouseholdAsync(user.Id);
    }
    
    public async Task<HouseholdRole?> GetHouseholdRole(
        [Parent] User user,
        [Service] IHouseholdRepository repository)
    {
        var membership = await repository.GetUserMembershipAsync(user.Id);
        return membership?.Role;
    }
}

// Extend Account type
[ExtendObjectType<Account>]
public class AccountHouseholdExtensions
{
    [GraphQLProvides("name members")]
    public async Task<Household?> GetHousehold(
        [Parent] Account account,
        [Service] IHouseholdRepository repository)
    {
        if (account.HouseholdId == null) return null;
        return await repository.GetByIdAsync(account.HouseholdId.Value);
    }
    
    public async Task<IEnumerable<Permission>> GetPermissions(
        [Parent] Account account,
        Guid userId,
        [Service] IPermissionService permissionService)
    {
        return await permissionService.GetAccountPermissionsAsync(account.Id, userId);
    }
}

// ============================================
// FEDERATION GATEWAY CONFIGURATION
// ============================================

// Gateway/Program.cs
public class GatewayProgram
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Configure Apollo Federation Gateway
        builder.Services.AddHttpClient();
        builder.Services
            .AddGraphQLServer()
            .AddApolloFederationGateway()
            .AddServiceEndpoint(new Uri("http://accounts-service/graphql"))
            .AddServiceEndpoint(new Uri("http://transactions-service/graphql"))
            .AddServiceEndpoint(new Uri("http://budgeting-service/graphql"))
            .AddServiceEndpoint(new Uri("http://intelligence-service/graphql"))
            .AddServiceEndpoint(new Uri("http://household-service/graphql"));
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}

// ============================================
// BENEFITS OF THIS FEDERATION APPROACH
// ============================================

/*
1. TEAM AUTONOMY:
   - Accounts team owns user/account logic
   - ML team owns intelligence service
   - Different teams can work independently

2. SCALABILITY:
   - Intelligence service can scale independently for ML workloads
   - Transaction service can handle high write volumes
   - Budgeting service can be optimized for complex calculations

3. TECHNOLOGY FLEXIBILITY:
   - Intelligence service could use Python for ML
   - Analytics service could use ClickHouse for time-series
   - Core services stay in C#/.NET

4. COMPLIANCE & SECURITY:
   - Sensitive data (accounts) isolated in one service
   - Audit service has immutable event store
   - Household service manages all permission logic

5. PERFORMANCE:
   - Each service has its own cache
   - Database per service pattern possible
   - Independent optimization strategies

6. DEPLOYMENT:
   - Deploy services independently
   - Canary deployments per service
   - Different SLAs per service

7. FAULT ISOLATION:
   - ML service failure doesn't affect core banking
   - Budget service issues don't impact transactions
   - Graceful degradation possible
*/
```

This C#/.NET implementation shows how to split your monolithic GraphQL API into federated services using HotChocolate with Apollo Federation.

## Key Benefits of This Federation Approach

### **1. Team Autonomy**

- **Accounts Team**: Owns core banking logic
- **ML Team**: Can work in Python/ML.NET independently  
- **Budgeting Team**: Complex financial calculations isolated
- **Mobile Team**: Can consume unified API

### **2. Independent Scaling**

- Intelligence service scales for ML workloads
- Transaction service handles high write volumes
- Budgeting service optimizes for complex calculations
- Audit service uses append-only patterns

### **3. Technology Flexibility**

Each service could use different tech:

- **Intelligence**: Python with TensorFlow
- **Analytics**: ClickHouse for time-series
- **Notifications**: Node.js for WebSockets
- **Core Services**: C#/.NET for type safety

### **4. Compliance Boundaries**

- Sensitive account data isolated
- Audit service immutable
- Household service owns all permissions
- Easy to implement data residency

### **5. Performance Optimization**

- Service-specific caching strategies
- Independent database choices
- Specialized data stores (TimescaleDB for analytics)
- GraphQL DataLoaders per service

### **6. Deployment Benefits**

- Deploy services independently
- Canary deployments per service
- Different release cycles
- Service-specific SLAs

### **7. Fault Isolation**

- ML failures don't affect banking
- Budget issues don't block transactions
- Graceful degradation built-in
- Circuit breakers between services

## When to Use Federation vs Monolith

**Use Federation when:**

- Multiple teams working on different domains
- Need to scale different parts independently
- Want technology flexibility
- Have compliance/security boundaries
- Building for enterprise scale

**Stay Monolithic when:**

- Small team (<10 developers)
- Early stage/MVP
- Simple domain model
- Don't need independent scaling
- Want simpler operations

The federated approach gives you the flexibility to evolve your architecture as you grow, while maintaining a single GraphQL API for clients.

**human** (Aug 29, 2025, 10:56 PM)

We are a small team for now, but we evolve to federation as we needed to?

**assistant** (Aug 29, 2025, 10:59 PM)

 Absolutely! Starting with a monolith and evolving to federation is actually the recommended approach for small teams. This is called the "Modular Monolith" pattern, and it's perfect for your situation. Let me show you how to structure your code NOW so that migration to federation is seamless LATER:

#### create Modular Monolith to Federation Migration Strategy

```csharp
// ============================================
// MODULAR MONOLITH ARCHITECTURE
// Start as monolith, evolve to federation when needed
// ============================================

/*
EVOLUTION PATH:

Stage 1 (NOW): Modular Monolith
--------------------------------
┌─────────────────────────────────────────┐
│         Single Deployment Unit          │
│                                         │
│  ┌──────────┐ ┌──────────┐ ┌────────┐ │
│  │ Accounts │ │Budgeting │ │  ML    │ │
│  │  Module  │ │  Module  │ │ Module │ │
│  └──────────┘ └──────────┘ └────────┘ │
│                                         │
│  ┌──────────────────────────────────┐  │
│  │     Shared Database              │  │
│  └──────────────────────────────────┘  │
└─────────────────────────────────────────┘

Stage 2 (6-12 months): Vertical Slices
---------------------------------------
┌─────────────────────────────────────────┐
│         Single Deployment Unit          │
│                                         │
│  ┌──────────┐ ┌──────────┐ ┌────────┐ │
│  │ Accounts │ │Budgeting │ │  ML    │ │
│  │  Module  │ │  Module  │ │ Module │ │
│  └─────┬────┘ └────┬─────┘ └───┬────┘ │
│        │           │            │       │
│  ┌─────▼────┐ ┌────▼─────┐ ┌───▼────┐ │
│  │ Accounts │ │ Budgeting│ │   ML   │ │
│  │    DB    │ │    DB    │ │   DB   │ │
│  └──────────┘ └──────────┘ └────────┘ │
└─────────────────────────────────────────┘

Stage 3 (12+ months): Federation
---------------------------------
┌──────────┐  ┌──────────┐  ┌────────┐
│ Accounts │  │Budgeting │  │  ML    │
│ Service  │  │ Service  │  │Service │
└─────┬────┘  └────┬─────┘  └───┬────┘
      │            │             │
┌─────▼────┐  ┌────▼─────┐  ┌───▼────┐
│ Accounts │  │ Budgeting│  │   ML   │
│    DB    │  │    DB    │  │   DB   │
└──────────┘  └──────────┘  └────────┘
*/

// ============================================
// SOLUTION STRUCTURE - MODULAR MONOLITH
// ============================================

/*
FinanceTracker/
├── src/
│   ├── Modules/                    # Feature modules (future microservices)
│   │   ├── FinanceTracker.Modules.Accounts/
│   │   │   ├── Application/
│   │   │   ├── Domain/
│   │   │   ├── Infrastructure/
│   │   │   ├── GraphQL/
│   │   │   └── ModuleDefinition.cs
│   │   │
│   │   ├── FinanceTracker.Modules.Transactions/
│   │   │   ├── Application/
│   │   │   ├── Domain/
│   │   │   ├── Infrastructure/
│   │   │   └── GraphQL/
│   │   │
│   │   ├── FinanceTracker.Modules.Budgeting/
│   │   │   ├── Application/
│   │   │   ├── Domain/
│   │   │   ├── Infrastructure/
│   │   │   └── GraphQL/
│   │   │
│   │   ├── FinanceTracker.Modules.Intelligence/
│   │   │   └── ... (ML, predictions, insights)
│   │   │
│   │   └── FinanceTracker.Modules.Household/
│   │       └── ... (family features)
│   │
│   ├── Shared/                     # Shared kernel
│   │   ├── FinanceTracker.Shared.Domain/
│   │   ├── FinanceTracker.Shared.Infrastructure/
│   │   └── FinanceTracker.Shared.Contracts/
│   │
│   └── Host/
│       └── FinanceTracker.WebAPI/  # Single deployment
│           └── Program.cs
*/

// ============================================
// SHARED CONTRACTS (Module Communication)
// ============================================

// Shared/Contracts/IAccountsModule.cs
namespace FinanceTracker.Shared.Contracts;

// Module interface - this becomes service API in federation
public interface IAccountsModule
{
    Task<AccountDto?> GetAccountAsync(Guid accountId, CancellationToken ct = default);
    Task<IEnumerable<AccountDto>> GetUserAccountsAsync(Guid userId, CancellationToken ct = default);
    Task<AccountDto> CreateAccountAsync(CreateAccountRequest request, CancellationToken ct = default);
    Task<decimal> GetAccountBalanceAsync(Guid accountId, CancellationToken ct = default);
    
    // Events that other modules can subscribe to
    event EventHandler<AccountCreatedEvent> AccountCreated;
    event EventHandler<AccountBalanceChangedEvent> BalanceChanged;
}

// Shared/Contracts/Events/AccountEvents.cs
public record AccountCreatedEvent
{
    public Guid AccountId { get; init; }
    public Guid UserId { get; init; }
    public string AccountType { get; init; }
    public decimal InitialBalance { get; init; }
    public DateTime CreatedAt { get; init; }
}

// Shared/Contracts/ITransactionsModule.cs
public interface ITransactionsModule
{
    Task<TransactionDto> CreateTransactionAsync(CreateTransactionRequest request, CancellationToken ct = default);
    Task<IEnumerable<TransactionDto>> GetAccountTransactionsAsync(Guid accountId, CancellationToken ct = default);
    Task<CategoryDto?> PredictCategoryAsync(Guid transactionId, CancellationToken ct = default);
    
    event EventHandler<TransactionCreatedEvent> TransactionCreated;
}

// ============================================
// ACCOUNTS MODULE (Future Microservice)
// ============================================

// Modules/Accounts/ModuleDefinition.cs
namespace FinanceTracker.Modules.Accounts;

public class AccountsModule : IAccountsModule, IModule
{
    private readonly IServiceProvider _serviceProvider;
    
    public event EventHandler<AccountCreatedEvent>? AccountCreated;
    public event EventHandler<AccountBalanceChangedEvent>? BalanceChanged;
    
    public AccountsModule(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        // Register module-specific services
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountService, AccountService>();
        
        // Register MediatR handlers for this module
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());
    }
    
    public void ConfigureGraphQL(IRequestExecutorBuilder builder)
    {
        builder
            .AddTypeExtension<AccountQueries>()
            .AddTypeExtension<AccountMutations>()
            .AddTypeExtension<AccountSubscriptions>()
            .AddType<Account>()
            .AddType<AccountType>()
            .AddDataLoader<AccountDataLoader>();
    }
    
    public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Future: REST API endpoints if needed
        endpoints.MapGet("/api/accounts/{id}", GetAccountEndpoint);
    }
    
    // IAccountsModule implementation
    public async Task<AccountDto?> GetAccountAsync(Guid accountId, CancellationToken ct = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
        var account = await repository.GetByIdAsync(accountId, ct);
        return account?.ToDto();
    }
    
    public async Task<AccountDto> CreateAccountAsync(CreateAccountRequest request, CancellationToken ct = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        var command = new CreateAccountCommand(request);
        var account = await mediator.Send(command, ct);
        
        // Raise event for other modules
        AccountCreated?.Invoke(this, new AccountCreatedEvent
        {
            AccountId = account.Id,
            UserId = account.UserId,
            AccountType = account.AccountType.ToString(),
            InitialBalance = account.Balance,
            CreatedAt = DateTime.UtcNow
        });
        
        return account.ToDto();
    }
}

// Modules/Accounts/Domain/Account.cs
namespace FinanceTracker.Modules.Accounts.Domain;

// Domain model stays the same
public class Account : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public AccountType Type { get; private set; }
    public Money Balance { get; private set; }
    
    // Domain logic unchanged
    public void Debit(Money amount)
    {
        if (Balance < amount)
            throw new InsufficientFundsException();
        
        Balance = Balance.Subtract(amount);
        AddDomainEvent(new AccountDebitedEvent(Id, amount));
    }
}

// Modules/Accounts/GraphQL/AccountQueries.cs
namespace FinanceTracker.Modules.Accounts.GraphQL;

[ExtendObjectType("Query")]
public class AccountQueries
{
    public async Task<Account?> GetAccount(
        Guid id,
        [Service] IAccountRepository repository)
    {
        return await repository.GetByIdAsync(id);
    }
    
    [UseFiltering]
    [UseSorting]
    public IQueryable<Account> GetAccounts(
        [Service] IAccountRepository repository,
        [Service] ICurrentUserService currentUser)
    {
        return repository.GetUserAccountsQueryable(currentUser.UserId);
    }
}

// ============================================
// TRANSACTIONS MODULE
// ============================================

// Modules/Transactions/ModuleDefinition.cs
namespace FinanceTracker.Modules.Transactions;

public class TransactionsModule : ITransactionsModule, IModule
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAccountsModule _accountsModule; // Module dependency
    
    public event EventHandler<TransactionCreatedEvent>? TransactionCreated;
    
    public TransactionsModule(
        IServiceProvider serviceProvider,
        IAccountsModule accountsModule) // Inject dependency
    {
        _serviceProvider = serviceProvider;
        _accountsModule = accountsModule;
        
        // Subscribe to account events
        _accountsModule.AccountCreated += OnAccountCreated;
    }
    
    private void OnAccountCreated(object? sender, AccountCreatedEvent e)
    {
        // React to account creation if needed
        // e.g., create initial transaction categories
    }
    
    public async Task<TransactionDto> CreateTransactionAsync(
        CreateTransactionRequest request,
        CancellationToken ct = default)
    {
        // Validate account exists (cross-module communication)
        var account = await _accountsModule.GetAccountAsync(request.AccountId, ct);
        if (account == null)
            throw new NotFoundException($"Account {request.AccountId} not found");
        
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        
        var transaction = await mediator.Send(new CreateTransactionCommand(request), ct);
        
        // Notify other modules
        TransactionCreated?.Invoke(this, new TransactionCreatedEvent
        {
            TransactionId = transaction.Id,
            AccountId = transaction.AccountId,
            Amount = transaction.Amount
        });
        
        return transaction.ToDto();
    }
}

// Modules/Transactions/GraphQL/TransactionExtensions.cs
[ExtendObjectType(typeof(Account))]
public class AccountTransactionExtensions
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Transaction> GetTransactions(
        [Parent] Account account,
        [Service] ITransactionRepository repository)
    {
        return repository.GetAccountTransactionsQueryable(account.Id);
    }
    
    public async Task<decimal> GetMonthlyAverage(
        [Parent] Account account,
        [Service] ITransactionAnalysisService analysisService,
        int months = 6)
    {
        return await analysisService.CalculateMonthlyAverage(account.Id, months);
    }
}

// ============================================
// BUDGETING MODULE
// ============================================

// Modules/Budgeting/ModuleDefinition.cs
namespace FinanceTracker.Modules.Budgeting;

public class BudgetingModule : IBudgetingModule, IModule
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITransactionsModule _transactionsModule;
    
    public BudgetingModule(
        IServiceProvider serviceProvider,
        ITransactionsModule transactionsModule)
    {
        _serviceProvider = serviceProvider;
        _transactionsModule = transactionsModule;
        
        // React to transaction events
        _transactionsModule.TransactionCreated += OnTransactionCreated;
    }
    
    private async void OnTransactionCreated(object? sender, TransactionCreatedEvent e)
    {
        // Update budget performance
        using var scope = _serviceProvider.CreateScope();
        var budgetService = scope.ServiceProvider.GetRequiredService<IBudgetService>();
        await budgetService.UpdateBudgetPerformanceAsync(e.AccountId, e.Amount);
    }
}

// ============================================
// INTELLIGENCE MODULE
// ============================================

// Modules/Intelligence/ModuleDefinition.cs
namespace FinanceTracker.Modules.Intelligence;

public class IntelligenceModule : IIntelligenceModule, IModule
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITransactionsModule _transactionsModule;
    
    public IntelligenceModule(
        IServiceProvider serviceProvider,
        ITransactionsModule transactionsModule)
    {
        _serviceProvider = serviceProvider;
        _transactionsModule = transactionsModule;
        
        _transactionsModule.TransactionCreated += OnTransactionCreated;
    }
    
    private async void OnTransactionCreated(object? sender, TransactionCreatedEvent e)
    {
        // Auto-categorize if needed
        if (!e.HasCategory)
        {
            using var scope = _serviceProvider.CreateScope();
            var mlService = scope.ServiceProvider.GetRequiredService<IMLPredictionService>();
            
            var prediction = await mlService.PredictCategoryAsync(e.TransactionId);
            if (prediction.Confidence > 0.85)
            {
                await _transactionsModule.UpdateCategoryAsync(
                    e.TransactionId, 
                    prediction.CategoryId);
            }
        }
    }
}

// ============================================
// HOST CONFIGURATION (Monolith)
// ============================================

// Host/WebAPI/Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Register modules as singletons (they manage their own scopes)
        builder.Services.AddSingleton<IAccountsModule, AccountsModule>();
        builder.Services.AddSingleton<ITransactionsModule, TransactionsModule>();
        builder.Services.AddSingleton<IBudgetingModule, BudgetingModule>();
        builder.Services.AddSingleton<IIntelligenceModule, IntelligenceModule>();
        builder.Services.AddSingleton<IHouseholdModule, HouseholdModule>();
        
        // Register all modules
        var modules = new List<IModule>
        {
            new AccountsModule(builder.Services.BuildServiceProvider()),
            new TransactionsModule(builder.Services.BuildServiceProvider(), 
                builder.Services.BuildServiceProvider().GetRequiredService<IAccountsModule>()),
            new BudgetingModule(builder.Services.BuildServiceProvider(),
                builder.Services.BuildServiceProvider().GetRequiredService<ITransactionsModule>()),
            new IntelligenceModule(builder.Services.BuildServiceProvider(),
                builder.Services.BuildServiceProvider().GetRequiredService<ITransactionsModule>()),
            new HouseholdModule(builder.Services.BuildServiceProvider())
        };
        
        // Let each module register its services
        foreach (var module in modules)
        {
            module.ConfigureServices(builder.Services);
        }
        
        // Configure shared services
        builder.Services.AddDbContext<FinanceContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        // Configure GraphQL with all modules
        var graphQLBuilder = builder.Services.AddGraphQLServer();
        
        foreach (var module in modules)
        {
            module.ConfigureGraphQL(graphQLBuilder);
        }
        
        graphQLBuilder
            .AddQueryType(d => d.Name("Query"))
            .AddMutationType(d => d.Name("Mutation"))
            .AddSubscriptionType(d => d.Name("Subscription"))
            .AddAuthorization()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
        
        var app = builder.Build();
        
        // Let modules configure endpoints
        foreach (var module in modules)
        {
            module.ConfigureEndpoints(app);
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGraphQL();
        
        app.Run();
    }
}

// ============================================
// MIGRATION PATH TO FEDERATION
// ============================================

// Step 1: Extract Module to Separate Project (Still Monolith)
// ---------------------------------------------------------
// Just move the module to its own project, still deployed together

// Step 2: Add HTTP API to Module (Prepare for Federation)
// --------------------------------------------------------
namespace FinanceTracker.Modules.Accounts.API;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsModule _module;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccount(Guid id)
    {
        var account = await _module.GetAccountAsync(id);
        return account == null ? NotFound() : Ok(account);
    }
}

// Step 3: Create Module Proxy (For Gradual Migration)
// ---------------------------------------------------
public class AccountsModuleProxy : IAccountsModule
{
    private readonly IAccountsModule? _localModule;
    private readonly HttpClient? _httpClient;
    private readonly bool _useRemote;
    
    public AccountsModuleProxy(IConfiguration configuration)
    {
        _useRemote = configuration.GetValue<bool>("UseRemoteAccountsService");
        
        if (_useRemote)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(configuration["AccountsServiceUrl"])
            };
        }
        else
        {
            _localModule = new AccountsModule(/* ... */);
        }
    }
    
    public async Task<AccountDto?> GetAccountAsync(Guid accountId, CancellationToken ct = default)
    {
        if (_useRemote)
        {
            var response = await _httpClient!.GetAsync($"api/accounts/{accountId}", ct);
            return response.IsSuccessStatusCode 
                ? await response.Content.ReadFromJsonAsync<AccountDto>(ct)
                : null;
        }
        else
        {
            return await _localModule!.GetAccountAsync(accountId, ct);
        }
    }
}

// Step 4: Deploy as Separate Service (Federation)
// -----------------------------------------------
// AccountsService/Program.cs
public class AccountsServiceProgram
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var module = new AccountsModule(builder.Services.BuildServiceProvider());
        module.ConfigureServices(builder.Services);
        
        builder.Services
            .AddGraphQLServer()
            .AddApolloFederation(); // Enable federation
        
        module.ConfigureGraphQL(builder.Services.GetGraphQLBuilder());
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}

// ============================================
// MIGRATION TRIGGERS - When to Federate?
// ============================================

/*
FEDERATE ACCOUNTS MODULE WHEN:
- Team grows and you need parallel development
- Account service needs different scaling (high read)
- Want to use different database (e.g., CockroachDB)
- Compliance requires data isolation

FEDERATE INTELLIGENCE MODULE WHEN:
- ML team wants to use Python
- Need GPU instances for model training
- Want to scale ML independently
- ML experiments slow down main development

FEDERATE BUDGETING MODULE WHEN:
- Complex calculations impact performance
- Want specialized time-series database
- Need different deployment schedule
- Business logic becomes very complex

FEDERATE HOUSEHOLD MODULE WHEN:
- Permission logic becomes complex
- Need real-time collaboration features
- Want to add social features
- Different team owns family features

SIGNS YOU'RE READY:
1. Module boundaries are stable (low cross-module changes)
2. Team size > 10 developers
3. Different scaling requirements emerged
4. Clear ownership boundaries established
5. Module has independent business value
*/

// ============================================
// BEST PRACTICES FOR MODULAR MONOLITH
// ============================================

/*
1. STRICT MODULE BOUNDARIES:
   - Modules communicate only through interfaces
   - No direct database access across modules
   - Each module has its own namespace

2. MODULE INDEPENDENCE:
   - Each module has its own domain model
   - Separate GraphQL types per module
   - Independent unit tests

3. SHARED KERNEL:
   - Only truly shared concepts (Money, UserId)
   - Minimal shared infrastructure
   - Common interfaces for cross-cutting concerns

4. DATABASE STRATEGY:
   - Start: Shared database, separate schemas
   - Later: Separate databases per module
   - Use Domain Events for consistency

5. TESTING:
   - Unit tests per module
   - Integration tests for module interactions
   - Contract tests for module interfaces

6. DEPLOYMENT:
   - Start: Single deployment
   - Later: Feature flags for module isolation
   - Finally: Separate deployments

7. MONITORING:
   - Module-level metrics from day one
   - Track inter-module dependencies
   - Monitor module boundaries for violations
*/
```

Perfect! Starting as a modular monolith is the smartest approach for a small team. Here's why this strategy works so well:

## Benefits of Starting with Modular Monolith

### **Immediate Advantages:**

- **Single deployment** - Simple DevOps, one database, easy debugging
- **Fast development** - No network calls between modules
- **Simple testing** - Everything runs locally
- **Lower costs** - One server, one database, simple infrastructure

### **Future-Proof Design:**

- **Clean boundaries** from day one make splitting easy
- **Module interfaces** become service APIs later
- **Domain events** become message queue events
- **No refactoring** needed when you federate

## Migration Triggers (When to Split)

### **Split the Intelligence Module First** (6-9 months)

- ML team wants Python/specialized tools
- Needs GPU instances for training
- Different deployment cycle for experiments
- Can fail without affecting core banking

### **Split the Accounts Module** (9-12 months)

- Compliance requires data isolation
- Need higher security for financial data
- Want to scale reads independently
- Banking integrations need isolation

### **Keep These Together Longer:**

- **Transactions + Budgeting** - Tight coupling, lots of shared logic
- **Household + Permissions** - Complex authorization logic
- **Audit/Events** - Cross-cutting concern

## Key Success Factors

### **1. Strict Module Boundaries**

```csharp
// ❌ BAD: Direct dependency
public class BudgetService {
    private readonly AccountRepository _accountRepo; // Direct access!
}

// ✅ GOOD: Module interface
public class BudgetService {
    private readonly IAccountsModule _accountsModule; // Through interface!
}
```

### **2. Separate Schemas Now**

```sql
-- Each module gets its own schema
CREATE SCHEMA accounts;
CREATE SCHEMA transactions;
CREATE SCHEMA budgeting;

-- Makes database splitting trivial later
```

### **3. Module Communication Patterns**

```csharp
// Synchronous: Direct module calls (becomes HTTP later)
var account = await _accountsModule.GetAccountAsync(id);

// Asynchronous: Domain events (becomes message queue later)
_accountsModule.AccountCreated += OnAccountCreated;
```

### **4. Independent Testing**

Each module should have its own test project with mocked dependencies, making it easy to test in isolation when it becomes a service.

## Timeline Recommendation

**Months 1-6: Pure Modular Monolith**

- Focus on features, not infrastructure
- Establish module boundaries
- Build domain events system
- Monitor module interactions

**Months 6-9: Prepare for First Split**

- Add HTTP APIs to modules
- Implement module proxies
- Add distributed tracing
- Set up container orchestration

**Months 9-12: First Federation**

- Extract Intelligence service (lowest risk)
- Keep others as monolith
- Learn from the experience
- Monitor performance/complexity

**Year 2+: Selective Federation**

- Only split what needs splitting
- Some modules may stay together forever
- Let team size and pain points guide you

## The Best Part

With this approach, you can stay as a monolith **forever** if it works for you. Many successful companies run modular monoliths at scale. You only pay the microservices complexity tax when you actually need the benefits.

The key is having the **option** to evolve without a rewrite. This architecture gives you that option while keeping things simple today.

**human** (Aug 29, 2025, 11:00 PM)

And I could stick with a hexagonal architecture with a modular monolith?

**assistant** (Aug 29, 2025, 11:03 PM)

 Absolutely! Hexagonal architecture and modular monolith are a **perfect combination**. In fact, they complement each other beautifully. Let me show you how to structure this:

#### create Hexagonal Architecture with Modular Monolith

```csharp
// ============================================
// HEXAGONAL ARCHITECTURE + MODULAR MONOLITH
// Best of both worlds: Clean architecture per module
// ============================================

/*
ARCHITECTURE OVERVIEW:

Each Module is a Hexagon:
------------------------
                     ┌─────────────────────────────────────┐
                     │         MODULAR MONOLITH            │
                     │                                     │
    ┌────────────────┼────────────────┬────────────────────┼────────────────┐
    │                │                │                    │                │
    │   ┌────────────▼──────────┐     │      ┌────────────▼──────────┐     │
    │   │   ACCOUNTS MODULE     │     │      │  TRANSACTIONS MODULE  │     │
    │   │                       │     │      │                       │     │
    │   │   ┌─────────────┐    │     │      │   ┌─────────────┐    │     │
    │   │   │   Domain    │    │     │      │   │   Domain    │    │     │
    │   │   │   (Core)    │    │     │      │   │   (Core)    │    │     │
    │   │   └──────┬──────┘    │     │      │   └──────┬──────┘    │     │
    │   │          │            │     │      │          │            │     │
    │   │   ┌──────▼──────┐    │     │      │   ┌──────▼──────┐    │     │
    │   │   │ Application │    │◄────┼──────►   │ Application │    │     │
    │   │   │   (Ports)   │    │     │      │   │   (Ports)   │    │     │
    │   │   └──────┬──────┘    │     │      │   └──────┬──────┘    │     │
    │   │          │            │     │      │          │            │     │
    │   │   ┌──────▼──────┐    │     │      │   ┌──────▼──────┐    │     │
    │   │   │Infrastructure│    │     │      │   │Infrastructure│    │     │
    │   │   │  (Adapters) │    │     │      │   │  (Adapters) │    │     │
    │   │   └─────────────┘    │     │      │   └─────────────┘    │     │
    │   └───────────────────────┘     │      └───────────────────────┘     │
    │                                 │                                    │
    └─────────────────────────────────┴────────────────────────────────────┘
                                      │
                            Module Communication
                            (Through Ports/Interfaces)

KEY PRINCIPLES:
1. Each module is its own hexagon with clean architecture
2. Modules communicate through well-defined ports
3. Domain logic is isolated in the center of each hexagon
4. Infrastructure concerns are at the edges
5. Module boundaries align with bounded contexts
*/

// ============================================
// SOLUTION STRUCTURE
// ============================================

/*
FinanceTracker/
├── Modules/
│   ├── Accounts/                              # Accounts Bounded Context
│   │   ├── Core/
│   │   │   └── FinanceTracker.Accounts.Domain/
│   │   │       ├── Aggregates/
│   │   │       ├── Entities/
│   │   │       ├── ValueObjects/
│   │   │       ├── DomainServices/
│   │   │       └── Ports/                    # Outgoing ports
│   │   │           ├── IAccountRepository.cs
│   │   │           ├── IEventPublisher.cs
│   │   │           └── IEmailService.cs
│   │   │
│   │   ├── FinanceTracker.Accounts.Application/
│   │   │   ├── Commands/
│   │   │   ├── Queries/
│   │   │   ├── Services/
│   │   │   └── Ports/                        # Incoming ports
│   │   │       └── IAccountsModule.cs
│   │   │
│   │   └── Adapters/
│   │       ├── FinanceTracker.Accounts.Infrastructure/
│   │       │   ├── Persistence/              # Outgoing adapter
│   │       │   ├── EventPublishing/          # Outgoing adapter
│   │       │   └── ExternalServices/         # Outgoing adapter
│   │       │
│   │       └── FinanceTracker.Accounts.GraphQL/
│   │           ├── Types/                    # Incoming adapter
│   │           ├── Queries/                  # Incoming adapter
│   │           └── Mutations/                # Incoming adapter
│   │
│   ├── Transactions/                         # Same hexagonal structure
│   │   ├── Core/
│   │   ├── Application/
│   │   └── Adapters/
│   │
│   └── Budgeting/                           # Same hexagonal structure
│       ├── Core/
│       ├── Application/
│       └── Adapters/
│
├── SharedKernel/                             # Truly shared domain concepts
│   └── FinanceTracker.SharedKernel/
│       ├── ValueObjects/
│       │   └── Money.cs
│       ├── Interfaces/
│       └── BaseClasses/
│
├── CrossCutting/                             # Infrastructure shared across modules
│   └── FinanceTracker.CrossCutting/
│       ├── Authentication/
│       ├── Logging/
│       └── Caching/
│
└── Host/
    └── FinanceTracker.WebAPI/               # Composition root
        └── Program.cs
*/

// ============================================
// ACCOUNTS MODULE - HEXAGONAL ARCHITECTURE
// ============================================

// ====== DOMAIN LAYER (Center of Hexagon) ======

// Accounts.Domain/Aggregates/Account.cs
namespace FinanceTracker.Accounts.Domain.Aggregates;

// Pure domain logic, no dependencies on infrastructure
public class Account : AggregateRoot
{
    public AccountId Id { get; private set; }
    public UserId OwnerId { get; private set; }
    public AccountName Name { get; private set; }
    public Money Balance { get; private set; }
    public AccountType Type { get; private set; }
    public AccountStatus Status { get; private set; }
    
    // Domain events
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyList<IDomainEvent> Events => _events;
    
    // Factory method enforces invariants
    public static Account Open(
        UserId ownerId,
        AccountName name,
        AccountType type,
        Money initialDeposit)
    {
        // Domain rules enforced here
        if (type == AccountType.Checking && initialDeposit < Money.Create(100, initialDeposit.Currency))
            throw new DomainException("Checking accounts require minimum $100 deposit");
        
        var account = new Account
        {
            Id = AccountId.Generate(),
            OwnerId = ownerId,
            Name = name,
            Type = type,
            Balance = initialDeposit,
            Status = AccountStatus.Active
        };
        
        account._events.Add(new AccountOpenedEvent(account.Id, ownerId, initialDeposit));
        return account;
    }
    
    // Domain operations
    public void Deposit(Money amount)
    {
        if (Status != AccountStatus.Active)
            throw new DomainException("Cannot deposit to inactive account");
        
        Balance = Balance.Add(amount);
        _events.Add(new MoneyDepositedEvent(Id, amount));
    }
    
    public void Withdraw(Money amount)
    {
        if (Status != AccountStatus.Active)
            throw new DomainException("Cannot withdraw from inactive account");
        
        if (Balance < amount)
            throw new InsufficientFundsException(Balance, amount);
        
        Balance = Balance.Subtract(amount);
        _events.Add(new MoneyWithdrawnEvent(Id, amount));
    }
    
    public void Freeze(FreezeReason reason)
    {
        if (Status == AccountStatus.Frozen)
            throw new DomainException("Account is already frozen");
        
        Status = AccountStatus.Frozen;
        _events.Add(new AccountFrozenEvent(Id, reason));
    }
}

// Accounts.Domain/ValueObjects/AccountId.cs
namespace FinanceTracker.Accounts.Domain.ValueObjects;

public record AccountId
{
    public Guid Value { get; }
    
    private AccountId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("AccountId cannot be empty");
        Value = value;
    }
    
    public static AccountId Create(Guid value) => new(value);
    public static AccountId Generate() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

// Accounts.Domain/Ports/IAccountRepository.cs (Outgoing Port)
namespace FinanceTracker.Accounts.Domain.Ports;

// Port interface - Domain defines what it needs
public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(AccountId id, CancellationToken ct = default);
    Task<IEnumerable<Account>> GetByOwnerAsync(UserId ownerId, CancellationToken ct = default);
    Task SaveAsync(Account account, CancellationToken ct = default);
    Task<bool> ExistsAsync(AccountId id, CancellationToken ct = default);
}

// Accounts.Domain/Ports/IEventPublisher.cs (Outgoing Port)
public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) 
        where TEvent : IDomainEvent;
}

// ====== APPLICATION LAYER (Ports) ======

// Accounts.Application/Ports/IAccountsModule.cs (Incoming Port)
namespace FinanceTracker.Accounts.Application.Ports;

// This is the public API of the Accounts module
public interface IAccountsModule
{
    // Commands
    Task<AccountDto> OpenAccountAsync(OpenAccountCommand command, CancellationToken ct = default);
    Task DepositAsync(DepositCommand command, CancellationToken ct = default);
    Task WithdrawAsync(WithdrawCommand command, CancellationToken ct = default);
    Task FreezeAccountAsync(FreezeAccountCommand command, CancellationToken ct = default);
    
    // Queries
    Task<AccountDto?> GetAccountAsync(AccountId id, CancellationToken ct = default);
    Task<IEnumerable<AccountDto>> GetUserAccountsAsync(UserId userId, CancellationToken ct = default);
    Task<Money> GetBalanceAsync(AccountId id, CancellationToken ct = default);
    
    // Events that other modules can subscribe to
    event EventHandler<AccountOpenedEvent> AccountOpened;
    event EventHandler<MoneyDepositedEvent> MoneyDeposited;
    event EventHandler<MoneyWithdrawnEvent> MoneyWithdrawn;
}

// Accounts.Application/Commands/OpenAccountCommand.cs
namespace FinanceTracker.Accounts.Application.Commands;

public record OpenAccountCommand(
    Guid UserId,
    string AccountName,
    AccountType Type,
    decimal InitialDeposit,
    string Currency) : ICommand<AccountDto>;

public class OpenAccountCommandHandler : ICommandHandler<OpenAccountCommand, AccountDto>
{
    private readonly IAccountRepository _repository;        // Outgoing port
    private readonly IEventPublisher _eventPublisher;       // Outgoing port
    private readonly ICurrentUserService _currentUser;      // Cross-cutting concern
    
    public OpenAccountCommandHandler(
        IAccountRepository repository,
        IEventPublisher eventPublisher,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
        _currentUser = currentUser;
    }
    
    public async Task<AccountDto> Handle(OpenAccountCommand command, CancellationToken ct)
    {
        // Validate user
        if (command.UserId != _currentUser.UserId)
            throw new UnauthorizedException("Cannot open account for another user");
        
        // Create domain object
        var account = Account.Open(
            UserId.Create(command.UserId),
            AccountName.Create(command.AccountName),
            command.Type,
            Money.Create(command.InitialDeposit, command.Currency));
        
        // Persist through port
        await _repository.SaveAsync(account, ct);
        
        // Publish events through port
        foreach (var @event in account.Events)
        {
            await _eventPublisher.PublishAsync(@event, ct);
        }
        
        // Return DTO
        return AccountDto.FromDomain(account);
    }
}

// Accounts.Application/Services/AccountsModuleService.cs
namespace FinanceTracker.Accounts.Application.Services;

// Implementation of the module's public interface
public class AccountsModuleService : IAccountsModule
{
    private readonly IMediator _mediator;
    
    public event EventHandler<AccountOpenedEvent>? AccountOpened;
    public event EventHandler<MoneyDepositedEvent>? MoneyDeposited;
    public event EventHandler<MoneyWithdrawnEvent>? MoneyWithdrawn;
    
    public AccountsModuleService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<AccountDto> OpenAccountAsync(OpenAccountCommand command, CancellationToken ct = default)
    {
        var result = await _mediator.Send(command, ct);
        
        // Raise module event for other modules
        AccountOpened?.Invoke(this, new AccountOpenedEvent(/* ... */));
        
        return result;
    }
    
    public async Task<AccountDto?> GetAccountAsync(AccountId id, CancellationToken ct = default)
    {
        return await _mediator.Send(new GetAccountQuery(id), ct);
    }
}

// ====== INFRASTRUCTURE LAYER (Adapters) ======

// Accounts.Infrastructure/Persistence/AccountRepository.cs (Outgoing Adapter)
namespace FinanceTracker.Accounts.Infrastructure.Persistence;

// Adapter implements the port interface
public class AccountRepository : IAccountRepository
{
    private readonly AccountsDbContext _context;
    private readonly IMapper _mapper;
    
    public AccountRepository(AccountsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Account?> GetByIdAsync(AccountId id, CancellationToken ct = default)
    {
        var entity = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id.Value, ct);
        
        return entity == null ? null : _mapper.MapToDomain(entity);
    }
    
    public async Task SaveAsync(Account account, CancellationToken ct = default)
    {
        var entity = _mapper.MapToEntity(account);
        
        if (_context.Entry(entity).State == EntityState.Detached)
            _context.Accounts.Add(entity);
        
        await _context.SaveChangesAsync(ct);
    }
}

// Accounts.Infrastructure/EventPublishing/EventPublisher.cs (Outgoing Adapter)
public class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;
    private readonly IEventStore _eventStore;
    
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
        where TEvent : IDomainEvent
    {
        // Store event
        await _eventStore.AppendAsync(@event, ct);
        
        // Publish to in-process handlers
        await _mediator.Publish(@event, ct);
        
        // In future: publish to message bus for other services
        // await _messageBus.PublishAsync(@event, ct);
    }
}

// Accounts.GraphQL/AccountQueries.cs (Incoming Adapter)
namespace FinanceTracker.Accounts.GraphQL;

[ExtendObjectType("Query")]
public class AccountQueries
{
    public async Task<AccountDto?> GetAccount(
        Guid id,
        [Service] IAccountsModule accountsModule)
    {
        return await accountsModule.GetAccountAsync(AccountId.Create(id));
    }
    
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<AccountDto>> GetAccounts(
        [Service] IAccountsModule accountsModule,
        [Service] ICurrentUserService currentUser)
    {
        return await accountsModule.GetUserAccountsAsync(UserId.Create(currentUser.UserId));
    }
}

// ============================================
// TRANSACTIONS MODULE - HEXAGONAL ARCHITECTURE
// ============================================

// Transactions.Application/Services/TransactionsModuleService.cs
namespace FinanceTracker.Transactions.Application.Services;

public class TransactionsModuleService : ITransactionsModule
{
    private readonly IMediator _mediator;
    private readonly IAccountsModule _accountsModule;  // Port to another module
    
    public TransactionsModuleService(
        IMediator mediator,
        IAccountsModule accountsModule)  // Injected port
    {
        _mediator = mediator;
        _accountsModule = accountsModule;
        
        // Subscribe to events from Accounts module
        _accountsModule.AccountOpened += OnAccountOpened;
        _accountsModule.MoneyDeposited += OnMoneyDeposited;
        _accountsModule.MoneyWithdrawn += OnMoneyWithdrawn;
    }
    
    private async void OnMoneyDeposited(object? sender, MoneyDepositedEvent e)
    {
        // Create automatic transaction record
        await CreateTransactionAsync(new CreateTransactionCommand(
            e.AccountId,
            e.Amount,
            TransactionType.Credit,
            "Deposit",
            DateTime.UtcNow));
    }
    
    public async Task<TransactionDto> CreateTransactionAsync(
        CreateTransactionCommand command,
        CancellationToken ct = default)
    {
        // Validate account exists (cross-module communication through port)
        var account = await _accountsModule.GetAccountAsync(command.AccountId, ct);
        if (account == null)
            throw new NotFoundException($"Account {command.AccountId} not found");
        
        return await _mediator.Send(command, ct);
    }
}

// ============================================
// BUDGETING MODULE - HEXAGONAL ARCHITECTURE
// ============================================

// Budgeting.Domain/Services/BudgetCalculator.cs
namespace FinanceTracker.Budgeting.Domain.Services;

// Domain service with pure business logic
public class BudgetCalculator
{
    public BudgetPerformance CalculatePerformance(
        Budget budget,
        IEnumerable<Transaction> transactions)
    {
        // Pure domain logic for budget calculations
        var spent = transactions
            .Where(t => t.Date >= budget.Period.StartDate)
            .Where(t => t.Date <= budget.Period.EndDate)
            .Sum(t => t.Amount);
        
        return new BudgetPerformance
        {
            BudgetId = budget.Id,
            Allocated = budget.TotalAllocated,
            Spent = spent,
            Remaining = budget.TotalAllocated - spent,
            PercentageUsed = budget.TotalAllocated > 0 
                ? (spent / budget.TotalAllocated) * 100 
                : 0
        };
    }
}

// Budgeting.Application/Services/BudgetingModuleService.cs
namespace FinanceTracker.Budgeting.Application.Services;

public class BudgetingModuleService : IBudgetingModule
{
    private readonly ITransactionsModule _transactionsModule;  // Port to Transactions
    private readonly IAccountsModule _accountsModule;          // Port to Accounts
    private readonly BudgetCalculator _calculator;             // Domain service
    
    public BudgetingModuleService(
        ITransactionsModule transactionsModule,
        IAccountsModule accountsModule,
        BudgetCalculator calculator)
    {
        _transactionsModule = transactionsModule;
        _accountsModule = accountsModule;
        _calculator = calculator;
        
        // React to transaction events
        _transactionsModule.TransactionCreated += OnTransactionCreated;
    }
    
    private async void OnTransactionCreated(object? sender, TransactionCreatedEvent e)
    {
        // Update budget performance when new transaction arrives
        await UpdateBudgetPerformanceAsync(e.AccountId, e.Amount);
    }
}

// ============================================
// MODULE REGISTRATION (Composition Root)
// ============================================

// WebAPI/Program.cs
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Register each module with its hexagonal architecture
        builder.Services.AddAccountsModule();
        builder.Services.AddTransactionsModule();
        builder.Services.AddBudgetingModule();
        builder.Services.AddIntelligenceModule();
        
        var app = builder.Build();
        app.MapGraphQL();
        app.Run();
    }
}

// WebAPI/ModuleExtensions/AccountsModuleExtensions.cs
public static class AccountsModuleExtensions
{
    public static IServiceCollection AddAccountsModule(this IServiceCollection services)
    {
        // Domain services
        services.AddScoped<AccountDomainService>();
        
        // Application layer (ports)
        services.AddScoped<IAccountsModule, AccountsModuleService>();
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<OpenAccountCommand>());
        
        // Infrastructure (adapters)
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddDbContext<AccountsDbContext>(options =>
            options.UseNpgsql("...", npgsql => 
                npgsql.MigrationsAssembly("FinanceTracker.Accounts.Infrastructure")));
        
        // GraphQL (incoming adapter)
        services.AddGraphQLServer()
            .AddTypeExtension<AccountQueries>()
            .AddTypeExtension<AccountMutations>();
        
        return services;
    }
}

// ============================================
// TESTING STRATEGY
// ============================================

// Tests/Accounts.Domain.Tests/AccountTests.cs
public class AccountTests
{
    [Fact]
    public void Open_WithInsufficientInitialDeposit_ThrowsException()
    {
        // Pure domain testing - no mocks needed!
        Assert.Throws<DomainException>(() =>
            Account.Open(
                UserId.Create(Guid.NewGuid()),
                AccountName.Create("Test"),
                AccountType.Checking,
                Money.Create(50, "USD"))); // Less than $100
    }
    
    [Fact]
    public void Withdraw_WithInsufficientFunds_ThrowsException()
    {
        // Arrange
        var account = Account.Open(
            UserId.Create(Guid.NewGuid()),
            AccountName.Create("Test"),
            AccountType.Checking,
            Money.Create(100, "USD"));
        
        // Act & Assert
        Assert.Throws<InsufficientFundsException>(() =>
            account.Withdraw(Money.Create(150, "USD")));
    }
}

// Tests/Accounts.Application.Tests/OpenAccountCommandHandlerTests.cs
public class OpenAccountCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_SavesAccountAndPublishesEvents()
    {
        // Arrange - Mock the ports
        var repository = new Mock<IAccountRepository>();
        var eventPublisher = new Mock<IEventPublisher>();
        var currentUser = new Mock<ICurrentUserService>();
        
        currentUser.Setup(x => x.UserId).Returns(Guid.NewGuid());
        
        var handler = new OpenAccountCommandHandler(
            repository.Object,
            eventPublisher.Object,
            currentUser.Object);
        
        var command = new OpenAccountCommand(
            currentUser.Object.UserId,
            "Checking",
            AccountType.Checking,
            100,
            "USD");
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        repository.Verify(x => x.SaveAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Once);
        eventPublisher.Verify(x => x.PublishAsync(It.IsAny<AccountOpenedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
    }
}

// ============================================
// BENEFITS OF HEXAGONAL + MODULAR MONOLITH
// ============================================

/*
1. CLEAN BOUNDARIES AT TWO LEVELS:
   - Module boundaries (future service boundaries)
   - Hexagonal boundaries (domain vs infrastructure)

2. TESTABILITY:
   - Pure domain logic tests (no mocks)
   - Application tests with mocked ports
   - Module integration tests
   - Full system tests

3. FLEXIBILITY:
   - Swap infrastructure without touching domain
   - Move modules to services without refactoring
   - Change databases per module
   - Different teams own different hexagons

4. MAINTAINABILITY:
   - Clear separation of concerns
   - Domain logic is protected
   - Infrastructure changes don't affect business rules
   - Module changes don't cascade

5. SCALABILITY PATH:
   - Start: Modules in same process
   - Later: Modules in different containers
   - Finally: Modules as separate services
   - No code changes required!

6. TEAM AUTONOMY:
   - Each team owns their hexagon
   - Clear interfaces between teams
   - Independent testing and deployment (later)
   - Different tech choices per adapter

This combination gives you the best of all worlds:
- Clean architecture (hexagonal)
- Service boundaries (modular)
- Simple deployment (monolith)
- Future flexibility (can become microservices)
*/
```

Perfect combination! Hexagonal architecture + Modular Monolith gives you the best of all worlds. Here's why this is so powerful:

## The Double-Layer Protection

### **Layer 1: Module Boundaries** (Vertical Slicing)

- Each module = future microservice
- Modules communicate through interfaces (ports)
- Clear team ownership boundaries

### **Layer 2: Hexagonal Architecture** (Horizontal Slicing)

- Domain logic protected in the center
- Infrastructure at the edges (adapters)
- Business rules independent of technology

## Key Benefits of This Combination

### **1. Ultimate Testability**

```csharp
// Domain layer: Pure unit tests, no mocks
[Fact]
public void Account_Withdraw_InsufficientFunds_Throws() {
    var account = Account.Open(/*...*/);
    Assert.Throws<InsufficientFundsException>(() => 
        account.Withdraw(Money.Create(1000, "USD")));
}

// Application layer: Mock only the ports
[Fact]
public async Task OpenAccount_ValidCommand_SavesAccount() {
    var mockRepo = new Mock<IAccountRepository>(); // Mock port
    var handler = new OpenAccountCommandHandler(mockRepo.Object);
    // Test without any infrastructure
}

// Module integration: Test module interactions
[Fact]
public async Task Transaction_Created_Updates_Budget() {
    // Test how modules interact through ports
}
```

### **2. Perfect Migration Path**

**Today (Monolith):**

```csharp
public class TransactionsModule {
    private readonly IAccountsModule _accounts; // Direct reference
    
    public async Task CreateTransaction(command) {
        var account = await _accounts.GetAccountAsync(id); // In-process call
    }
}
```

**Tomorrow (Microservices):**

```csharp
public class TransactionsModule {
    private readonly IAccountsModule _accounts; // Same interface!
    
    public async Task CreateTransaction(command) {
        var account = await _accounts.GetAccountAsync(id); // HTTP call
    }
}
// Just swap the adapter implementation!
```

### **3. Technology Flexibility Per Module**

Each module can have different:

- **Databases**: Accounts → PostgreSQL, Analytics → ClickHouse
- **Caching**: Transactions → Redis, ML → In-memory
- **Frameworks**: Core → .NET, ML → Python (later)

### **4. Clear Ownership**

```
Team A owns Accounts Module:
├── Domain (business rules)
├── Application (use cases)
├── Infrastructure (their choice of DB)
└── GraphQL (their API design)

Team B owns Budgeting Module:
├── Domain (budget algorithms)
├── Application (budget operations)
├── Infrastructure (maybe different DB)
└── GraphQL (their schema)
```

### **5. Gradual Complexity**

Start simple, add complexity only when needed:

**Phase 1**: Single database, modules in folders
**Phase 2**: Separate schemas per module
**Phase 3**: Separate databases per module
**Phase 4**: Extract first module to service
**Phase 5**: Full microservices (if needed)

## Practical Implementation Tips

### **1. Module Communication Rules**

```csharp
// ✅ GOOD: Through ports
public interface IAccountsModule {
    Task<AccountDto> GetAccountAsync(AccountId id);
}

// ❌ BAD: Direct domain access
public class BudgetService {
    private readonly Account _account; // Never do this!
}
```

### **2. Shared Kernel Rules**

Only truly universal concepts:

```csharp
// SharedKernel - Used by ALL modules
public record Money(decimal Amount, string Currency);
public record UserId(Guid Value);

// NOT SharedKernel - Module specific
public class Account { } // Only in Accounts module
public class Budget { }  // Only in Budgeting module
```

### **3. Event Communication**

```csharp
// Module raises domain event
public class AccountsModule {
    public event EventHandler<AccountOpenedEvent> AccountOpened;
}

// Other modules subscribe
public class BudgetingModule {
    public BudgetingModule(IAccountsModule accounts) {
        accounts.AccountOpened += OnAccountOpened;
    }
}
```

### **4. Testing Strategy**

- **Unit tests**: Test domain logic (no infrastructure)
- **Integration tests**: Test module interactions
- **Contract tests**: Verify module interfaces
- **E2E tests**: Test complete user flows

## When This Architecture Shines

This combination is perfect when you:

- Want clean architecture but not microservices complexity
- Have a small team but expect to grow
- Need flexibility to evolve architecture
- Want to protect business logic from infrastructure changes
- Plan to potentially extract services later

## The Bottom Line

Hexagonal + Modular Monolith gives you:

- **Simplicity of monolith** (one deployment, one database)
- **Clean architecture benefits** (testable, maintainable)
- **Future flexibility** (can become microservices)
- **Team autonomy** (clear boundaries)

You get all the architectural benefits without the operational complexity. It's the perfect starting point that can evolve with your needs!
