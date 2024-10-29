namespace Common.Transversal.Events.Offers
{
    public class OfferUpdatedEvent: BaseEvent
    {
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}