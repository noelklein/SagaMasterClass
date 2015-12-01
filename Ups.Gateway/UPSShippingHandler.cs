using NServiceBus;
using Shipping;
using Shipping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FedEx.Gateway
{
    class UPSShippingHandler : IHandleMessages<ShipToUPS>
    {
        public void Handle(ShipToUPS message)
        {
            Console.WriteLine("Shipping with UPS: " + message.OrderId);
        }
    }
}
