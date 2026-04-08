using LegacyRenewalApp.SupportFee;
using LegacyRenewalApp.Tax;

namespace LegacyRenewalApp.Invoice;

public class RenewalInvoiceCreationData
{
    public Customer Customer { get; set; }
    public DiscountContext DiscountContext { get; set; }
    public FeeContext FeeContext { get; set; }
    public TaxContext TaxContext { get; set; }
}