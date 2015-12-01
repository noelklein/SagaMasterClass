using Billing.Contracts;
using NServiceBus;
using NServiceBus.Saga;
using Sales.Contracts;
using Shipping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping
{
    class DispatcherSaga : Saga<DispatcherData>, IAmStartedByMessages<OrderReadyForShipment>, IHandleMessages<FedExOrderShipped>, IHandleTimeouts<FedExTimeoutResponse>, IHandleSagaNotFound
    {
        public void Handle(FedExOrderShipped message)
        {
            var response = new CarrierAcceptedShipment();
            response.OrderId = message.OrderId;
            ReplyToOriginator(response);
            MarkAsComplete();
        }

        public void Handle(object message)
        {

        }

        public void Handle(OrderReadyForShipment message)
        {
            Data.OrderId = message.OrderId;

            Bus.Publish<ShipToFedEx>(x =>
            {
                x.OrderId = Data.OrderId;
            });

            RequestTimeout<FedExTimeoutResponse>(DateTime.Now.AddSeconds(20));
        }

        public void Timeout(FedExTimeoutResponse timeoutResponse)
        {
            ReplyToOriginator(new ShipmentFailed());

            //Bus.Send<ShipToUPS>(x =>
            //{
            //    x.OrderId = Data.OrderId;
            //});
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<DispatcherData> mapper)
        {
            mapper.ConfigureMapping<OrderReadyForShipment>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<FedExTimeoutResponse>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<FedExOrderShipped>(m => m.OrderId).ToSaga(s => s.OrderId);
        }
    }
}
