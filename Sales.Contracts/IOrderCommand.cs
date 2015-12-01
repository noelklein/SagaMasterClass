namespace Sales.Contracts
{
    using NServiceBus;

    public interface IOrderCommand : ICommand
    {
        string OrderId { get; set; }
    }
}