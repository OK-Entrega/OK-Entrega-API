using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class LoginHandler : IRequestHandler<LoginRequest, GenericCommandResult>
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IDelivererRepository _delivererRepository;

        public LoginHandler(IShipperRepository shipperRepository, IDelivererRepository delivererRepository)
        {
            _shipperRepository = shipperRepository;
            _delivererRepository = delivererRepository;
        }

        public Task<GenericCommandResult> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Discriminator == null)
                    return Task.FromResult(new GenericCommandResult(400, null, null));
                else if (request.Discriminator == "Shipper")
                {
                    var shipper = _shipperRepository.Search(request.Email);
                    if (shipper == null)
                        return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário cadastrado com esse email!", null));

                    if(!shipper.User.Active)
                        return Task.FromResult(new GenericCommandResult(400, "O email associado a esta conta ainda não foi verificado!", null));

                    if(!PasswordServices.Validate(request.Password, shipper.User.Password))
                        return Task.FromResult(new GenericCommandResult(400, "Senha incorreta!", null));

                    var token = JWTServices.Generate(shipper.User.Name, "Shipper", shipper.UserId, 44640);

                    return Task.FromResult(new GenericCommandResult(200, $"Bem vindo novamente, {shipper.User.Name}!", token));
                }
                else
                {
                    var deliverer = _delivererRepository.Search(request.CellphoneNumber);
                    if (deliverer == null)
                        return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário cadastrado com esse número de celular!", null));

                    if (!deliverer.User.Active && deliverer.VerifyingCode.Length != 0)
                        return Task.FromResult(new GenericCommandResult(400, "O número de celular associado a esta conta ainda não foi verificado!", null));

                    if (!PasswordServices.Validate(request.Password, deliverer.User.Password))
                        return Task.FromResult(new GenericCommandResult(400, "Senha incorreta!", null));
                    
                    var token = JWTServices.Generate(deliverer.User.Name, "Shipper", deliverer.UserId, 44640);

                    return Task.FromResult(new GenericCommandResult(200, $"Bem vindo novamente, {deliverer.User.Name}!", token));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
