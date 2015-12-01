using NServiceBus;

namespace Sales.Contracts
{
    public interface OrderStarted : IEvent
    {
        string OrderId { get; set; }
    }
}