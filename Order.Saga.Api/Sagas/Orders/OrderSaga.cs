using Common.Transversal.Entities;
using MassTransit;

namespace Order.Saga.Api.Sagas.Orders
{
    //internal class OrderSaga : ISaga
    //{
    //    public Guid CorrelationId { get; set; }
    //}

    public class OrderSaga//: ISaga
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; private set; }
        public string CustomerId { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public bool IsPaymentProcessed { get; private set; }
        public bool IsInventoryReserved { get; private set; }

        //public OrderSaga(Guid CorrelationId)
        //{
        //    this.CorrelationId = CorrelationId;
        //}
        public OrderSaga(Guid CorrelationId, Guid orderId, string customerId, List<OrderItem> items)
        {
            this.CorrelationId = CorrelationId;
            OrderId = orderId;
            CustomerId = customerId;
            Items = items;
            IsPaymentProcessed = false;
            IsInventoryReserved = false;
        }

        public void MarkPaymentProcessed() => IsPaymentProcessed = true;
        public void MarkInventoryReserved() => IsInventoryReserved = true;
    }
}