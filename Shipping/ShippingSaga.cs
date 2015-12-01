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
    class ShippingSaga : Saga<ShippingData>, IAmStartedByMessages<OrderBilled>, IAmStartedByMessages<OrderAccepted>, IHandleMessages<CarrierAcceptedShipment>, IHandleSagaNotFound
    {
        public void Handle(OrderAccepted message)
        {
            this.Data.OrderId = message.OrderId;
            this.Data.OrderAccepted = true;
            HandleOrder();
        }

        public void Handle(OrderBilled message)
        {
            this.Data.OrderId = message.OrderId;
            this.Data.OrderBilled = true;
            HandleOrder();
        }

        void HandleOrder()
        {
            if(Data.OrderBilled && Data.OrderAccepted)
            {
                Bus.Publish<OrderReadyForShipment>(x => {
                    x.OrderId = Data.OrderId;
                });
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingData> mapper)
        {
            mapper.ConfigureMapping<OrderAccepted>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<OrderBilled>(m => m.OrderId).ToSaga(s => s.OrderId);
            mapper.ConfigureMapping<CarrierAcceptedShipment>(m => m.OrderId).ToSaga(s => s.OrderId);
        }

        public void Handle(CarrierAcceptedShipment message)
        {
            Console.WriteLine("Shipment Succeeded");
        }

        public void Handle(object message)
        {
        }
    }
}
