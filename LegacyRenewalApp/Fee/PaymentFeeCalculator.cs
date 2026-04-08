using System;
using System.Collections.Generic;

namespace LegacyRenewalApp.SupportFee;

public class PaymentFeeCalculator : IFeeCalculator
{
    public void Calculate(FeeContext context)
    {
        IPaymentStrategy strategy = GetStrategy(context);
        var fee = strategy.CalculateFee(context.Amount);
        var note = strategy.AddNote();
        context.PaymentFee += fee;
        context.Notes += note;
    }

    private List<IPaymentStrategy> CreateListOfStrategies()
    {
        return new List<IPaymentStrategy>()
        {
            new CardPayment(),
            new BankTransferPayment(),
            new PaypalPayment(),
            new InvoicePayment()
        };
    }

    private IPaymentStrategy GetStrategy(FeeContext context)
    {
        foreach (var strategy in CreateListOfStrategies())
        {
            if (strategy.CanApply(context)) return strategy;
        }
        throw new ArgumentException("Unsupported payment method: " + context.Method);
    }
}

class CardPayment : IPaymentStrategy
{
    public bool CanApply(FeeContext context) => context.Method == "CARD";

    public decimal CalculateFee(decimal baseAmount) => baseAmount * 0.02m;

    public string AddNote() => "card payment fee; ";
}

class BankTransferPayment : IPaymentStrategy
{
    public bool CanApply(FeeContext context) => context.Method == "BANK_TRANSFER";

    public decimal CalculateFee(decimal baseAmount) => baseAmount * 0.01m;

    public string AddNote() => "bank transfer fee; ";
}

class PaypalPayment : IPaymentStrategy
{
    public bool CanApply(FeeContext context) => context.Method == "PAYPAL";

    public decimal CalculateFee(decimal baseAmount) => baseAmount * 0.035m;

    public string AddNote() => "paypal fee; ";
}

class InvoicePayment : IPaymentStrategy
{
    public bool CanApply(FeeContext context) => context.Method == "INVOICE";

    public decimal CalculateFee(decimal baseAmount) => 0m;

    public string AddNote() => "invoice payment; ";
}