

namespace Common.Transversal.RabbitMqConnector
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
        Task PublishAsync<TEvent>(TEvent @event);
        void Subscribe<TEvent>(Action<TEvent> handler);
        Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler);
    }
}