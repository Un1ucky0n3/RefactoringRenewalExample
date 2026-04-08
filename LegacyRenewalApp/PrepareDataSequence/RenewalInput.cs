namespace LegacyRenewalApp;

public class RenewalInput
{
    public Customer Customer { get; set; }
    public SubscriptionPlan Plan { get; set; }
    public string NormalizedPlanCode { get; set; }
    public string NormalizedPaymentMethod { get; set; }
    public int SeatCount { get; set; }
}