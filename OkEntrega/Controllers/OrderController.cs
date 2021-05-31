using Domains.Commands.Requests.OrderRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        public async Task<ObjectResult> CreateOccurrence([FromForm] CreateOccurrenceRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPost("finish-order")]
        [Authorize]
        public async Task<ObjectResult> FinishOrder([FromBody] FinishOrderRequest request)
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
