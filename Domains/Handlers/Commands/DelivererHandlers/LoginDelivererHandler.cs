using Commom.Commands;
using Commom.Utils;
using Domains.Commands.Requests.DelivererRequests;
using Domains.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.DelivererHandlers
{
    public class LoginDelivererHandler : IRequestHandler<LoginDelivererRequest, GenericCommandResult>
    {
        private readonly IDelivererRepository _delivererRepository;

        public LoginDelivererHandler(IDelivererRepository delivererRepository)
        {
            _delivererRepository = delivererRepository;
        }

        public Task<GenericCommandResult> Handle(LoginDelivererRequest request, CancellationToken cancellationToken)
        {
            var deliverer = _delivererRepository.Search(request.CellphoneNumber);

            if (deliverer == null)
                return Task.FromResult(new GenericCommandResult(400, "Não existe nenhum usuário cadastrado com o número de celular informado!", request.CellphoneNumber));

            var isCorrectPassword = Password.Validate(request.Password, deliverer.User.Password);

            if (!isCorrectPassword)
                return Task.FromResult(new GenericCommandResult(400, "Senha incorreta!", null));

            var token = JWT.Generate(deliverer.User.Name, "Deliverer", deliverer.UserId, 120);

            return Task.FromResult(new GenericCommandResult(200, "Bem vindo novamente, " + deliverer.User.Name + "!", token));
        }
    }
}
