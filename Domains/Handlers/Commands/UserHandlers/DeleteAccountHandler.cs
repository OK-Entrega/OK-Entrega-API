using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteAccountHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _userRepository.Search(request.UserId);

                if (user == null)
                    return Task.FromResult(new GenericCommandResult(400, "Usuário não encontrado!", null));

                if (!PasswordServices.Validate(request.Password, user.Password))
                    return Task.FromResult(new GenericCommandResult(400, "Para garantir a autenticidade dessa ação de risco, exigimos que você digite sua senha corretamente antes de deletar sua conta!", null));

                _userRepository.Delete(user);

                return Task.FromResult(new GenericCommandResult(200, "Infelizmente, você teve sucesso em nos deixar! :(", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
