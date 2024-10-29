using Common.Transversal.Events.Offers;
using MassTransit;

namespace Offer.Api.Consumers
{
    public class OfferPatchedConsumer : IConsumer<OfferPatchedEvent>
    {
        private readonly ILogger<OfferPatchedConsumer> logger;

        public OfferPatchedConsumer(ILogger<OfferPatchedConsumer> logger)
        {
            this.logger = logger;
        }
        public Task Consume(ConsumeContext<OfferPatchedEvent> context)
        {
            var message = context.Message;
            logger.LogInformation($"Offer Patched {message.OrderId} time {message.CreatedAt}");
            
            return Task.CompletedTask;
        }
    }
}
