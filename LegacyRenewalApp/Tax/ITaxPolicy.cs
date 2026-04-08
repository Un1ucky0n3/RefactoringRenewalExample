namespace LegacyRenewalApp.Tax;

public interface ITaxPolicy
{
    decimal GetRate(string country);
}