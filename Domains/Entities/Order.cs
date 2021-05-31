using Commom.Entities;
using Commom.Enum;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Order : Entity
    {
        //Dados da NFE
        public string XMLPath { get; private set; }
        public EnModelNFE ModelNFE { get; private set; }
        public string Series { get; private set; }
        public string Number { get; private set; }
        public string AccessKey { get; private set; }
        public string NatureOperation { get; private set; }
        public EnIssueType IssueType { get; private set; }
        public string CFOP { get; private set; }
        public string NumericCode { get; private set; }
        public string VerifyingDigit { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public DateTime DispatchedAt { get; private set; }
        public string UFIssuerCode { get; private set; }

        //Dados do destinatário
        public string ReceiverName { get; private set; }
        public string ReceiverCNPJ { get; private set; }

        //Dados da transportadora
        public string CarrierName { get; private set; }
        public string CarrierCNPJ { get; private set; }
        public EnVehicleType VehicleType { get; private set; }
        public string VehiclePlate { get; private set; }

        //Informações de destino
        public string DestinationCEP { get; private set; }
        public string DestinationAddress { get; private set; }
        public string DestinationNumber { get; private set; }
        public string DestinationComplement { get; private set; }
        public string DestinationDistrict { get; private set; }
        public string DestinationUF { get; private set; }
        public string DestinationCity { get; private set; }

        //Informações adicionais
        public FinishOrder FinishOrder { get; private set; }
        public ICollection<OccurrenceOrder> Occurrences { get; private set; }

        //Dados do embarcador
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }
        public string IssuerCEP { get; private set; }
        public string IssuerAddress { get; private set; }
        public string IssuerNumber { get; private set; }
        public string IssuerComplement { get; private set; }

        public string IssuerDistrict { get; private set; }
        public string IssuerUF { get; private set; }
        public string IssuerCity { get; private set; }

        public Order(
            string xmlPath,
            string ufIsserCode,
            DateTime issuedAt,
            Company company,
            EnModelNFE modelNFE,
            string series,
            string number,
            EnIssueType issueType,
            string numericCode,
            string verifyingDigit,
            string natureOperation,
            string cfop,
            DateTime dispatchedAt,
            string issuerCEP,
            string issuerAddress,
            string issuerNumber,
            string issuerComplement,
            string issuerDistrict,
            string issuerUF,
            string issuerCity,
            string receiverName,
            string receiverCNPJ,
            string destinationCEP,
            string destinationAddress,
            string destinationNumber,
            string destinationComplement,
            string destinationDistrict,
            string destinationUF,
            string destinationCity,
            string carrierName,
            string carrierCNPJ,
            EnVehicleType vehicleType,
            string vehiclePlate,
            decimal totalValue,
            decimal weight
        )
        {
            XMLPath = xmlPath;
            Series = series;
            Number = number;
            NumericCode = numericCode;
            ModelNFE = modelNFE;
            AccessKey = $"{ufIsserCode}{issuedAt.Year.ToString().Substring(2, 2)}{issuedAt.ToString("MM")}{company.CNPJ}{(int)modelNFE}{series}{number}{(int)issueType}{numericCode}{verifyingDigit}";
            NatureOperation = natureOperation;
            IssueType = issueType;
            CFOP = cfop;
            NumericCode = numericCode;
            TotalValue = Math.Round(totalValue, 2);
            Weight = Math.Round(weight, 2);
            IssuedAt = issuedAt;
            DispatchedAt = dispatchedAt;
            ReceiverName = receiverName;
            ReceiverCNPJ = receiverCNPJ;
            CarrierName = carrierName;
            CarrierCNPJ = carrierCNPJ;
            VehicleType = vehicleType;
            VehiclePlate = VehicleType == EnVehicleType.Ship ? null : vehiclePlate;
            Company = company;
            UFIssuerCode = ufIsserCode;
            VerifyingDigit = verifyingDigit;

            DestinationCEP = destinationCEP;
            DestinationAddress = destinationAddress;
            DestinationNumber = destinationNumber;
            DestinationComplement = destinationComplement;
            DestinationDistrict = destinationDistrict;
            DestinationUF = destinationUF;
            DestinationCity = destinationCity;

            IssuerCEP = issuerCEP;
            IssuerAddress = issuerAddress;
            IssuerNumber = issuerNumber;
            IssuerComplement = issuerComplement;
            IssuerDistrict = issuerDistrict;
            IssuerUF = issuerUF;
            IssuerCity = issuerCity;

            Occurrences = new List<OccurrenceOrder>();
        }

        public Order(){}
    }
}
