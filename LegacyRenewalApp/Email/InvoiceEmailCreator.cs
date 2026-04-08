namespace LegacyRenewalApp.Email;

public class InvoiceEmailCreator : IEmailCreator
{
    private RenewalInvoice Invoice;
    private Customer Customer;
    private string NormalizedPlanCode;
    
    public InvoiceEmailCreator(RenewalInvoice invoice, Customer customer, string normalizedPlanCode)
    {
        Customer = customer;
        Invoice = invoice;
        NormalizedPlanCode = normalizedPlanCode;
    }
    public (string subject, string body) CreateEmail()
    {
        string subject = "Subscription renewal invoice";
        string body =
            $"Hello {Customer.FullName}, your renewal for plan {NormalizedPlanCode} " +
            $"has been prepared. Final amount: {Invoice.FinalAmount:F2}.";
        return (subject, body);
    }
}