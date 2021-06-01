using Commom.Commands;
using Commom.Services;
using Domains.Commands.Requests.OrderRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.OrderHandlers
{
    public class CreateOccurrenceHandler : IRequestHandler<CreateOccurrenceRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public CreateOccurrenceHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(CreateOccurrenceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _orderRepository.Search(request.AccessKey);

                if(order == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa nota não está cadastrada na base!", null));

                if (order.FinishOrder != null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa entrega já foi finalizada!", null));

                var deliverer = _userRepository.Search(request.UserId).Deliverer;

                if(deliverer == null)
                    return Task.FromResult(new GenericCommandResult(400, "Esse entregador não existe!", null));

                string evidences= "";

                if(request.Evidences != null)
                {
                    foreach (var evidence in request.Evidences.Take(5))
                    {
                        var filePath = UploadServices.Image(evidence);
                        evidences += filePath + " ";
                    }
                }

                evidences = evidences.Trim();

                var occurrenceOrder = new OccurrenceOrder(request.ReasonOccurrence, deliverer.Id, request.LatitudeDeliverer, request.LongitudeDeliverer, order.Id, evidences);

                _orderRepository.CreateOccurrence(occurrenceOrder);

                return Task.FromResult(new GenericCommandResult(200, "Ocorrência cadastrada com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
