using System;
using LegacyRenewalApp.Notes;
using LegacyRenewalApp.SupportFee;
using LegacyRenewalApp.Tax;

namespace LegacyRenewalApp.Invoice;

public class RenewalInvoiceFactory : IInvoiceFactory
{
    Customer Customer;
    DiscountContext DiscountContext;
    FeeContext FeeContext;
    TaxContext TaxContext;
    INotesBuilder NotesBuilder;
    
    public RenewalInvoiceFactory(RenewalInvoiceCreationData data, INotesBuilder notesBuilder)
    {
        Customer = data.Customer;
        DiscountContext = data.DiscountContext;
        FeeContext = data.FeeContext;
        TaxContext = data.TaxContext;
        NotesBuilder = notesBuilder;
    }
    
    public IInvoice CreateInvoice()
    {
        string notes = NotesBuilder.Build(DiscountContext, FeeContext, TaxContext);
        
        var invoice = new RenewalInvoice
        {
            InvoiceNumber = $"INV-{DateTime.UtcNow:yyyyMMdd}-{Customer.Id}-{FeeContext.PlanCode}",
            CustomerName = Customer.FullName,
            PlanCode = FeeContext.PlanCode,
            PaymentMethod = FeeContext.Method,
            SeatCount = DiscountContext.SeatCount,
            BaseAmount = Math.Round(DiscountContext.baseAmount, 2, MidpointRounding.AwayFromZero),
            DiscountAmount = Math.Round(DiscountContext.DiscountAmount, 2, MidpointRounding.AwayFromZero),
            SupportFee = Math.Round(FeeContext.SupportFee, 2, MidpointRounding.AwayFromZero),
            PaymentFee = Math.Round(FeeContext.PaymentFee, 2, MidpointRounding.AwayFromZero),
            TaxAmount = Math.Round(TaxContext.TaxAmount, 2, MidpointRounding.AwayFromZero),
            FinalAmount = Math.Round(TaxContext.FinalAmount, 2, MidpointRounding.AwayFromZero),
            Notes = notes.Trim(),
            GeneratedAt = DateTime.UtcNow
        };
        return invoice;
    }
}