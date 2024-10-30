using MassTransit;
using Order.Api.Consumers;

namespace Order.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OfferAcceptedConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin1234");
                    });
                    cfg.ConfigureEndpoints(context);
                    
                    //cfg.ReceiveEndpoint("offer-accepted-queue", e =>
                    //{
                    //    e.ConfigureConsumer<OfferAcceptedConsumer>(context);
                    //});
                });
              
            });
            return services;
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            //services.AddApiVersioning(o =>
            //{
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.ReportApiVersions = true;
            //    o.ApiVersionReader = new UrlSegmentApiVersionReader();
            //}).AddApiExplorer(options =>
            //{
            //    options.GroupNameFormat = "'v'VVV";
            //    options.SubstituteApiVersionInUrl = true;
            //});

            return services;
        }

    }
}
