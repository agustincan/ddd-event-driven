using Common.Transversal.RabbitMqConnector;
using MassTransit;
using Order.Saga.Api.Consumers;
using Order.Saga.Api.Sagas.Orders;
using Order.Saga.Api.Services;
using System.Reflection;

namespace Order.Saga.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            var rabbitMqConfig = new RabbitMqConfig
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin1234",
                Exchange = "OrderSagaExchange"
            };

            services.AddSingleton(rabbitMqConfig);
            services.AddSingleton<IEventBus, RabbitMqEventBus>();

            //services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IInventoryService2, InventoryService2>();

            //services.AddScoped<IOrderSagaOrchestrator, OrderSagaOrchestrator>();
            services.AddScoped<IOrderSagaOrchestratorGpt, OrderSagaOrchestratorGpt>();
            
            return services;
        }
        public static IServiceCollection AddMassTransitService(this IServiceCollection services)
        {
            services.AddMassTransit(r =>
            {
                r.AddConsumer<OrderSagaConsumer>();
                r.SetKebabCaseEndpointNameFormatter();
                r.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin1234");
                    });
                    cfg.ConfigureEndpoints(context);
                });

                // add a state machine saga, with the in-memory repository
                r.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .InMemoryRepository();

                // add a consumer saga with the in-memory repository
                //r.AddSaga<OrderSaga>().InMemoryRepository();

                // add a saga by type, without a repository. The repository should be registered
                // in the container elsewhere
                //r.AddSaga(typeof(OrderSaga));

                // add a state machine saga by type, including a saga definition for that saga
                //r.AddSagaStateMachine(typeof(OrderState), typeof(OrderStateDefinition));

                // add all saga state machines by type
                r.AddSagaStateMachines(Assembly.GetExecutingAssembly());

                // add all sagas in the specified assembly
                r.AddSagas(Assembly.GetExecutingAssembly());

                // add sagas from the namespace containing the type
                r.AddSagasFromNamespaceContaining<OrderSaga>();
                r.AddSagasFromNamespaceContaining(typeof(OrderSaga));
            });

            return services;
        }
    }
}
