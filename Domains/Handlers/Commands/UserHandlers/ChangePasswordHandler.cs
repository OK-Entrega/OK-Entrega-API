using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _userRepository.Search(request.UserId);

                if(user == null)
                    return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário com este id!", null));

                if(!PasswordServices.Validate(request.CurrentPassword, user.Password))
                    return Task.FromResult(new GenericCommandResult(400, "Por questões de segurança, insira a sua senha atual corretamente antes de solicitar a sua alteração!", null));

                if(request.CurrentPassword == request.NewPassword)
                    return Task.FromResult(new GenericCommandResult(400, "A nova senha deve ser diferente da atual!", null));

                var encryptedPassword = PasswordServices.Encrypt(request.NewPassword);

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
