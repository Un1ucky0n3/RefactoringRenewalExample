namespace LegacyRenewalApp.SubTotalPolicy;

public interface ISubtotalPolicyCalculator
{
    public (decimal subtotal, string note) Apply(decimal subtotal);
}