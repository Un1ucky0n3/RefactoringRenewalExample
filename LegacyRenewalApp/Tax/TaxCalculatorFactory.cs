using System.Collections.Generic;
using LegacyRenewalApp.SupportFee;

namespace LegacyRenewalApp.Tax;

public class TaxCalculatorFactory
{
    private List<ITaxCalculator> _calculators = new List<ITaxCalculator>()
    {
        new TaxCalculator()
    };
    
    private TaxContext _context;

    public TaxCalculatorFactory(TaxContext context)
    {
        _context = context;
    }

    public void Run()
    {
        foreach (var calculator in _calculators)
        {
            
        }
    }
    
}