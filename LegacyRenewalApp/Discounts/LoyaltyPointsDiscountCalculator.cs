using System.Collections.Generic;

namespace LegacyRenewalApp;
public class LoyaltyPointsDiscountCalculator : IDiscountCalculator
{
    public (decimal discount, string note) CalculateDiscount(DiscountContext context)
    {
        int points = context.LoyaltyPoints;
        points = points > 200 ? 200 : points;
        IDiscountStrategy strategy = GetStrategy(context);

        var discount = strategy.CalculateDiscount(context.baseAmount);
        var note = strategy.AddNote();
        var noteArray = note.Split('&');
        if(noteArray.Length != 1)
            note = noteArray[0]+points+noteArray[1];
        else{
            note = string.Empty;
        }

        return (discount, note);
    }
    public List<IDiscountStrategy> CreateListOfStrategies()
    {
        return new List<IDiscountStrategy>()
        {
            new DefaultLoyaltyPointsDiscount()
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
class DefaultLoyaltyPointsDiscount : IDiscountStrategy
{
    public bool canApply(DiscountContext context)
    {
        return context.LoyaltyPoints > 0;
    }
    public decimal CalculateDiscount(decimal pointsToUse)
    {
        return pointsToUse;
    }
    public string AddNote()
    {
        return "loyalty points used: &; ";
    }
}