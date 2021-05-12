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
    }
}
