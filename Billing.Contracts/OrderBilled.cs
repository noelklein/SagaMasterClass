using NServiceBus;

namespace Billing.Contracts
{
    public interface OrderBilled : IEvent
    {
        string OrderId { get; set; }
        decimal OrderAmount { get; set; }
    }
}