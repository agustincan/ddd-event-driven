﻿using MassTransit;

namespace Order.Saga.Api.Sagas.Orders
{
    internal abstract class BaseOrderStateMachine<TState> : MassTransitStateMachine<TState>
        where TState : BaseOrderState, new()
    {
        protected BaseOrderStateMachine()
        {
        }

        public State Submitted { get; private set; } = null!;
        public State Accepted { get; private set; } = null!;
    }

    internal class OrderStateMachine: BaseOrderStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);
            SetCompletedWhenFinalized();
        }
    }

    internal class OrderStateIntMachine : MassTransitStateMachine<OrderStateInt>
    {
        public State Submitted { get; private set; } = null!;
        public State Accepted { get; private set; } = null!;

        public OrderStateIntMachine()
        {
            InstanceState(x => x.CurrentStateInt, Submitted, Accepted);
            SetCompletedWhenFinalized();
        }
    }
}