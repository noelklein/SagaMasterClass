using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Contracts
{
    public interface OrderAccepted : IEvent
    {
        string OrderId { get; set; }
    }
}
