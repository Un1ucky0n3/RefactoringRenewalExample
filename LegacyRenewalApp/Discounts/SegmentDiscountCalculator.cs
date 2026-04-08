using System.Collections.Generic;

namespace LegacyRenewalApp;

public class SegmentDiscountCalculator : IDiscountCalculator
{
    public (decimal discount, string note) CalculateDiscount(DiscountContext context)
    {
        IDiscountStrategy strategy = GetStrategy(context);

        var discount = strategy.CalculateDiscount(context.baseAmount);
        var note = strategy.AddNote();

        return (discount, note);
    }

    public List<IDiscountStrategy> CreateListOfStrategies()
    {
        return new List<IDiscountStrategy>()
        {
            new DiscountStrategyGold(),
            new DiscountStrategySilver(),
            new DiscountStrategyEducation(),
            new DiscountStrategyPlatinum(),
        };
    }

    public IDiscountStrategy GetStrategy(DiscountContext context)
    {
        foreach (var strategy in CreateListOfStrategies())
        {
            if(strategy.canApply(context)) return strategy;
        }
        return new NoDiscount();
    }
}

class DiscountStrategyPlatinum : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.Segment.ToUpper() == "PLATINUM";
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.15m;
    }

    public string AddNote()
    {
        return "platinum discount; ";
    }
}

class DiscountStrategyGold : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.Segment.ToUpper() == "GOLD";
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.10m;
    }

    public string AddNote()
    {
        return "gold discount; ";
    }
}

class DiscountStrategySilver : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.Segment.ToUpper() == "SILVER";
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.05m;
    }

    public string AddNote()
    {
        return "silver discount; ";
    }
}
class DiscountStrategyEducation : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.Segment.ToUpper() == "EDUCATION" && context.IsEducationEligible;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.20m;
    }

    public string AddNote()
    {
        return "education discount; ";
    }
}