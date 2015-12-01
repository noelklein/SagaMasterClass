using NServiceBus;

namespace Sales.Contracts
{
    public interface OrderCancelled : IEvent
    {
        string OrderId { get; set; }
    }
}