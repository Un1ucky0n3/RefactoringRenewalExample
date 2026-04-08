using System.Collections.Generic;

namespace LegacyRenewalApp.Tax;

public class TaxPolicy : ITaxPolicy
{
    private static readonly Dictionary<string, decimal> Rates = new()
    {
        ["Poland"] = 0.23m,
        ["Germany"] = 0.19m,
        ["Czech Republic"] = 0.21m,
        ["Norway"] = 0.25m
    };

    public decimal GetRate(string country)
    {
        return Rates.TryGetValue(country, out var rate) ? rate : 0.20m;
    }
}