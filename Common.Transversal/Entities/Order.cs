namespace Common.Transversal.Entities
{
    public enum OrderStatus
    {
        Pending,
        Approved,
        InventoryChecked,
        PaymentProcessed,
        ShippingConfirmed,
        Completed,
        Rejected
    }

    public class Order
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    public class OrderEntity
    {
        public Guid Id { get; private set; }
        public string CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool IsConfirmed { get; private set; }

        public OrderEntity(string customerId, List<OrderItem> items)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Items = items;
            TotalAmount = items.Sum(item => item.Price * item.Quantity);
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }
    }

    public class OrderItem
    {
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(string productName, decimal price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
    }
}
