namespace Sales.Contracts
{
    public class StartOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}