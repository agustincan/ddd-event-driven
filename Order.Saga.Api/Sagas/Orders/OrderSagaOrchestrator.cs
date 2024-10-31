using Common.Transversal.Entities;
using Order.Saga.Api.Services;

namespace Order.Saga.Api.Sagas.Orders
{
    public interface IOrderSagaOrchestrator
    {
        Task<OrderSaga> ProcessOrderAsync(string customerId, List<OrderItem> items);
    }

    public class OrderSagaOrchestrator : IOrderSagaOrchestrator
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IInventoryService _inventoryService;

        public OrderSagaOrchestrator(IOrderService orderService, IPaymentService paymentService, IInventoryService inventoryService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
            _inventoryService = inventoryService;
        }

        public async Task<OrderSaga> ProcessOrderAsync(string customerId, List<OrderItem> items)
        {
            var order = new OrderEntity(customerId, items);
            var saga = new OrderSaga(order.Id, order.Id, customerId, items);

            try
            {
                // Step 1: Process Payment
                await _paymentService.ProcessPaymentAsync(order.TotalAmount);
                saga.MarkPaymentProcessed();

                // Step 2: Reserve Inventory
                await _inventoryService.ReserveInventoryAsync(items);
                saga.MarkInventoryReserved();

                // Step 3: Confirm Order
                await _orderService.ConfirmOrderAsync(order);
                return saga; // Saga completed successfully
            }
            catch (Exception ex)
            {
                // Handle failure and initiate compensation
                await CompensateAsync(saga);
                throw; // Rethrow or handle as needed
            }
        }

        private async Task CompensateAsync(OrderSaga saga)
        {
            if (saga.IsPaymentProcessed)
            {
                await _paymentService.RefundPaymentAsync(saga.OrderId);
            }

            if (saga.IsInventoryReserved)
            {
                await _inventoryService.ReleaseInventoryAsync(saga.Items);
            }
        }
    }
}
