using Punica.Bp.CQRS.Messages;

namespace Sample.Application.Orders.Commands
{
    public class CreateOrderRequest : ICommand<Guid>
    {
        public string BuyerEmail { get; set; }
        public string BuyerName { get; set; }
        public List<Item> Items { get; set; }
    }
}
