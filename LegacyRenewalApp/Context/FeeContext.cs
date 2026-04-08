using System;
using LegacyRenewalApp.Context;

namespace LegacyRenewalApp.SupportFee;

public class FeeContext : IContext
{
    public string PlanCode { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; }
    public decimal SubTotalAfterDiscount { get; set; }
    public decimal SupportFee { get; set; }
    public string Notes { get; set; }
    public decimal PaymentFee { get; set; }
    public bool IncludePremiumSupport { get; set; }

    public FeeContext(string planCode, decimal amount, string method, decimal subTotalAfterDiscount, bool includePremiumSupport)
    {
        PlanCode = planCode;
        Amount = amount;
        Method = method;
        SubTotalAfterDiscount = subTotalAfterDiscount;
        IncludePremiumSupport = includePremiumSupport;
        SupportFee = 0m;
        Notes = string.Empty;
        PaymentFee = 0m;
    }
    
    public string GetNotes()
    {
        return Notes;
    }
}