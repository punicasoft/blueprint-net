
using Punica.Bp.Application.Query;
using Punica.Bp.CQRS.Handlers;
using Punica.Bp.Ddd.Domain.Repository;
using Sample.Domain.Aggregates.Orders;

namespace Sample.Application.Orders.Queries
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, List<object>>
    {
        private readonly IOrderQueries _queries; //TEMPORARY USAGE


        public GetOrderQueryHandler(IOrderQueries queries)
        {
            _queries = queries;
        }

        public async Task<List<dynamic>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            await Task.Delay(1, cancellationToken);
            return _queries.GetPersons(new QueryOptions<Order>(query.Select, query.Filter, query.OrderBy, query.Skip, query.Take));
        }
    }
}
