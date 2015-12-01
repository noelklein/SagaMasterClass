using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    public class FedExOrderShipped : IMessage
    {
        public string OrderId { get; set; }

        public FedExOrderShipped(string OrderId)
        {
            this.OrderId = OrderId;
        }
    }
}
