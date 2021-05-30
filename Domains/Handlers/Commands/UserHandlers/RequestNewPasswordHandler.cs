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
    public class RequestNewPasswordHandler : IRequestHandler<RequestNewPasswordRequest, GenericCommandResult>
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IDelivererRepository _delivererRepository;

        public RequestNewPasswordHandler(IShipperRepository shipperRepository, IDelivererRepository delivererRepository)
        {
            _shipperRepository = shipperRepository;
            _delivererRepository = delivererRepository;
        }

        public Task<GenericCommandResult> Handle(RequestNewPasswordRequest request, CancellationToken cancellationToken)
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

                    if (!shipper.User.Active)
                        return Task.FromResult(new GenericCommandResult(400, "O email associado a esta conta ainda não foi verificado!", null));

                    var jwt = JWTServices.Generate(shipper.User.Name, "Shipper", shipper.UserId, 5);

                    var link = $"https://okentrega.com.br/i-forgot-my-password/change/{jwt}";

                    MessageServices.SendEmail(shipper.Email, $"Olá, {shipper.User.Name}!", $"<p style='color: black; font-weight: bold'>Olá, {shipper.User.Name}!<br>Ao clicar no botão abaixo, você será redirecionado para uma página da web onde poderá redefinir sua senha com segurança! <br>(Este link é válido por apenas 5 minutos.)</p><br><a href='{link}'><button style='display: block; margin: auto; border-color: #2ecc71; background: #2ecc71; color: white; font-weight: bold; text-decoration: none; cursor: pointer; box-shadow: none'>Ir</button></a>");

                    return Task.FromResult(new GenericCommandResult(200, $"Um email com mais instruções para a redefinição da senha será enviado à você!", null));
                }
                else
                {
                    var deliverer = _delivererRepository.Search(request.CellphoneNumber);
                    if (deliverer == null)
                        return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário cadastrado com esse número de celular!", null));

                    if (!deliverer.User.Active && deliverer.VerifyingCode.Length != 0)
                        return Task.FromResult(new GenericCommandResult(400, "O número de celular associado a esta conta ainda não foi verificado!", null));

                    var newPassword = GeneratePassword();

                    deliverer.User.ChangePassword(newPassword);

                    _delivererRepository.Update(deliverer);

                    MessageServices.SendSMS(deliverer.CellphoneNumber, $"Olá, {deliverer.User.Name}! Aqui está a sua nova senha provisória: {newPassword}. Você pode alterá-la pelo aplicativo assim que fizer login!");

                    return Task.FromResult(new GenericCommandResult(200, $"Um SMS com uma senha provisória será enviado à você!", null));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }

        private static string GeneratePassword()
        {
            string caracters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@!#$%&";
            string password = "";

            Random random = new Random();

            for (int c = 0; c < 8; c++)
            {
                password += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return password;
        }
    }
}
