
using Commom.Commands;
using Domain.Commands.DelivererResponses;
using Domains.Commands.Requests.DelivererRequests;
using Domains.Repositories;
using MediatR;

namespace Domain.Handlers.Commands.DelivererHandlers
{
    /*public class ChangeDelivererHandler : IRequestHandler<ChangeDelivererCommand, GenericCommandResult>
    {
        private IDelivererRepository Repository { get; set; }

        public ChangeDelivererHandler(IDelivererRepository repository)
        {
            Repository = repository;
        }

        public ICommandResult Handle(ChangeDelivererCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(400, "Dados inválidos!", command.Notifications);

            var deliverer = Repository.Search(command.CellphoneNumber);

            if (deliverer == null)
                return new GenericCommandResult(true, "Já existe um usuário cadastrado com o número informado!", command.Email);

            if (deliverer.Id != command.UserId)
                return new GenericCommandResult(false, "Você não tem permissão para alterar esse usuário!", null);

            deliverer.Change(command.Name, command.CellphoneNumber, command.Password);

            deliverer = Repository.Change(deliverer);

            var result = new DelivererGenericCommandResult(deliverer.Name, deliverer.CellphoneNumber, deliverer.Password);

            return new GenericCommandResult(true, "Usuário alterado", result);
        }
    }*/
}