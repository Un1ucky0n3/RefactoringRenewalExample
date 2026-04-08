using System.Runtime.CompilerServices;

namespace LegacyRenewalApp;

public class NoDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return true;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.0m;
    }

    public string AddNote()
    {
        return string.Empty;
    }
}