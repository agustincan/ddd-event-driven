using Common.Transversal.Events;
using MassTransit;

namespace Order.Api.Consumers
{
    public class OfferCreatedConsumer : IConsumer<OfferCreatedEvent>
    {
        private readonly ILogger<OfferCreatedConsumer> logger;

        public OfferCreatedConsumer(ILogger<OfferCreatedConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<OfferCreatedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation($"Offer accepted {message.OrderId} time {message.CreatedAt}");
            
            return Task.CompletedTask;
        }
    }
}
