namespace Common.Transversal.Events.Offers
{
    public class OfferPatchedEvent: BaseEvent
    {
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}