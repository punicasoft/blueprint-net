using Punica.Bp.CQRS.Messages;

namespace Sample.Application.Orders.Queries
{
    public class GetOrderQuery : IQuery<List<object>>
    {
        public string? Select { get; set; }
        public string? Filter { get; set; }
        public string? OrderBy { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
