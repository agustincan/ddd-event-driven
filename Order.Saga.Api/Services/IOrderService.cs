using Common.Transversal.Entities;

namespace Order.Saga.Api.Services
{
    public interface IOrderService
    {
        Task ConfirmOrderAsync(OrderEntity order);
    }

    public interface IPaymentService
    {
        Task ProcessPaymentAsync(decimal amount);
        Task RefundPaymentAsync(Guid orderId);
    }

    public interface IInventoryService
    {
        Task ReserveInventoryAsync(List<OrderItem> items);
        Task ReleaseInventoryAsync(List<OrderItem> items);
    }
}
