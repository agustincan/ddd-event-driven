using Common.Transversal.Events.Offers;
using MassTransit;

namespace Offer.Api.Consumers
{
    public class OfferUpdatedConsumer : IConsumer<OfferUpdatedEvent>
    {
        private readonly ILogger<OfferUpdatedConsumer> logger;

        public OfferUpdatedConsumer(ILogger<OfferUpdatedConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<OfferUpdatedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation($"Offer Updated {message.OrderId} time {message.CreatedAt}");
            
            return Task.CompletedTask;
        }
    }
}
