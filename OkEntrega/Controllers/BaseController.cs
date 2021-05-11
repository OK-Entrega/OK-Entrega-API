using Commom.Commands;
using Commom.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseController([FromServices] IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<ObjectResult> Result([FromBody] CommandRequest request)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return StatusCode(400, new GenericCommandResult(400, "Requisição inválida", request.Notifications));

                var result = await _mediator.Send(request);

                return result.StatusCode switch
                {
                    200 => Ok(result),
                    400 => BadRequest(result),
                    401 => Unauthorized(result),
                    404 => NotFound(result),
                    _ => null
                };
            }
            catch(Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(500, "Erro no servidor! Desculpe-nos.", ex.Message));
            }
        }

        protected async Task<ObjectResult> Result([FromBody] QueryRequest request)
        {
            try
            {
                request.Validate();
                if (!request.IsValid)
                    return StatusCode(400, new GenericCommandResult(400, "Requisição inválida", request.Notifications));

                var result = await _mediator.Send(request);

                return result.StatusCode switch
                {
                    200 => Ok(result),
                    401 => Unauthorized(result),
                    404 => NotFound(result)
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericCommandResult(500, "Erro no servidor! Desculpe-nos.", ex.Message));
            }
        }
    }
}
