using MassTransit;
using Offer.Api.Consumers;
using Offer.Api.Consumers.Definitions;

namespace Offer.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            
            return services;
        }

        public static IServiceCollection AddMassTransitService(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OfferCreatedConsumer, OfferCreatedConsumerDefinition>();
                x.AddConsumer<OfferUpdatedConsumer>();
                x.AddConsumer<OfferPatchedConsumer>();
                x.AddConsumer<OfferDeletedConsumer>();
                
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin1234");
                    });
                    cfg.ConfigureEndpoints(context);
                    //cfg.ReceiveEndpoint("offer-created-queue", e =>
                    //cfg.ReceiveEndpoint(e =>
                    //{
                    //    e.ConfigureConsumer<OfferCreatedConsumer>(context);
                    //});
                    //cfg.ReceiveEndpoint("offer-updated-queue", e =>
                    //cfg.ReceiveEndpoint(e =>
                    //{
                    //    e.ConfigureConsumer<OfferUpdatedConsumer>(context);
                    //});
                });

            });
            return services;
        }
    }
}
