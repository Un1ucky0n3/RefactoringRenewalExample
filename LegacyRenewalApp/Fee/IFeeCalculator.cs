namespace LegacyRenewalApp.SupportFee;

public interface IFeeCalculator
{
    void Calculate(FeeContext context);
}