using System;
using LegacyRenewalApp.BillingGateway;
using LegacyRenewalApp.Email;
using LegacyRenewalApp.Invoice;
using LegacyRenewalApp.Notes;
using LegacyRenewalApp.SupportFee;
using LegacyRenewalApp.Tax;

namespace LegacyRenewalApp
{
    public class SubscriptionRenewalService
    {
        internal IBillingGateway BillingGateway { get; set; } = new LegacyBillingGatewayAdapter();
        public RenewalInvoice CreateRenewalInvoice(
            int customerId,
            string planCode,
            int seatCount,
            string paymentMethod,
            bool includePremiumSupport,
            bool useLoyaltyPoints)
        {
            //Preparing values
            // =======================
            RenewalInput input = RenewalInputPreparer.Prepare(customerId, planCode, seatCount, paymentMethod);
            string normalizedPlanCode = input.NormalizedPlanCode;
            string normalizedPaymentMethod = input.NormalizedPaymentMethod;
            var customer = input.Customer;
            var plan = input.Plan;
            // =======================
            
            //Discount
            // =======================
            DiscountContext discountContext = new DiscountContext(plan, customer, seatCount, useLoyaltyPoints);
            new DiscountCalculatorFactory(discountContext).Run();
            //========================
            
            //Applying Fees
            //========================
            FeeContext feeContext = new FeeContext(normalizedPlanCode, 0, normalizedPaymentMethod, discountContext.SubtotalAfterDiscount, includePremiumSupport);
            new FeeCalculatorFactory(feeContext).Run();
            //========================
            
            //Tax
            //========================
            TaxContext taxContext = new TaxContext(discountContext.SubtotalAfterDiscount, feeContext.SupportFee, feeContext.PaymentFee, customer.Country);
            new TaxCalculatorFactory(taxContext).Run();
            //========================
            
            
            //Invoice Creation
            //========================
            RenewalInvoiceCreationData data = new RenewalInvoiceCreationData();
            data.Customer = customer;
            data.DiscountContext = discountContext;
            data.FeeContext = feeContext;
            data.TaxContext = taxContext;
            IInvoiceFactory invoiceFactory = new RenewalInvoiceFactory(data, new InvoiceNotesBuilder());
            RenewalInvoice invoice = (RenewalInvoice)invoiceFactory.CreateInvoice();
            //========================

            BillingGateway.SaveInvoice(invoice);
            
            IEmailCreator emailCreator = new InvoiceEmailCreator(invoice,  customer, normalizedPlanCode);
            (string subject, string body) = emailCreator.CreateEmail();
            BillingGateway.SendEmail(customer.Email, subject, body);

            return invoice;
        }
    }
}
