using System.Collections.Generic;

namespace LegacyRenewalApp;

public class SeatCountDiscountCalculator : IDiscountCalculator
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
            new FiftySeatCountDiscount(),
            new TwentySeatCountDiscount(),
            new TenSeatCountDiscount()
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

class FiftySeatCountDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.SeatCount >= 50;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.12m;
    }

    public string AddNote()
    {
        return "large team discount; ";
    }
}

class TwentySeatCountDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.SeatCount >= 20 && context.SeatCount < 50;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.08m;
    }

    public string AddNote()
    {
        return "medium team discount; ";
    }
}

class TenSeatCountDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.SeatCount >= 10 && context.SeatCount < 20;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.04m;
    }

    public string AddNote()
    {
        return "small team discount; ";;
    }
}