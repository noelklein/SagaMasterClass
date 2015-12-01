
using NServiceBus.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class OrderData : ContainSagaData
    {
        public virtual string CustomerId { get; set; }

        public virtual double OrderValue { get; set; }

        public virtual DateTime? OrderDate { get; set; }

        public virtual bool OrderPlaced { get; set; }

        [Unique]
        public virtual string OrderId { get; set; }
    }
}
