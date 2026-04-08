using LegacyRenewalApp.Context;

namespace LegacyRenewalApp;

public class DiscountContext : IContext
{
    public string Segment { get; set; }
    public int YearsWithCompany { get; set; }
    public int SeatCount { get; set; }
    public decimal baseAmount { get; set; }
    public bool IsEducationEligible { get; set; }
    public int LoyaltyPoints { get; set; }
    public bool UseLoyaltyPoints { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal SubtotalAfterDiscount {get; set;}
    public string Notes { get; set; }

    public DiscountContext(SubscriptionPlan plan, Customer customer, int seatCount, bool useLoyaltyPoints)
    {
        baseAmount = (plan.MonthlyPricePerSeat * seatCount * 12m) + plan.SetupFee;
        IsEducationEligible = plan.IsEducationEligible;
        SeatCount = seatCount;
        YearsWithCompany = customer.YearsWithCompany;
        LoyaltyPoints = customer.LoyaltyPoints;
        Segment = InputNormalizer.Normalize(customer.Segment);
        UseLoyaltyPoints = useLoyaltyPoints;
        DiscountAmount = 0m;
        SubtotalAfterDiscount = 0m;
        Notes = string.Empty;
    }

    public string GetNotes()
    {
        return Notes;
    }
}