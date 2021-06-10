using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.UserRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, GenericCommandResult>
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IDelivererRepository _delivererRepository;

        public CreateAccountHandler(IShipperRepository shipperRepository, IDelivererRepository delivererRepository)
        {
            _shipperRepository = shipperRepository;
            _delivererRepository = delivererRepository;
        }

        public Task<GenericCommandResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.Discriminator == null)
                    return Task.FromResult(new GenericCommandResult(400, null, null));
                else if (request.Discriminator == "Shipper")
                {
                    if(_shipperRepository.Search(request.Email) != null)
                        return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse email!", null));

                    var encryptedPassword = PasswordServices.Encrypt(request.Password);

                    var user = new User(request.Name, encryptedPassword);
                    var shipper = new Shipper(request.Email, user);

                    _shipperRepository.Create(shipper);

                    MessageServices.SendEmail(shipper.Email, "Verificação de email", $"<p style='color: black; font-weight: bold'>Olá, {user.Name}!<br> Seja muito bem-vindo ao OK Entrega! Antes de começar a usufruir de nossos serviços, precisamos verificar se o seu endereço de email é válido. Para isso, basta clicar no botão abaixo e então você estará livre para desfrutar de nossa plataforma! <br>(Sua conta será deletada de nossa base dentro de 1 dia se seu email não for verificado).</p><br><a href='http://localhost:3000/verify-account/shipperId={shipper.Id}'><button style='display: block; margin: auto; border-color: #2ecc71; background: #2ecc71; color: white; font-weight: bold; text-decoration: none; cursor: pointer; box-shadow: none'>Verificar</button></a>");

                    return Task.FromResult(new GenericCommandResult(200, $"Olá, {user.Name}. Um email com mais instruções será enviado a você!", null));
                }
                else
                {
                    if (_delivererRepository.Search(request.CellphoneNumber) != null)
                        return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse número de celular!", null));

                    var encryptedPassword = PasswordServices.Encrypt(request.Password);

                    var user = new User(request.Name, encryptedPassword);
                    var deliverer = new Deliverer(request.CellphoneNumber, user);

                    _delivererRepository.Create(deliverer);

                    MessageServices.SendSMS(deliverer.CellphoneNumber, $"Olá, {user.Name}! Seja muito bem-vindo ao OK Entrega! Antes de começar a usufruir de nossos serviços, precisamos verificar se o seu número de celular é válido. Para isso, basta inserir esses 4 dígitos no seu aplicativo e então você estará livre para desfrutar de nossa plataforma! (Sua conta será deletada de nossa base dentro de 1 dia se seu número de celular não for verificado).");

                    return Task.FromResult(new GenericCommandResult(200, $"Olá, {user.Name}. Um SMS com mais instruções será enviado a você!", deliverer.Id));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
