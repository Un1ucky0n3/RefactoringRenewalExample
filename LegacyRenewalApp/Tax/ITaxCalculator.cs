namespace LegacyRenewalApp.Tax;

public interface ITaxCalculator
{
    public void Calculate(TaxContext context);
}