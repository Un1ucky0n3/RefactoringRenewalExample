namespace LegacyRenewalApp.Tax;

public class TaxCalculator : ITaxCalculator
{
    public void Calculate(TaxContext context)
    {
        var taxPolicy = new TaxPolicy();
        decimal taxRate = taxPolicy.GetRate(context.Country);
        
        decimal taxBase = context.SubtotalAfterDiscount + context.SupportFee + context.PaymentFee;
        context.TaxAmount = taxBase * taxRate;
        context.FinalAmount = taxBase + context.TaxAmount;
        if (context.FinalAmount < 500m)
        {
            context.FinalAmount = 500m;
            context.Notes += "minimum invoice amount applied; ";
        }
    }
}