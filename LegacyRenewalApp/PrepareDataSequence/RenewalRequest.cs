namespace LegacyRenewalApp;

public class RenewalRequest
{
    public int CustomerId { get; set; }
    public string PlanCode { get; set; }
    public int SeatCount { get; set; }
    public string PaymentMethod { get; set; }
}