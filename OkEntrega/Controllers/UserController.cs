using Domains.Commands.Requests.UserRequests;
using Domains.Queries.Requests.UserRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpGet("get-profile")]
        public async Task<ObjectResult> SignUp()
        {
            var request = new GetProfileRequest();
            request.Discriminator = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "discriminator").Value;
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPost("signup")]
        public async Task<ObjectResult> SignUp([FromBody] CreateAccountRequest request)
        {
            return await Result(request);
        }

        [HttpPost("signin")]
        public async Task<ObjectResult> SignIn([FromBody] LoginRequest request)
        {
            return await Result(request);
        }

        [HttpPost("request-new-password")]
        public async Task<ObjectResult> RequestNewPassword([FromBody] RequestNewPasswordRequest request)
        {
            return await Result(request);
        }

        [HttpPut("change-password-forgotten")]
        public async Task<ObjectResult> ChangePasswordForgotten([FromBody] ChangePasswordForgottenRequest request)
        {
            return await Result(request);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<ObjectResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            request.Discriminator = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "discriminator").Value;
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPut("change-user")]
        [Authorize]
        public async Task<ObjectResult> ChangeUser([FromBody] ChangeUserRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPut("verify-account")]
        public async Task<ObjectResult> VerifyAccount([FromBody] VerifyAccountRequest request)
        {
            return await Result(request);
        }

        [HttpPost("request-new-access")]
        [Authorize]
        public async Task<ObjectResult> RequestNewAccess([FromBody] RequestNewAccessRequest request)
        {
            request.Discriminator = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "discriminator").Value;
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpPut("change-access")]
        [Authorize]
        public async Task<ObjectResult> ChangeAccess([FromBody] ChangeAccessRequest request)
        {
            request.Discriminator = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "discriminator").Value;
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ObjectResult> DeletarConta([FromBody] DeleteAccountRequest request)
        {
            request.UserId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return await Result(request);
        }
    }
}
