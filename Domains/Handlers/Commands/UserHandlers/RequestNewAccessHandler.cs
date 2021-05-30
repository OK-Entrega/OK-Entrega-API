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
    public class RequestNewAccessHandler : IRequestHandler<RequestNewAccessRequest, GenericCommandResult>
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IDelivererRepository _delivererRepository;
        private readonly IUserRepository _userRepository;

        public RequestNewAccessHandler(IShipperRepository shipperRepository, IDelivererRepository delivererRepository, IUserRepository userRepository)
        {
            _shipperRepository = shipperRepository;
            _delivererRepository = delivererRepository;
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(RequestNewAccessRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Discriminator == null)
                    return Task.FromResult(new GenericCommandResult(400, null, null));
                else if (request.Discriminator == "Shipper")
                {
                    var user = _userRepository.Search(request.UserId);

                    if (user.Shipper.Email == request.NewEmail)
                        return Task.FromResult(new GenericCommandResult(400, "O novo email deve ser diferente do atual!", null));

                    if (_shipperRepository.Search(request.NewEmail) != null)
                        return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse email!", null));

                    user.Shipper.RequestNewEmail($"{GenerateCode()}{request.NewEmail}");

                    _userRepository.Update(user);

                    MessageServices.SendEmail(request.NewEmail, "Verificação de email", $"<p style='color: black; font-weight: bold'>Olá, {user.Name}!<br> Para sua segurança, antes de redefinirmos seu email, precisamos que você insira este código de 4 dígitos na tela que aparece no sistema!</p><br><p style='text-align: center; font-weight: bold; color: #2ecc71'>{user.Shipper.CodeEmail}</p>");

                    return Task.FromResult(new GenericCommandResult(200, $"Para sua segurança, um código de 4 dígitos será enviado para {request.NewEmail}. Insira este código no campo abaixo e após isso redefiniremos o seu email!", null));
                }
                else
                {
                    var user = _userRepository.Search(request.UserId);

                    if (user.Deliverer.CellphoneNumber == request.NewCellphoneNumber)
                        return Task.FromResult(new GenericCommandResult(400, "O novo número de celular deve ser diferente do atual!", null));

                    if (_delivererRepository.Search(request.NewCellphoneNumber) != null)
                        return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse número de celular!", null));

                    user.Deliverer.RequestNewCellphoneNumber($"{GenerateCode()}{request.NewCellphoneNumber}");

                    _userRepository.Update(user);

                    MessageServices.SendSMS(request.NewCellphoneNumber, $"Olá, {user.Name}! Para sua segurança, antes de redefinirmos seu número de celular, precisamos que você insira este código de 4 dígitos na tela que aparece no sistema! Código: {user.Deliverer.CodeCellphoneNumber}");

                    return Task.FromResult(new GenericCommandResult(200, $"Para sua segurança, um código de 4 dígitos será enviado para {request.NewCellphoneNumber}. Insira este código no campo abaixo e após isso redefiniremos o seu número de celular!", null));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }

        private string GenerateCode()
        {
            string caracters = "0123456789";
            string code = "";

            Random random = new Random();

            for (int c = 0; c < 4; c++)
            {
                code += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return code;
        }
    }
}
