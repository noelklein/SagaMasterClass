using NServiceBus;
using NServiceBus.Saga;
using Sales.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    class SalesSaga : Saga<OrderData>, IAmStartedByMessages<StartOrder>, IHandleMessages<PlaceOrder>, IHandleMessages<CancelOrder>, IHandleTimeouts<OrderExpiredTimeout>
    {
        public void Handle(PlaceOrder message)
        {
            this.Data.OrderDate = message.OrderDate;
            this.Data.OrderValue = message.OrderValue;
            this.Data.OrderPlaced = true;

            this.Bus.Publish<OrderAccepted>(x =>
            {
                x.OrderId = this.Data.OrderId;
            });

            Console.WriteLine("Order Placed: " + message.OrderId);
        }

        public void Handle(CancelOrder message)
        {
            this.Bus.Publish<OrderCancelled>(x =>
            {
                x.OrderId = this.Data.OrderId;
            });
            this.MarkAsComplete();
            Console.WriteLine("Order Cancelled: " + message.OrderId);
        }

        public void Handle(StartOrder message)
        {
            this.Data.OrderId = message.OrderId;
            this.RequestTimeout<OrderExpiredTimeout>(DateTime.Now.AddSeconds(20));
            this.Bus.Publish<OrderStarted>(x =>
            {
                x.OrderId = this.Data.OrderId;
            });

            Console.WriteLine("Order Started: " + message.OrderId);
        }

        public void Timeout(OrderExpiredTimeout state)
        {
            if(this.Data.OrderPlaced)
            {
                return;
            }

            this.Bus.Publish<OrderAbandoned>(x =>
            {
                x.OrderId = this.Data.OrderId;
            });

            Console.WriteLine("Order Expired: " + this.Data.OrderId);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderData> mapper)
        {
            mapper.ConfigureMapping<StartOrder>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<CancelOrder>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<PlaceOrder>(m => m.OrderId).ToSaga(s => s.OrderId);
        }
    }
}
