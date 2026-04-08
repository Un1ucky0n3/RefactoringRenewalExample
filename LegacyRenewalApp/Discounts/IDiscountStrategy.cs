namespace LegacyRenewalApp;

public interface IDiscountStrategy
{
    public bool canApply(DiscountContext context);
    public decimal CalculateDiscount(decimal baseAmount);
    public string AddNote();
}