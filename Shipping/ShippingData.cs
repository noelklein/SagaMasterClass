using NServiceBus.Saga;

namespace Shipping
{
    public class ShippingData : ContainSagaData
    {
        public virtual string OrderId { get; set; }
        public virtual bool OrderBilled { get; set; }
        public virtual bool OrderAccepted { get; set; }
    }
}