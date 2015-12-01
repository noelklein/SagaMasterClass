using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    class ShippingHandler : IHandleMessages<OrderReadyForShipment>
    {
        public void Handle(OrderReadyForShipment message)
        {
            Console.WriteLine("Shipping Order: " + message.OrderId);
        }
    }
}
