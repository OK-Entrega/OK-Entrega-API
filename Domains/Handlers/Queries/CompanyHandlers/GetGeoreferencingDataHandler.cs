using Commom.Enum;
using Commom.Queries;
using Commom.Services;
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
    public class GetGeoreferencingDataHandler : IRequestHandler<GetGeoreferencingDataRequest, GenericQueryResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOccurrenceRepository _occurrenceRepository;

        public GetGeoreferencingDataHandler(IOrderRepository orderRepository, IOccurrenceRepository occurrenceRepository)
        {
            _orderRepository = orderRepository;
            _occurrenceRepository = occurrenceRepository;
        }

        public Task<GenericQueryResult> Handle(GetGeoreferencingDataRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryOrder = _orderRepository.Read(request.CompanyId).Where(o => o.FinishOrder != null);
                var queryOccurrence = _occurrenceRepository.Read(request.CompanyId);

                if (!string.IsNullOrEmpty(request.Type))
                    switch (request.Type)
                    {
                        case "Devoluções":
                            queryOrder = queryOrder.Where(o => o.FinishOrder.FinishType == EnFinishType.Devolution);
                            queryOccurrence.Where(o => o.CreatedAt < DateTime.MinValue);
                            break;
                        case "Entregues":
                            queryOrder = queryOrder.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                            queryOccurrence.Where(o => o.CreatedAt < DateTime.MinValue);
                            break;
                        case "Ocorrências":
                            queryOrder = queryOrder.Where(o => o.FinishOrder.CreatedAt < DateTime.MinValue);
                            break;
                    }

                if (!string.IsNullOrEmpty(request.DelivererName))
                {
                    queryOrder = queryOrder.Where(o => o.FinishOrder.Deliverer.User.Name.ToLower().Contains(request.DelivererName.ToLower()));
                    queryOccurrence = queryOccurrence.Where(o => o.Deliverer.User.Name.ToLower().Contains(request.DelivererName.ToLower()));
                }

                if (request.DateLessThen != null)
                {
                    queryOrder = queryOrder.Where(o => o.FinishOrder.FinishedAt <= request.DateLessThen);
                    queryOccurrence = queryOccurrence.Where(o => o.CreatedAt <= request.DateLessThen);
                }

                if (request.DateBiggerThen != null)
                {
                    queryOrder = queryOrder.Where(o => o.FinishOrder.FinishedAt >= request.DateLessThen);
                    queryOccurrence = queryOccurrence.Where(o => o.CreatedAt >= request.DateLessThen);
                }

                var result = new List<GetGeoreferencingDataResponse>();

                result.AddRange(queryOrder.Select(o => new GetGeoreferencingDataResponse(o.FinishOrder.Deliverer.User.Name, o.FinishOrder.Deliverer.CellphoneNumber, EnumServices.GetDescription(o.VehicleType), o.VehiclePlate, o.FinishOrder.FinishedAt.ToString("dd/MM/yyyy"), o.AccessKey, EnumServices.GetDescription(o.FinishOrder.FinishType), o.FinishOrder.LatitudeDeliverer, o.FinishOrder.LongitudeDeliverer)));

                result.AddRange(queryOccurrence?.Select(oc => new GetGeoreferencingDataResponse(oc.Deliverer.User.Name, oc.Deliverer.CellphoneNumber, EnumServices.GetDescription(oc.Order.VehicleType), oc.Order.VehiclePlate, oc.CreatedAt.ToString("dd/MM/yyyy"), oc.Order.AccessKey, "Ocorrência", oc.LatitudeDeliverer, oc.LongitudeDeliverer)));

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
