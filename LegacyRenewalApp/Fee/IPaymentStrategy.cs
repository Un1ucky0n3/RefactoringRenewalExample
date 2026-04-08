namespace LegacyRenewalApp.SupportFee;

public interface IPaymentStrategy
{
    public bool CanApply(FeeContext context);
    public decimal CalculateFee(decimal baseAmount);
    public string AddNote();
}