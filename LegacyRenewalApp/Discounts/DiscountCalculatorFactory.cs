using System.Collections.Generic;
using LegacyRenewalApp.SubTotalPolicy;

namespace LegacyRenewalApp;

public class DiscountCalculatorFactory
{
    private List<IDiscountCalculator> _calculators = new List<IDiscountCalculator>()
    {
        new SegmentDiscountCalculator(),
        new YearsWithCompanyDiscountCalculator(),
        new SeatCountDiscountCalculator(),
        new LoyaltyPointsDiscountCalculator()
    };
    private DiscountContext _context;
    public DiscountCalculatorFactory(DiscountContext context)
    {
        this._context = context;
    }
    public void Run()
    {
        foreach (var calculator in _calculators)
        {
            (decimal discount, string note) data = calculator.CalculateDiscount(_context);
            _context.DiscountAmount += data.discount;
            _context.Notes += data.note;
        }
        calculateSubTotalAfterDiscount();
    }

    private void calculateSubTotalAfterDiscount()
    {
        ISubtotalPolicyCalculator subtotalPolicyCalculator = new MinimumSubtotalPolicyCalculator(300m);
        (decimal subtotalAfterDiscount, string subtotalNote) =  subtotalPolicyCalculator.Apply(_context.baseAmount - _context.DiscountAmount);
        _context.SubtotalAfterDiscount += subtotalAfterDiscount;
        _context.Notes += subtotalNote;
    }
}