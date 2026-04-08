using System.Collections.Generic;

namespace LegacyRenewalApp.SupportFee;

public class SupportFeeCalculator : IFeeCalculator
{
    private static readonly Dictionary<string, decimal> Fees = new()
    {
        ["START"] = 250m,
        ["PRO"] = 400m,
        ["ENTERPRISE"] = 700m
    };

    public void Calculate(FeeContext context)
    {
        if(context.IncludePremiumSupport) {
            context.SupportFee += Fees.TryGetValue(context.PlanCode, out var fee) ? fee : 0m;
            context.Notes += "premium support included; ";
        }
        context.Amount = context.SubTotalAfterDiscount + context.SupportFee;
    }
}