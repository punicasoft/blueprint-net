using Microsoft.AspNetCore.Mvc;
using Punica.Bp.CQRS;
using Sample.Application.Orders.Commands;
using Sample.Application.Orders.Queries;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<ActionResult<Guid>> CreateOrder([FromHeader(Name = "TenantId")] Guid tenantId, CreateOrderRequest command)
        {
            var orderId = await _mediator.Send(command);

            return Ok(orderId);
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<string>> AddItemToOrder(Guid orderId, [FromHeader(Name = "TenantId")]Guid tenantId, AddItemToOrderCommand command)
        {
            command.OrderId = orderId;
            await _mediator.Send(command);

            return Ok("success");
        }

        [HttpPost("details")]
        public async Task<ActionResult<string>> GetOrder(GetOrderQuery query , [FromHeader(Name = "TenantId")] Guid tenantId)
        {
            var order = await _mediator.Send(query);

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetOrderQuery([FromQuery]QueryOptions options)
        {
            var order = await _mediator.Send(new GetOrderQuery()
            {
                Filter = options.Filter,
                OrderBy = options.OrderBy,
                Select = options.Select,
                Skip = options.Skip,
                Take = options.Take

            });

            return Ok(order);
        }

        public class QueryOptions
        {
            [BindProperty(Name = "$select", SupportsGet = true)]
            public string? Select { get; set; }

            [BindProperty(Name = "$filter", SupportsGet = true)]
            public string? Filter { get; set; }

            [BindProperty(Name = "$orderBy", SupportsGet = true)]
            public string? OrderBy { get; set; }

            [BindProperty(Name = "$skip", SupportsGet = true)]
            public int? Skip { get; set; }

            [BindProperty(Name = "$take", SupportsGet = true)]
            public int? Take { get; set; }
        }

        //public class QueryOptions
        //{
        //    public string? Select { get; private set; }
        //    public string? Filter { get; private set; }
        //    public string? OrderBy { get; private set; }
        //    public int? Skip { get; private set; }
        //    public int? Take { get; private set; }

        //    public QueryOptions(HttpContext context)
        //    {
        //        var query = context.Request.Query;
        //        Select = query["$select"];
        //        Filter = query["$filter"];
        //        OrderBy = query["$orderby"];
        //        Skip = query["$skip"].Count > 0 ? int.Parse(query["$skip"].First()) : (int?)null;
        //        Take = query["$take"].Count > 0 ? int.Parse(query["$take"].First()) : (int?)null;

        //    }
        //}

        //{
        //    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        //    "title": "One or more validation errors occurred.",
        //    "status": 400,
        //    "traceId": "00-ad8b73bf9e56403f31553f23d4cce054-2a63fa58c930afb0-00",
        //    "errors": {
        //        "Filter": [
        //        "The Filter field is required."
        //            ],
        //        "Select": [
        //        "The Select field is required."
        //            ],
        //        "OrderBy": [
        //        "The OrderBy field is required."
        //            ]
        //    }
        //}
    }
}
