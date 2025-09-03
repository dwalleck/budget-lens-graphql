namespace BudgetLens.Core.Domain.Accounts;

/// <summary>
/// Value object representing a monetary amount with currency.
/// Ensures precision and currency consistency in financial calculations.
/// </summary>
public readonly record struct Money(decimal Amount, Currency Currency)
{
    /// <summary>
    /// Zero amount in the specified currency.
    /// </summary>
    public static Money Zero(Currency currency) => new(0, currency);
    
    /// <summary>
    /// Creates a new Money instance from a decimal amount and currency.
    /// </summary>
    public static Money From(decimal amount, Currency currency)
    {
        return new Money(Math.Round(amount, currency.GetDecimalPlaces(), MidpointRounding.AwayFromZero), currency);
    }
    
    /// <summary>
    /// Creates a new Money instance from a double amount and currency.
    /// </summary>
    public static Money From(double amount, Currency currency)
    {
        return From((decimal)amount, currency);
    }
    
    /// <summary>
    /// Adds two Money amounts. Both must have the same currency.
    /// </summary>
    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "addition");
        return new Money(left.Amount + right.Amount, left.Currency);
    }
    
    /// <summary>
    /// Subtracts two Money amounts. Both must have the same currency.
    /// </summary>
    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "subtraction");
        return new Money(left.Amount - right.Amount, left.Currency);
    }
    
    /// <summary>
    /// Multiplies Money by a scalar value.
    /// </summary>
    public static Money operator *(Money money, decimal multiplier)
    {
        return From(money.Amount * multiplier, money.Currency);
    }
    
    /// <summary>
    /// Multiplies Money by a scalar value.
    /// </summary>
    public static Money operator *(decimal multiplier, Money money)
    {
        return money * multiplier;
    }
    
    /// <summary>
    /// Divides Money by a scalar value.
    /// </summary>
    public static Money operator /(Money money, decimal divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Cannot divide money by zero");
            
        return From(money.Amount / divisor, money.Currency);
    }
    
    /// <summary>
    /// Returns the absolute value of the Money amount.
    /// </summary>
    public Money Abs()
    {
        return new Money(Math.Abs(Amount), Currency);
    }
    
    /// <summary>
    /// Returns the negation of the Money amount.
    /// </summary>
    public Money Negate()
    {
        return new Money(-Amount, Currency);
    }
    
    /// <summary>
    /// Returns true if the amount is positive.
    /// </summary>
    public bool IsPositive => Amount > 0;
    
    /// <summary>
    /// Returns true if the amount is negative.
    /// </summary>
    public bool IsNegative => Amount < 0;
    
    /// <summary>
    /// Returns true if the amount is zero.
    /// </summary>
    public bool IsZero => Amount == 0;
    
    /// <summary>
    /// Compares two Money amounts. Both must have the same currency.
    /// </summary>
    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "comparison");
        return left.Amount > right.Amount;
    }
    
    /// <summary>
    /// Compares two Money amounts. Both must have the same currency.
    /// </summary>
    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "comparison");
        return left.Amount < right.Amount;
    }
    
    /// <summary>
    /// Compares two Money amounts. Both must have the same currency.
    /// </summary>
    public static bool operator >=(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "comparison");
        return left.Amount >= right.Amount;
    }
    
    /// <summary>
    /// Compares two Money amounts. Both must have the same currency.
    /// </summary>
    public static bool operator <=(Money left, Money right)
    {
        EnsureSameCurrency(left, right, "comparison");
        return left.Amount <= right.Amount;
    }
    
    /// <summary>
    /// Returns a formatted string representation of the money amount.
    /// </summary>
    public override string ToString()
    {
        var decimalPlaces = Currency.GetDecimalPlaces();
        return $"{Currency.GetSymbol()}{Amount.ToString($"N{decimalPlaces}")}";
    }
    
    /// <summary>
    /// Returns a formatted string representation with currency code.
    /// </summary>
    public string ToStringWithCurrency()
    {
        var decimalPlaces = Currency.GetDecimalPlaces();
        return $"{Amount.ToString($"N{decimalPlaces}")} {Currency}";
    }
    
    private static void EnsureSameCurrency(Money left, Money right, string operation)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException(
                $"Cannot perform {operation} on different currencies: {left.Currency} and {right.Currency}");
        }
    }
}