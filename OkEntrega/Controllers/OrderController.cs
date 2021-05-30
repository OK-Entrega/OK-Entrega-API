using Domains.Commands.Requests.OrderRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController([FromServices] IMediator mediator) : base(mediator) { }

        [HttpPost("create-orders-with-xml")]
        [Authorize]
        public async Task<ObjectResult> CreateOrdersWithXml([FromForm] CreateOrdersRequest request)
        {
            return await Result(request);
        }

        [HttpPost("create-occurrence")]
        [Authorize]
        public async Task<ObjectResult> CreateOccurrence([FromBody] CreateOccurrenceRequest request)
        {
            return await Result(request);
        }

        [HttpDelete("delete-orders")]
        [Authorize]
        public async Task<ObjectResult> DeleteOrders([FromBody] DeleteOrdersRequest request)
        {
            return await Result(request);
        }
    }
}
