using Common.Transversal.Entities;

namespace Common.Transversal.Commands.Orders
{
    public class OrderCreateCommand: BaseCommand
    {
        public string CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
