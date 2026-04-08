using System.Collections.Generic;

namespace LegacyRenewalApp;

public interface IDiscountCalculator
{
    public (decimal discount, string note) CalculateDiscount(DiscountContext context);
    public List<IDiscountStrategy> CreateListOfStrategies();
    public IDiscountStrategy GetStrategy(DiscountContext context);
}