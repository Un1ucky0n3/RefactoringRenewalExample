using System.Collections.Generic;

namespace LegacyRenewalApp.SupportFee;

public class FeeCalculatorFactory
{
    private List<IFeeCalculator> _calculators = new List<IFeeCalculator>()
    {
        new SupportFeeCalculator(),
        new PaymentFeeCalculator()
    };
    
    private FeeContext _context;

    public FeeCalculatorFactory(FeeContext context)
    {
        _context = context;
    }
    public void Run()
    {
        foreach (var calculator in _calculators)
        {
            calculator.Calculate(_context);
        }
    }
}