using System;
using System.Collections.Generic;

namespace LegacyRenewalApp;

public class YearsWithCompanyDiscountCalculator : IDiscountCalculator
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
            new FiveYearsDiscount(),
            new TwoYearsDiscount()
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

class FiveYearsDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.YearsWithCompany >= 5;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.07m;
    }
    public string AddNote()
    {
        return "long-term loyalty discount; ";
    }
}

class TwoYearsDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.SeatCount >= 2 &&  context.YearsWithCompany < 5;
    }
    public decimal CalculateDiscount(decimal baseAmount)
    {
        return baseAmount * 0.03m;
    }

    public string AddNote()
    {
        return "basic loyalty discount; ";
    }
}