using System.ComponentModel;

namespace BudgetLens.Core.Domain.Accounts;

/// <summary>
/// Represents supported currencies in the Budget Lens system.
/// </summary>
public enum Currency
{
    [Description("US Dollar")]
    USD = 840,
    
    [Description("Euro")]
    EUR = 978,
    
    [Description("British Pound")]
    GBP = 826,
    
    [Description("Canadian Dollar")]
    CAD = 124,
    
    [Description("Australian Dollar")]
    AUD = 36,
    
    [Description("Japanese Yen")]
    JPY = 392,
    
    [Description("Swiss Franc")]
    CHF = 756,
    
    [Description("Chinese Yuan")]
    CNY = 156,
    
    [Description("Indian Rupee")]
    INR = 356
}

/// <summary>
/// Extension methods for Currency enum.
/// </summary>
public static class CurrencyExtensions
{
    /// <summary>
    /// Gets the display name for the currency.
    /// </summary>
    public static string GetDisplayName(this Currency currency)
    {
        var field = currency.GetType().GetField(currency.ToString());
        var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        return attribute?.Description ?? currency.ToString();
    }
    
    /// <summary>
    /// Gets the currency symbol.
    /// </summary>
    public static string GetSymbol(this Currency currency)
    {
        return currency switch
        {
            Currency.USD => "$",
            Currency.EUR => "€",
            Currency.GBP => "£",
            Currency.CAD => "C$",
            Currency.AUD => "A$",
            Currency.JPY => "¥",
            Currency.CHF => "CHF",
            Currency.CNY => "¥",
            Currency.INR => "₹",
            _ => currency.ToString()
        };
    }
    
    /// <summary>
    /// Gets the number of decimal places typically used for this currency.
    /// </summary>
    public static int GetDecimalPlaces(this Currency currency)
    {
        return currency switch
        {
            Currency.JPY => 0, // Yen typically doesn't use decimal places
            _ => 2 // Most currencies use 2 decimal places
        };
    }
}