﻿using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Contracts
{
    public class FedExTimeoutResponse : IEvent
    {
        public string OrderId { get; set; }
    }
}
