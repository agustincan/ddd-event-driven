using MassTransit;

namespace Order.Saga.Api.Sagas.Orders
{
    internal class OrderSaga : ISaga
    {
        public Guid CorrelationId { get; set; }
    }
}