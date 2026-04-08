using System;
using LegacyRenewalApp.RepositoryHandler;

namespace LegacyRenewalApp;

public class RenewalInputPreparer
{
    public static RenewalInput Prepare(
        int customerId,
        string planCode,
        int seatCount,
        string paymentMethod)
    {
        var request = new RenewalRequest
        {
            CustomerId = customerId,
            PlanCode = planCode,
            SeatCount = seatCount,
            PaymentMethod = paymentMethod
        };
        
        RenewalValidator.Validate(request);
        
        var normalizedPlanCode = InputNormalizer.Normalize(request.PlanCode);
        var normalizedPaymentMethod = InputNormalizer.Normalize(request.PaymentMethod);
        
        var repositoryManager = new RepositoryManager();

        var customer = (Customer)repositoryManager
            .GetRepository("customers")
            .GetByValue(request.CustomerId);

        if (!customer.IsActive)
        {
            throw new InvalidOperationException("Inactive customers cannot renew subscriptions");
        }
        
        var plan = (SubscriptionPlan)repositoryManager
            .GetRepository("plans")
            .GetByValue(request.PlanCode);
        
        return new RenewalInput
        {
            Customer = customer,
            Plan = plan,
            NormalizedPlanCode = normalizedPlanCode,
            NormalizedPaymentMethod = normalizedPaymentMethod,
            SeatCount = request.SeatCount
        };
    }
}