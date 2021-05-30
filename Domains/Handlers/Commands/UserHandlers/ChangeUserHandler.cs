using Commom.Commands;
using Domains.Commands.Requests.UserRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.UserHandlers
{
    public class ChangeUserHandler : IRequestHandler<ChangeUserRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(ChangeUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _userRepository.Search(request.UserId);

                if (user == null)
                    return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário com este id!", null));

                if (user.Name == request.Name)
                    return Task.FromResult(new GenericCommandResult(400, "O novo nome está igual ao atual!", null));

                user.ChangeName(request.Name);

                _userRepository.Update(user);

                return Task.FromResult(new GenericCommandResult(200, "Nome alterado com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
