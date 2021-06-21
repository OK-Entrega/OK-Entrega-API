using Commom.Enum;
using Commom.Queries;
using Commom.Services;
using Domains.Entities;
using Domains.Queries.Requests.OrderRequests;
using Domains.Queries.Responses.OrderResponses;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.OrderHandlers
{
    public class GetFinishOrdersHandler : IRequestHandler<GetFinishOrdersRequest, GenericQueryResult>
    {
        private readonly IOrderRepository _orderRepository;

        public GetFinishOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<GenericQueryResult> Handle(GetFinishOrdersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _orderRepository.Read(request.CompanyId).Where(o => o.FinishOrder != null);

                if (query == null)
                    return Task.FromResult(new GenericQueryResult(404, null, null));

                if (!string.IsNullOrEmpty(request.Type))
                {
                    if (request.Type == "Entregas")
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                    else if (request.Type == "Devoluções")
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Devolution);
                }

                if (!string.IsNullOrEmpty(request.AccessKey))
                    query = query.Where(o => o.AccessKey.Contains(request.AccessKey));

                if (!string.IsNullOrEmpty(request.ReceiverName))
                    query = query.Where(o => o.ReceiverName.ToLower().Contains(request.ReceiverName.ToLower()));
                if (!string.IsNullOrEmpty(request.ReceiverCNPJ))
                    query = query.Where(o => o.ReceiverCNPJ.Contains(request.ReceiverCNPJ));

                if (!string.IsNullOrEmpty(request.CarrierName))
                    query = query.Where(o => o.CarrierName.ToLower().Contains(request.CarrierName.ToLower()));
                if (!string.IsNullOrEmpty(request.CarrierCNPJ))
                    query = query.Where(o => o.CarrierCNPJ.Contains(request.CarrierCNPJ));

                if (!string.IsNullOrEmpty(request.DelivererName))
                    query = query.Where(o => o.FinishOrder.Deliverer.User.Name.ToLower().Contains(request.DelivererName.ToLower()));
                if (!string.IsNullOrEmpty(request.DelivererCellphoneNumber))
                    query = query.Where(o => o.FinishOrder.Deliverer.CellphoneNumber.Contains(request.DelivererCellphoneNumber));

                if (request.IssuedAtLessThen != null)
                    query = query.Where(o => o.IssuedAt <= request.IssuedAtLessThen);
                if (request.IssuedAtBiggerThen != null)
                    query = query.Where(o => o.IssuedAt >= request.IssuedAtBiggerThen);

                if (request.DispatchedAtLessThen != null)
                    query = query.Where(o => o.DispatchedAt <= request.DispatchedAtLessThen);
                if (request.DispatchedAtBiggerThen != null)
                    query = query.Where(o => o.DispatchedAt >= request.DispatchedAtBiggerThen);

                if (request.FinishedAtLessThen != null)
                    query = query.Where(o => o.FinishOrder.FinishedAt <= request.FinishedAtLessThen);
                if (request.DispatchedAtBiggerThen != null)
                    query = query.Where(o => o.FinishOrder.FinishedAt >= request.FinishedAtBiggerThen);

                if (request.VehicleType != null)
                    query = query.Where(o => o.VehicleType == request.VehicleType);
                if (request.VehiclePlate != null)
                    query = query.Where(o => o.VehiclePlate.ToLower().Contains(request.VehiclePlate.ToLower()));

                if (request.TotalValueLessThen != null && request.TotalValueLessThen != 0)
                    query = query.Where(o => o.TotalValue <= request.TotalValueLessThen);
                if (request.TotalValueBiggerThen != null && request.TotalValueBiggerThen != 0)
                    query = query.Where(o => o.TotalValue >= request.TotalValueBiggerThen);

                if (request.WeightLessThen != null && request.WeightLessThen != 0)
                    query = query.Where(o => o.Weight <= request.WeightLessThen);
                if (request.WeightBiggerThen != null && request.WeightBiggerThen != 0)
                    query = query.Where(o => o.Weight >= request.WeightBiggerThen);

                if (!string.IsNullOrEmpty(request.DestinationCEP))
                    query = query.Where(o => o.DestinationCEP.Contains(request.DestinationCEP));
                else
                {
                    if (!string.IsNullOrEmpty(request.DestinationAddress))
                        query = query.Where(o => o.DestinationAddress.ToLower().Contains(request.DestinationAddress.ToLower()));
                    if (!string.IsNullOrEmpty(request.DestinationDistrict))
                        query = query.Where(o => o.DestinationDistrict.ToLower().Contains(request.DestinationDistrict.ToLower()));
                    if (!string.IsNullOrEmpty(request.DestinationCity))
                        query = query.Where(o => o.DestinationCity.ToLower().Contains(request.DestinationCity.ToLower()));
                    if (!string.IsNullOrEmpty(request.DestinationUF))
                        query = query.Where(o => o.DestinationUF.ToLower().Contains(request.DestinationUF.ToLower()));
                }
                if (!string.IsNullOrEmpty(request.DestinationNumber))
                    query = query.Where(o => o.DestinationNumber.Contains(request.DestinationNumber));
                if (!string.IsNullOrEmpty(request.DestinationComplement))
                    query = query.Where(o => o.DestinationComplement.ToLower().Contains(request.DestinationComplement.ToLower()));

                if (!string.IsNullOrEmpty(request.VoucherSituation))
                {
                    query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                    if (request.VoucherSituation == "Válidos")
                        query = query.Where(o => o.FinishOrder.Voucher.Score > 50);
                    if (request.VoucherSituation == "Inválidos")
                        query = query.Where(o => o.FinishOrder.Voucher.Score < 50);
                    if (request.VoucherSituation == "Sem data")
                        query = query.Where(o => o.FinishOrder.Voucher.HasData < 50);
                    if (request.VoucherSituation == "Sem assinatura")
                        query = query.Where(o => o.FinishOrder.Voucher.HasSignature < 50);
                    if (request.VoucherSituation == "Sem número e série")
                        query = query.Where(o => o.FinishOrder.Voucher.HasNumberAndSeries < 50);
                    //if (request.VoucherSituation == "Data inválida")
                    //    query = query.Where(o => o.FinishOrder.Voucher.DataIsCorrect < 50);
                    //if (request.VoucherSituation == "Número e série inválidos")
                    //    query = query.Where(o => o.FinishOrder.Voucher.NumberAndSeriesIsCorrect < 50);
                }

                if (query == null || query.Count() < 1)
                    return Task.FromResult(new GenericQueryResult(404, null, null));

                Expression<Func<Order, object>> orderByExpression = null;

                switch (request.OrderBy)
                {
                    case "receiverName":
                        orderByExpression = o => o.ReceiverName;
                        break;
                    case "receiverCNPJ":
                        orderByExpression = o => o.ReceiverCNPJ;
                        break;
                    case "carrierName":
                        orderByExpression = o => o.CarrierName;
                        break;
                    case "carrierCNPJ":
                        orderByExpression = o => o.CarrierCNPJ;
                        break;
                    case "issuedAt":
                        orderByExpression = o => o.IssuedAt;
                        break;
                    case "dispatchedAt":
                        orderByExpression = o => o.DispatchedAt;
                        break;
                    case "finishedAt":
                        orderByExpression = o => o.FinishOrder.FinishedAt;
                        break;
                    case "vehicleType":
                        orderByExpression = o => o.VehicleType;
                        break;
                    case "vehiclePlate":
                        orderByExpression = o => o.VehiclePlate;
                        break;
                    case "totalValue":
                        orderByExpression = o => o.TotalValue;
                        break;
                    case "weight":
                        orderByExpression = o => o.Weight;
                        break;
                    case "accessKey":
                        orderByExpression = o => o.AccessKey;
                        break;
                    case "destinationCEP":
                        orderByExpression = o => o.DestinationCEP;
                        break;
                    case "destinationAddress":
                        orderByExpression = o => o.DestinationAddress;
                        break;
                    case "destinationDistrict":
                        orderByExpression = o => o.DestinationDistrict;
                        break;
                    case "destinationCity":
                        orderByExpression = o => o.DestinationCity;
                        break;
                    case "destinationUF":
                        orderByExpression = o => o.DestinationUF;
                        break;
                    case "destinationNumber":
                        orderByExpression = o => o.DestinationNumber;
                        break;
                    case "destinationComplement":
                        orderByExpression = o => o.DestinationComplement;
                        break;
                    case "typeDevolutions":
                        orderByExpression = o => o.FinishOrder.FinishType == EnFinishType.Devolution;
                        break;
                    case "typeSuccess":
                        orderByExpression = o => o.FinishOrder.FinishType == EnFinishType.Success;
                        break;
                    case "voucherSituationValid":
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                        orderByExpression = o => o.FinishOrder.Voucher.Score > 50;
                        break;
                    case "voucherSituationInvalid":
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                        orderByExpression = o => o.FinishOrder.Voucher.Score < 50;
                        break;
                    case "voucherSituationHasNoData":
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                        orderByExpression = o => o.FinishOrder.Voucher.HasData < 50;
                        break;
                    case "voucherSituationHasNoSignature":
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                        orderByExpression = o => o.FinishOrder.Voucher.HasSignature < 50;
                        break;
                    case "voucherSituationHasNoNumberAndSeries":
                        query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                        orderByExpression = o => o.FinishOrder.Voucher.HasNumberAndSeries < 50;
                        break;
                    //case "voucherSituationDataIsIncorrect":
                    //    query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                    //    orderByExpression = o => o.FinishOrder.Voucher.DataIsCorrect < 50;
                    //    break;
                    //case "voucherSituationNumberAndSeriesIsIncorrect":
                    //    query = query.Where(o => o.FinishOrder.FinishType == EnFinishType.Success);
                    //    orderByExpression = o => o.FinishOrder.Voucher.NumberAndSeriesIsCorrect < 50;
                    //    break;
                    default:
                        orderByExpression = o => o.CreatedAt;
                        break;
                }

                switch (request.Descending)
                {
                    case true:
                        query = query.OrderByDescending(orderByExpression);
                        break;
                    case false:
                        query = query.OrderBy(orderByExpression);
                        break;
                }

                int pageCount = (((query.Count() - 1) / 20) + 1);
                query = query.Skip((request.Page - 1) * 20).Take(20);

                var result = new
                {
                    PageCount = pageCount,
                    PendingOrders = query.Select(o => new GetFinishOrdersResponse(
                            o.Id, o.FinishOrder.FinishType == EnFinishType.Devolution ? "Devolução" : "Entregue", o.ReceiverName, o.ReceiverCNPJ, o.CarrierName, o.CarrierCNPJ, o.FinishOrder.Deliverer.User.Name, o.FinishOrder.Deliverer.CellphoneNumber, o.IssuedAt.ToString("dd/MM/yyyy"), o.DispatchedAt.ToString("dd/MM/yyyy"), o.FinishOrder.FinishedAt.ToString("dd/MM/yyyy"), EnumServices.GetDescription(o.VehicleType), o.VehiclePlate, o.TotalValue, o.Weight, o.AccessKey, o.DestinationCEP, o.DestinationAddress, o.DestinationDistrict, o.DestinationCity, o.DestinationUF, o.DestinationNumber, o.DestinationComplement, o.XMLPath, o.FinishOrder.UrlsEvidences, o.Occurrences.OrderBy(oc => oc.CreatedAt).Select(oc => new Occurrences(oc.ReasonOccurrence, oc.Deliverer.User.Name, oc.UrlsEvidences, oc.CreatedAt)).ToList()
                        )
                    ).ToList()
                };

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}