namespace Sales.Contracts
{
    using System;

    public class PlaceOrder : IOrderCommand
    {
        public string CustomerId { get; set; }
        public double OrderValue { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderId { get; set; }
    }
}