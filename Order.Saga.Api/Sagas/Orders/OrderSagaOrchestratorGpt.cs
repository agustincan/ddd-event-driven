using Common.Transversal.Events.Orders;
using Common.Transversal.RabbitMqConnector;
using MassTransit;
using Order.Saga.Api.Services;

namespace Order.Saga.Api.Sagas.Orders
{
    public interface IOrderSagaOrchestratorGpt
    {
        Task Handle(InventoryChecked inventoryChecked);
        Task Handle(OrderCreated orderCreated);
        Task Handle(PaymentProcessed paymentProcessed);
        Task Handle(ShippingConfirmed shippingConfirmed);
    }

    public class OrderSagaOrchestratorGpt : IOrderSagaOrchestratorGpt
    {
        private readonly IEventBus _eventBus;
        //private readonly IPublishEndpoint _eventBus;
        private readonly IInventoryService2 invService;

        public OrderSagaOrchestratorGpt(IEventBus publisher, IInventoryService2 invService)
        {
            //_eventBus = eventBus;
            this._eventBus = publisher;
            this.invService = invService;

            // Subscribe to relevant events
            _eventBus.SubscribeAsync<OrderCreated>(Handle);
            _eventBus.SubscribeAsync<InventoryChecked>(Handle);
            _eventBus.SubscribeAsync<PaymentProcessed>(Handle);
            _eventBus.SubscribeAsync<ShippingConfirmed>(Handle);
            _eventBus.SubscribeAsync<OrderFailed>(Handle);
        }

        public async Task Handle(OrderCreated orderCreated)
        {
            // Step 1: Check Inventory
            var inventoryChecked = await invService.CheckInventory(orderCreated.OrderId);
            if (inventoryChecked.IsAvailable)
            {
               await _eventBus.PublishAsync(new InventoryChecked(orderCreated.OrderId, true));
            }
            else
            {
                await _eventBus.PublishAsync(new OrderFailed(orderCreated.OrderId, "Inventory unavailable"));
            }
        }

        public async Task Handle(InventoryChecked inventoryChecked)
        {
            if (inventoryChecked.IsAvailable)
            {
                // Step 2: Process Payment
                var paymentProcessed = await invService.ProcessPayment(inventoryChecked.OrderId);
                await _eventBus.PublishAsync(new PaymentProcessed(inventoryChecked.OrderId, paymentProcessed.IsSuccessful));
            }
            else
            {
                await _eventBus.PublishAsync(new OrderFailed(inventoryChecked.OrderId, "Inventory check failed"));
            }
        }

        public async Task Handle(PaymentProcessed paymentProcessed)
        {
            if (paymentProcessed.IsSuccessful)
            {
                // Step 3: Confirm Shipping
                var shippingConfirmed = await invService.ConfirmShipping(paymentProcessed.OrderId);
                await _eventBus.PublishAsync(new ShippingConfirmed(paymentProcessed.OrderId, shippingConfirmed.IsShipped));
            }
            else
            {
                await _eventBus.PublishAsync(new OrderFailed(paymentProcessed.OrderId, "Payment processing failed"));
            }
        }

        public async Task Handle(ShippingConfirmed shippingConfirmed)
        {
            if (shippingConfirmed.IsShipped)
            {
                // Final Step: Complete Order
                await _eventBus.PublishAsync(new OrderCompleted(shippingConfirmed.OrderId));
            }
            else
            {
                await _eventBus.PublishAsync(new OrderFailed(shippingConfirmed.OrderId, "Shipping confirmation failed"));
            }
        }

        public async Task Handle(OrderFailed orderFailed)
        {
            await _eventBus.
            await Task.CompletedTask;
        }
    }

}
