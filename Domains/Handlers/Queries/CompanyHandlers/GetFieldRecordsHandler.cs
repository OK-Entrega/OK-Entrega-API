using Commom.Enum;
using Commom.Queries;
using Domains.Queries.Requests.CompanyRequests;
using Domains.Queries.Responses.CompanyResponses;
using Domains.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.CompanyHandlers
{
    public class GetFieldRecordsHandler : IRequestHandler<GetFieldRecordsRequest, GenericQueryResult>
    {
        private readonly IDelivererRepository _delivererRepository;
        private readonly IOrderRepository _orderRepository;

        public GetFieldRecordsHandler(IDelivererRepository delivererRepository, IOrderRepository orderRepository)
        {
            _delivererRepository = delivererRepository;
            _orderRepository = orderRepository;
        }

        public Task<GenericQueryResult> Handle(GetFieldRecordsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _delivererRepository.Read();

                if (!string.IsNullOrEmpty(request.DelivererName))
                    query = query.Where(d => d.User.Name.ToLower().Contains(request.DelivererName.ToLower()));

                if (request.Date != null)
                    request.Date = request.Date.Value.Date;
                else
                    request.Date = DateTime.Now.Date;

                var result = new List<GetFieldRecordsResponse>();

                foreach (var deliverer in query)
                {
                    result.Add(new GetFieldRecordsResponse(
                        deliverer.User.Name,
                        deliverer.FinishedOrders.Where(f => f.Order.CompanyId == request.CompanyId && f.FinishedAt.Date == request.Date && f.FinishType == EnFinishType.Success).Count(),
                        deliverer.FinishedOrders.Where(f => f.Order.CompanyId == request.CompanyId && f.FinishedAt.Date == request.Date && f.FinishType == EnFinishType.Devolution).Count(),
                        deliverer.OccurrencesOrders.Where(o => o.Order.CompanyId == request.CompanyId && o.CreatedAt.Date == request.Date).Count()
                    ));
                }

                if (!string.IsNullOrEmpty(request.Type))
                    switch (request.Type)
                    {
                        case "Devoluções":
                            result = result.FindAll(d => d.FinishedsWithDevolution > 0);
                            break;
                        case "Entregas":
                            result = result.FindAll(d => d.FinishedsWithSuccess > 0);
                            break;
                        case "Sem ocorrências":
                            result = result.FindAll(d => d.Occurrences == 0);
                            break;
                        case "Com ocorrências":
                            result = result.FindAll(d => d.Occurrences > 0);
                            break;
                    }

                var ordersQuery = _orderRepository.Read(request.CompanyId);

                var finishedsWithSuccess = new
                {
                    Id = "Entregues",
                    Label = "Entregues",
                    Value = ordersQuery.Where(o => o.FinishOrder.FinishedAt.Date == request.Date && o.FinishOrder.FinishType == EnFinishType.Success).Count(),
                    Color = "hsl(145, 63%, 49%)"
                };

                var finishedsWithDevolution = new
                {
                    Id = "Devoluções",
                    Label = "Devoluções",
                    Value = ordersQuery.Where(o => o.FinishOrder.FinishedAt.Date == request.Date && o.FinishOrder.FinishType == EnFinishType.Devolution).Count(),
                    Color = "hsl(0, 100%, 50%)"
                };

                var occurrences = new
                {
                    Id = "Ocorrências",
                    Label = "Ocorrências",
                    Value = ordersQuery.SelectMany(o => o.Occurrences.Where(oc => oc.CreatedAt.Date == request.Date)).Count(),
                    Color = "hsl(36, 99%, 56%)"
                };

                var countNotes = ordersQuery.Where(o => o.CreatedAt.Date == request.Date).Count() - (finishedsWithSuccess.Value + finishedsWithDevolution.Value + occurrences.Value);

                countNotes = countNotes <= 0 ? 0 : countNotes;

                var notes = new
                {
                    Id = "Em aberto",
                    Label = "Em aberto",
                    Value = countNotes,
                    Color = "hsl(0, 0%, 53%)"
                };

                result = result.FindAll(r => r.FinishedsWithDevolution > 0 || r.FinishedsWithSuccess > 0 || r.Occurrences > 0);

                var graph = new[]
                {
                    new
                    {
                        finishedsWithSuccess.Id,
                        finishedsWithSuccess.Label,
                        finishedsWithSuccess.Value,
                        finishedsWithSuccess.Color
                    },
                    new
                    {
                        finishedsWithDevolution.Id,
                        finishedsWithDevolution.Label,
                        finishedsWithDevolution.Value,
                        finishedsWithDevolution.Color
                    },
                    new
                    {
                        notes.Id,
                        notes.Label,
                        notes.Value,
                        notes.Color
                    },
                    new
                    {
                        occurrences.Id,
                        occurrences.Label,
                        occurrences.Value,
                        occurrences.Color
                    }
                };

                if (!result.Any())
                    return Task.FromResult(new GenericQueryResult(404, null, null));

                result = result.OrderBy(d => d.DelivererName).ThenBy(d => d.FinishedsWithSuccess).ThenBy(d => d.FinishedsWithDevolution).ThenBy(d => d.Occurrences).ToList();

                return Task.FromResult(new GenericQueryResult(200, null, new { deliverers = result.FindAll(d => d.FinishedsWithDevolution > 0 || d.FinishedsWithSuccess > 0 || d.Occurrences > 0), graph }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
