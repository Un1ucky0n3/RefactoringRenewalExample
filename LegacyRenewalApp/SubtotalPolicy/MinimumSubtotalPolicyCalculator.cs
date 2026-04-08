namespace LegacyRenewalApp.SubTotalPolicy;

public class MinimumSubtotalPolicyCalculator : ISubtotalPolicyCalculator
{
    private readonly decimal _minimum;

    public MinimumSubtotalPolicyCalculator(decimal minimum)
    {
        _minimum = minimum;
    }

    public (decimal subtotal, string note) Apply(decimal subtotal)
    {
        if (subtotal < _minimum)
            return (_minimum, "minimum discounted subtotal applied; ");

        return (subtotal, string.Empty);
    }
}