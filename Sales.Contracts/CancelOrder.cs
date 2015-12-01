namespace Sales.Contracts
{
    public class CancelOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}