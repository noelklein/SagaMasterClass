using NServiceBus.Saga;

namespace Billing
{
    public class BillingData : ContainSagaData
    {
        public string OrderId { get; set; }
    }
}