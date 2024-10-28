using Common.Transversal.Events;
using MassTransit;

namespace Order.Api.Consumers
{
    public class OfferAcceptedConsumer : IConsumer<OfferAcceptedEvent>
    {
        private readonly ILogger<OfferAcceptedConsumer> logger;

        public OfferAcceptedConsumer(ILogger<OfferAcceptedConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<OfferAcceptedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation($"Offer accepted {message.OrderId} time {message.CreatedAt}");
            
            return Task.CompletedTask;
        }
    }
}
