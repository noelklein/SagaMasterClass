using Billing.Contracts;
using NServiceBus;
using Sales.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing
{
    class OrderPlacedHandler : IHandleMessages<OrderAccepted>
    {
        public IBus Bus { get; set; }
        public void Handle(OrderAccepted message)
        {
            Bus.Publish<OrderBilled>(x=>
            {
                x.OrderId = message.OrderId;
            });
            Console.WriteLine("Order Billed: " + message.OrderId);
        }
    }
}
