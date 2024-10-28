namespace Common.Transversal.Events
{
    public class OfferDeletedEvent
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}