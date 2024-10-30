using MassTransit;
using Order.Saga.Api.Sagas.Orders;

namespace Order.Saga.Api.Consumers
{
    public class OrderSagaConsumer : IConsumer<OrderStateIntMachine>
    {
        private readonly ILogger<OrderSagaConsumer> logger;

        public OrderSagaConsumer(ILogger<OrderSagaConsumer> logger)
        {
            this.logger = logger;
        }
        Task IConsumer<OrderStateIntMachine>.Consume(ConsumeContext<OrderStateIntMachine> context)
        {
            logger.LogInformation($"Consumer saga {context.MessageId}");
            return Task.CompletedTask;
        }
    }
}
