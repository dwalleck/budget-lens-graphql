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
