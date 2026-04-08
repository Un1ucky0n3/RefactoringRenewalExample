using LegacyRenewalApp.Context;

namespace LegacyRenewalApp.Tax;

public class TaxContext : IContext
{
    public decimal SubtotalAfterDiscount { get; set; }
    public decimal SupportFee { get; set; }
    public decimal PaymentFee { get; set; }
    public string Country {get; set;}

    public decimal FinalAmount;
    
    public decimal TaxAmount;
    public string Notes { get; set; }

    public TaxContext(decimal subTotalAfterDiscount, decimal supportFee, decimal paymentFee, string country)
    {
        SubtotalAfterDiscount = subTotalAfterDiscount;
        SupportFee = supportFee;
        PaymentFee = paymentFee;
        Country = country;
        Notes = string.Empty;
        FinalAmount = 0m;
        TaxAmount = 0m;
    }
    
    public string GetNotes()
    {
        return Notes;
    }
}