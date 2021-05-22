using Domains.Commands.Requests.DelivererRequests;
using Domains.Commands.Requests.ShipperRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("signup/shipper")]
        public async Task<ObjectResult> SignUpShipper([FromBody] CreateShipperRequest request)
        {
            return await Result(request);
        }

        [HttpPost("signup/deliverer")]
        public async Task<ObjectResult> SignUpDeliverer([FromBody] CreateDelivererRequest request)
        {
            return await Result(request);
        }

        [HttpPost("signin/deliverer")]
        public async Task<ObjectResult> SignInDeliverer([FromBody] LoginDelivererRequest request)
        {
            return await Result(request);
        }

        /*[HttpGet("list-profile")]
        public IQueryResult ListProfile([FromServices] ListProfileQueryHandler handler)
        {
            var query = new ListProfileQuery();
            return (GenericQueryResult)handler.Handle(query);
        }

        [HttpPut("change-user")]
        public ICommandResult Change(ChangeDelivererCommand command, [FromServices] ChangeDelivererHandler handler)
        {
            return (GenericCommandResult)handler.Handle(command);
        }
        [HttpDelete("delete-deliverer")]
        [Authorize]
        public ICommandResult DeletarConta(DeleteDelivererCommand command, [FromServices] DeleteDelivererHandler handler)
        {

            return (GenericCommandResult)handler.Handle(command);
        }*/
    }
}
