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