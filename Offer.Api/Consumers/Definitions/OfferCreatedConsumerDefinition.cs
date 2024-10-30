using MassTransit;
using MassTransit.RabbitMqTransport.Middleware;

namespace Offer.Api.Consumers.Definitions
{
    internal class OfferCreatedConsumerDefinition: ConsumerDefinition<OfferCreatedConsumer>
    {
        public OfferCreatedConsumerDefinition()
        {
            //EndpointName = "offer-service";
            ConcurrentMessageLimit = 10;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<OfferCreatedConsumer> consumerConfigurator, IRegistrationContext context)
        {
            // configure message retry with millisecond intervals
            endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

            // use the outbox to prevent duplicate events from being published
            endpointConfigurator.UseInMemoryOutbox(context);
            endpointConfigurator.PrefetchCount = 49;
        }
    }
}
