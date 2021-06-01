using Commom.Commands;
using Commom.Enum;
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
    public class FinishOrderHandler : IRequestHandler<FinishOrderRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public FinishOrderHandler(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(FinishOrderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _orderRepository.Search(request.AccessKey);

                if (order == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa nota não está cadastrada na base!", null));

                if (order.FinishOrder != null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa entrega já foi finalizada!", null));

                var deliverer = _userRepository.Search(request.UserId).Deliverer;

                if (deliverer == null)
                    return Task.FromResult(new GenericCommandResult(400, "Esse entregador não existe!", null));

                string evidences = null;

                if (request.Evidences != null)
                {
                    foreach (var evidence in request.Evidences.Take(5))
                    {
                        var filePath = UploadServices.Image(evidence);
                        evidences += filePath + " ";
                    }
                    evidences = evidences.Trim();
                }

                if (request.FinishType == EnFinishType.Success)
                {
                    var voucherPath = UploadServices.Image(request.Voucher);

                    var voucher = new Voucher(voucherPath);

                    var finishOrder = new FinishOrder(evidences, null, deliverer.Id, request.LatitudeDeliverer, request.LongitudeDeliverer, request.FinishedAt, order.Id, EnFinishType.Success, voucher);

                    _orderRepository.Finish(finishOrder);

                    //mensagens caso de nota baixa

                    return Task.FromResult(new GenericCommandResult(200, "Entrega cadastrada com sucesso!", null));
                }
                else
                {
                    var finishOrder = new FinishOrder(evidences, request.ReasonDevolution, deliverer.Id, request.LatitudeDeliverer, request.LongitudeDeliverer, request.FinishedAt, order.Id, EnFinishType.Devolution);

                    _orderRepository.Finish(finishOrder);

                    return Task.FromResult(new GenericCommandResult(200, "Devolução cadastrada com sucesso!", null));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
