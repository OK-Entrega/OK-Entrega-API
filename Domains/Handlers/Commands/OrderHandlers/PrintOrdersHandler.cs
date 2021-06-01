using Commom.Commands;
using Commom.Services.PDFServices.Interfaces;
using Domains.Commands.Requests.OrderRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.OrderHandlers
{
    public class PrintOrdersHandler : IRequestHandler<PrintOrdersRequest, GenericCommandResult>
    {
        private readonly IOrderRepository _orderRepository;

        public PrintOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<GenericCommandResult> Handle(PrintOrdersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if(request.OrdersIds == null)
                    return Task.FromResult(new GenericCommandResult(400, "Nenhuma nota a ser impressa!", null));

                var orders = new List<Order>();

                foreach (var orderId in request.OrdersIds)
                {
                    var order = _orderRepository.Search(orderId);
                    orders.Add(order);
                }

                return Task.FromResult(new GenericCommandResult(200, null, orders));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
