namespace Order.Api.Events
{
    public class OrderCreatedEvent
    {
        Guid OrderId { get; }
        DateTime CreatedAt { get; }
        string ProductName { get; }
        int Quantity { get; }
    }

}
