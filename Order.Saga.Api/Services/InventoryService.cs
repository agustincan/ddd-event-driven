using Common.Transversal.Events.Orders;
using MassTransit;

namespace Order.Saga.Api.Services
{
    public interface IInventoryService2
    {
        Task<InventoryChecked> CheckInventory(Guid orderId);
        Task<ShippingConfirmed> ConfirmShipping(Guid orderId);
        Task<PaymentProcessed> ProcessPayment(Guid orderId);
    }

    public class InventoryService2 : IInventoryService2
    {
        //private readonly IEventBus _eventBus;
        private readonly IPublishEndpoint publisher;

        public InventoryService2(IPublishEndpoint publisher)
        {
            //_eventBus = eventBus;
            this.publisher = publisher;
        }

        public async Task<InventoryChecked> CheckInventory(Guid orderId)
        {
            // Check inventory logic
            bool isAvailable = true; // Example check
            //_eventBus.Publish(new InventoryChecked(orderId, isAvailable));
            var evt = new InventoryChecked(orderId, isAvailable);
            await publisher.Publish(evt);
            return evt;
        }
        public async Task<PaymentProcessed> ProcessPayment(Guid orderId)
        {
            // Check inventory logic
            bool isAvailable = true; // Example check
            //_eventBus.Publish(new InventoryChecked(orderId, isAvailable));
            var evt = new PaymentProcessed(orderId, isAvailable);
            await publisher.Publish(evt);
            return evt;
        }

        public async Task<ShippingConfirmed> ConfirmShipping(Guid orderId)
        {
            // Check inventory logic
            bool isAvailable = true; // Example check
            //_eventBus.Publish(new InventoryChecked(orderId, isAvailable));
            var evt = new ShippingConfirmed(orderId, isAvailable);
            await publisher.Publish(evt);
            return evt;
        }
    }

}
