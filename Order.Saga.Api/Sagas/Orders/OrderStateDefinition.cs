using MassTransit;

namespace Order.Saga.Api.Sagas.Orders
{
    internal class OrderStateDefinition : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
    }
}