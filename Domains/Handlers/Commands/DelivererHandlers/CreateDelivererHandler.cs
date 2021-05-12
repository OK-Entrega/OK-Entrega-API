using Commom.Commands;
using Commom.Utils;
using Domains.Commands.Requests.DelivererRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.DelivererHandlers
{
    public class CreateDelivererHandler : IRequestHandler<CreateDelivererRequest, GenericCommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDelivererRepository _delivererRepository;

        public CreateDelivererHandler(IUserRepository userRepository, IDelivererRepository delivererRepository)
        {
            _userRepository = userRepository;
            _delivererRepository = delivererRepository;
        }

        public Task<GenericCommandResult> Handle(CreateDelivererRequest request, CancellationToken cancellationToken)
        {
            try
            {
                bool exists = _delivererRepository.Search(request.CellphoneNumber) != null;
                if (exists)
                    return Task.FromResult(new GenericCommandResult(400, "Já existe um usuário cadastrado com esse número de celular!", request.CellphoneNumber));

                string passwordEncoded = Password.Encrypt(request.Password);

                var user = new User(request.Name, passwordEncoded);
                var deliverer = new Deliverer(request.CellphoneNumber, user.Id);

                _userRepository.Create(user);
                _delivererRepository.Add(deliverer);

                //Enviar sms Sendgrid.SendEmail(shipper.Email, "Bem-vindo!", "Seja muito bem vindo ao OKEntrega, um sistema SaaS para ajudar na gestão de entregas da sua empresa!");

                return Task.FromResult(new GenericCommandResult(200, "Seja bem vindo, " + user.Name, JWT.Generate(user.Name, deliverer.CellphoneNumber, user.Id, 100)));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
