using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class ChangePasswordForgottenHandler : IRequestHandler<ChangePasswordForgottenRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordForgottenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(ChangePasswordForgottenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var expired = (request.Token.ValidTo.CompareTo(DateTime.Now)) <= 0;
                if (expired)
                    return Task.FromResult(new GenericCommandResult(400, "O link expirou. Solicite outro. Lembre-se que após a solicitação do link para redefinir sua senha, você tem apenas 5 minutos, por questões de segurança!", null));

                var userId = Guid.Parse(request.Token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                var user = _userRepository.Search(userId);

                if (user == null)
                    return Task.FromResult(new GenericCommandResult(400, "Bad request! Esse erro geralmente acontece quando a url dessa página é diferente da url mandada para você. Para corrigir, solicite outro link.", null));

                var isValidPassword = PasswordServices.ShipperPasswordIsValid(request.Password);

                if (!isValidPassword)
                    return Task.FromResult(new GenericCommandResult(400, "A senha deve conter letras maiúsculas, minúsculas e números!", null));

                var encryptedPassword = PasswordServices.Encrypt(request.Password);

                user.ChangePassword(encryptedPassword);

                _userRepository.Update(user);

                return Task.FromResult(new GenericCommandResult(200, "Senha alterada com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
