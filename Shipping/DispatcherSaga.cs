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
    class DispatcherSaga : Saga<DispatcherData>, IAmStartedByMessages<OrderReadyForShipment>, IHandleMessages<FedExOrderShipped>, IHandleTimeouts<FedExTimeoutResponse>
    {
        public void Handle(FedExOrderShipped message)
        {
            Console.WriteLine("FedEx accepted shipment");

            ReplyToOriginator(new CarrierAcceptedShipment());
            MarkAsComplete();
        }

        public void Handle(OrderReadyForShipment message)
        {
            Console.WriteLine("Sending order to shipper");
            Data.OrderId = message.OrderId;

            Bus.Publish<ShipToFedEx>(x =>
            {
                x.OrderId = Data.OrderId;
            });

            RequestTimeout<FedExTimeoutResponse>(DateTime.Now.AddSeconds(20));
        }

        public void Timeout(FedExTimeoutResponse timeoutResponse)
        {
            Console.WriteLine("FedEx timed out, Attempting to send with UPS...");
            Bus.Publish<ShipToUPS>(x =>
            {
                x.OrderId = Data.OrderId;
            });

            ReplyToOriginator(new CarrierAcceptedShipment());
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<DispatcherData> mapper)
        {
            mapper.ConfigureMapping<OrderReadyForShipment>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<FedExTimeoutResponse>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<FedExOrderShipped>(m => m.OrderId).ToSaga(s => s.OrderId);
        }
    }
}
