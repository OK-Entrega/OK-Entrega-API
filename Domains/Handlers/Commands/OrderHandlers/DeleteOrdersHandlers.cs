using Commom.Commands;
using Domains.Commands.Requests.OrderRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.OrderHandlers
{
    public class DeleteOrdersHandlers : IRequestHandler<DeleteOrdersRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrdersHandlers(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<GenericCommandResult> Handle(DeleteOrdersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.OrdersIds == null)
                    return Task.FromResult(new GenericCommandResult(400, "Nenhuma entrega a ser deletada!", null));

                foreach (var orderId in request.OrdersIds)
                {
                    var order = _orderRepository.Search(orderId);
                    if(order != null)
                        _orderRepository.Delete(order);
                }

                return Task.FromResult(new GenericCommandResult(200, "Entregas deletadas com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
