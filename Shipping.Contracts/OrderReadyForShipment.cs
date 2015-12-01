using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    public interface OrderReadyForShipment : IEvent
    {
        string OrderId { get; set; }
    }
}
