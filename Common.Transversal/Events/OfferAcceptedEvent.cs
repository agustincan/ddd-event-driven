﻿namespace Common.Transversal.Events
{
    public class OfferAcceptedEvent
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}