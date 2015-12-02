using NServiceBus;
using NServiceBus.Saga;
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
    class FedExShippingGateway : IHandleMessages<ShipToFedEx>
    {
        public IBus Bus { get; set; }

        public void Handle(ShipToFedEx message)
        {
            try
            {
                var request = WebRequest.Create("http://localhost:8888/fedex/shipit");
                var response = request.GetResponse();
                Console.WriteLine("sent shipping request to fedex");

                Bus.Reply(new FedExOrderShipped(message.OrderId));
            }
            catch (Exception)
            {
                Console.WriteLine("FedEx service timed out");
            }
        }
    }
}
