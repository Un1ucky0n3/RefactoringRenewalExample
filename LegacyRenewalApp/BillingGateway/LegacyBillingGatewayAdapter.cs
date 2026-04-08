namespace LegacyRenewalApp.BillingGateway;

public class LegacyBillingGatewayAdapter : IBillingGateway
{
    public void SaveInvoice(RenewalInvoice invoice)
    {
        LegacyBillingGateway.SaveInvoice(invoice);
    }
    public void SendEmail(string email, string subject, string body)
    {
        if(!string.IsNullOrWhiteSpace(email))
            LegacyBillingGateway.SendEmail(email, subject, body);
    }
}