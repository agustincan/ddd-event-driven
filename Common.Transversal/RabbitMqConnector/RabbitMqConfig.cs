namespace Common.Transversal.RabbitMqConnector
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; } = "localhost";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Exchange { get; set; } = "OrderSagaExchange";
    }
}