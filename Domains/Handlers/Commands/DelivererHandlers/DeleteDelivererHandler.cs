

namespace Domain.Handlers.Commands.DelivererHandlers
{
    /*public class DeleteDelivererHandler : IHandlerCommand<DeleteDelivererCommand>
    {
        private IDelivererRepository Repository { get; set; }

        public DeleteDelivererHandler(IDelivererRepository repository)
        {
            Repository = repository;
        }

        public ICommandResult Handle(DeleteDelivererCommand command)
        {
            var deliverer = Repository.Search(command.CellphoneNumber);

            if (deliverer == null)
                return new GenericCommandResult(false, "Usuário inexistente ou você não tem permissão para deletá-lo!", null);

            Repository.Remove(deliverer.UserId);

            return new GenericCommandResult(true, "Usuário deletado com sucesso! Quando quiser voltar, estaremos à disposição.. :(", null);
        }
    }*/
}