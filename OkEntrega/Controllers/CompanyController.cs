using Domains.Commands.Requests.CompanyRequests;
using Domains.Queries.Requests.CompanyRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanyController : BaseController
    {
        public CompanyController(IMediator mediator) : base(mediator) { }

        [HttpGet("get-mine")]
        [Authorize]
        public async Task<ObjectResult> GetMine()
        {
	        var request = new GetMyCompaniesRequest();
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpGet("get-dashboard-data")]
        [Authorize]
        public async Task<ObjectResult> GetDashboardData([FromQuery] GetDashboardDataRequest request)
        {
            return await Result(request);
        }

        [HttpGet("get-field-records")]
        [Authorize]
        public async Task<ObjectResult> GetFieldRecords([FromQuery] GetFieldRecordsRequest request)
        {
            return await Result(request);
        }

        [HttpGet("get-shippers")]
        [Authorize]
        public async Task<ObjectResult> GetShippers([FromQuery] GetShippersRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPost("create-company")]
        [Authorize]
        public async Task<ObjectResult> CreateCompany([FromForm] CreateCompanyRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPost("invite-shipper")]
        [Authorize]
        public async Task<ObjectResult> InviteShipper([FromBody] InviteShipperRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPut("join-a-company/accept-invite")]
        [Authorize]
        public async Task<ObjectResult> AcceptInvite([FromBody] AcceptInviteRequest request)
        {
            return await Result(request);
        }

        [HttpPut("join-a-company/with-code")]
        [Authorize]
        public async Task<ObjectResult> JoinWithCode([FromBody] JoinWithCodeRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPut("change-company")]
        [Authorize]
        public async Task<ObjectResult> ChangeCompany(ChangeCompanyRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpDelete("remove-shipper")]
        [Authorize]
        public async Task<ObjectResult> RemoveShipper([FromBody] RemoveShipperRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpDelete("delete-company")]
        [Authorize]
        public async Task<ObjectResult> RemoveCompany([FromBody] DeleteCompanyRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpDelete("leave-from-company")]
        [Authorize]
        public async Task<ObjectResult> LeaveFromCompany([FromBody] LeaveFromCompanyRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }
    }
}
