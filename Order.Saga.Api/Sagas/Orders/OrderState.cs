using MassTransit;

namespace Order.Saga.Api.Sagas.Orders
{
    internal abstract class BaseOrderState: SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
    }
    internal class OrderState: BaseOrderState
    {
        /// <summary>
        /// The saga state machine instance current state
        /// </summary>
        public string CurrentState { get; set; } = default!;
    }

    internal class OrderStateInt : BaseOrderState
    {

        /// <summary>
        /// The saga state machine instance current state
        /// </summary>
        public int CurrentStateInt { get; set; }
    }
}