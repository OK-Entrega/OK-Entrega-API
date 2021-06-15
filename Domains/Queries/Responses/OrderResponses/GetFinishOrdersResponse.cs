using System;
using System.Collections.Generic;

namespace Domains.Queries.Responses.OrderResponses
{
    public class GetFinishOrdersResponse
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverCNPJ { get; set; }

        public string CarrierName { get; set; }
        public string CarrierCNPJ { get; set; }

        public string DelivererName { get; set; }
        public string DelivererCellphoneNumber { get; set; }

        public string IssuedAt { get; set; }
        public string DispatchedAt { get; set; }
        public string FinishedAt { get; set; }

        public string VehicleType { get; set; }
        public string VehiclePlate { get; set; }

        public decimal TotalValue { get; set; }
        public decimal Weight { get; set; }

        public string AccessKey { get; set; }

        public string DestinationCEP { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationDistrict { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationUF { get; set; }
        public string DestinationNumber { get; set; }
        public string DestinationComplement { get; set; }

        public string Documents { get; set; }

        public ICollection<Occurrences> Occurrences { get; set; }

        public GetFinishOrdersResponse(
            Guid id,
            string type,
            string receiverName,
            string receiverCNPJ,
            string carrierName,
            string carrierCNPJ,
            string delivererName,
            string delivererCellphoneNumber,
            string issuedAt,
            string dispatchedAt,
            string finishedAt,
            string vehicleType,
            string vehiclePlate,
            decimal totalValue,
            decimal weight,
            string accessKey,
            string destinationCEP,
            string destinationAddress,
            string destinationDistrict,
            string destinationCity,
            string destinationUF,
            string destinationNumber,
            string destinationComplement,
            string xmlPath,
            string urlsEvidences,
            ICollection<Occurrences> occurrences
        )
        {
            Id = id;
            Type = type;
            ReceiverName = receiverName;
            ReceiverCNPJ = receiverCNPJ;
            CarrierName = carrierName;
            CarrierCNPJ = carrierCNPJ;
            DelivererName = delivererName;
            DelivererCellphoneNumber = delivererCellphoneNumber;
            IssuedAt = issuedAt;
            DispatchedAt = dispatchedAt;
            FinishedAt = finishedAt;
            VehicleType = vehicleType;
            VehiclePlate = vehiclePlate;
            TotalValue = totalValue;
            Weight = weight;
            AccessKey = accessKey;
            DestinationCEP = destinationCEP;
            DestinationAddress = destinationAddress;
            DestinationDistrict = destinationDistrict;
            DestinationCity = destinationCity;
            DestinationUF = destinationUF;
            DestinationNumber = destinationNumber;
            DestinationComplement = destinationComplement;
            Documents = $"{xmlPath} {urlsEvidences}";
            Occurrences = occurrences;
        }
    }
}
