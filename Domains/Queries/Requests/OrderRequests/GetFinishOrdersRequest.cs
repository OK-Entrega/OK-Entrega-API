using Commom.Enum;
using Commom.Queries;
using System;

namespace Domains.Queries.Requests.OrderRequests
{
    public class GetFinishOrdersRequest : QueryRequest
    {
        public string Type { get; set; }

        public Guid CompanyId { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverCNPJ { get; set; }

        public string CarrierName { get; set; }
        public string CarrierCNPJ { get; set; }

        public DateTime? IssuedAtLessThen { get; set; }
        public DateTime? IssuedAtBiggerThen { get; set; }

        public DateTime? DispatchedAtLessThen { get; set; }
        public DateTime? DispatchedAtBiggerThen { get; set; }

        public DateTime? FinishedAtLessThen { get; set; }
        public DateTime? FinishedAtBiggerThen { get; set; }

        public string DelivererName { get; set; }
        public string DelivererCellphoneNumber { get; set; }

        public EnVehicleType? VehicleType { get; set; }
        public string VehiclePlate { get; set; }

        public decimal? TotalValueLessThen { get; set; }
        public decimal? TotalValueBiggerThen { get; set; }

        public decimal? WeightLessThen { get; set; }
        public decimal? WeightBiggerThen { get; set; }

        public string AccessKey { get; set; }

        public string DestinationCEP { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationDistrict { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationUF { get; set; }
        public string DestinationNumber { get; set; }
        public string DestinationComplement { get; set; }

        //public string VoucherSituation { get; set; }

        public string OrderBy { get; set; }
        public bool? Descending { get; set; } = false;
        public int Page { get; set; } = 1;

        public GetFinishOrdersRequest(
            string accessKey,
            string receiverCNPJ,
            string carrierCNPJ,
            string vehiclePlate,
            string destinationCEP,
            string delivererCellphoneNumber,
            int page = 1
        )
        {
            AccessKey = accessKey?.Replace(" ", "");
            ReceiverCNPJ = receiverCNPJ?.Replace("/", "").Replace("-", "").Replace(".", "");
            CarrierCNPJ = carrierCNPJ?.Replace("/", "").Replace("-", "").Replace(".", "");
            VehiclePlate = vehiclePlate?.Replace("-", "");
            DestinationCEP = destinationCEP?.Replace("-", "");
            DelivererCellphoneNumber = delivererCellphoneNumber.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            Page = page <= 0 ? 1 : page;
        }

        public GetFinishOrdersRequest(){}
    }
}
