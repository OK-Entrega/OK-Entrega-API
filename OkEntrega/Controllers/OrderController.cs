using Commom.Commands;
using Commom.Services.PDFServices.Interfaces;
using Domains.Commands.Requests.OrderRequests;
using Domains.Entities;
using Domains.Queries.Requests.OrderRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IPDFGenerator _pdfGenerator;

        public OrderController([FromServices] IMediator mediator, IPDFGenerator pdfGenerator) : base(mediator) 
        {
            _pdfGenerator = pdfGenerator;
        }

        [HttpGet("pending")]
        [Authorize]
        public async Task<ObjectResult> Pending([FromQuery] GetPendingOrdersRequest request)
        {
            return await Result(request);
        }

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
        public async Task<ObjectResult> FinishOrder([FromForm] FinishOrderRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPost("print-orders")]
        [Authorize]
        public async Task<ActionResult> PrintOrders([FromBody] PrintOrdersRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

                var pdf = _pdfGenerator.Generate((List<Order>) result.Data, "NFE").Result;

                return result.StatusCode switch
                {
                    200 => File(pdf, "application/pdf"),
                    400 => BadRequest(result),
                    500 => StatusCode(500, result),
                    _ => null
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(500, "Erro no servidor! Desculpe-nos.", ex.Message));
            }
        }

        [HttpDelete("delete-orders")]
        [Authorize]
        public async Task<ObjectResult> DeleteOrders([FromBody] DeleteOrdersRequest request)
        {
            return await Result(request);
        }
    }
}
