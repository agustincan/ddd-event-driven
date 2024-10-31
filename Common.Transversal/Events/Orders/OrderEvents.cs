namespace Common.Transversal.Events.Orders
{
    public record OrderCreated(Guid OrderId);
    public record InventoryChecked(Guid OrderId, bool IsAvailable);
    public record PaymentProcessed(Guid OrderId, bool IsSuccessful);
    public record ShippingConfirmed(Guid OrderId, bool IsShipped);
    public record OrderCompleted(Guid OrderId);
    public record OrderFailed(Guid OrderId, string Reason);

}
