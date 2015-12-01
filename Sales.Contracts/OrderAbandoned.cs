using NServiceBus;

namespace Sales
{
    public interface OrderAbandoned : IEvent
    {
        string OrderId { get; set; }
    }
}