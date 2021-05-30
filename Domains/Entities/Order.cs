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
            decimal totalValue,
            decimal weight
        )
        {
            XMLPath = xmlPath;
            Series = series.Trim().Replace(".", "");
            Number = number.Trim().Replace(".", "");
            NumericCode = numericCode;
            AccessKey = $"{ufIsserCode}{issuedAt.Year.ToString().Substring(2, 2)}{issuedAt.Month}{company.CNPJ}{modelNFE}{series}{number}{issueType}{numericCode}{verifyingDigit}";
            NatureOperation = natureOperation.Trim();
            IssueType = issueType;
            CFOP = cfop.Trim().Replace(".", "");
            NumericCode = numericCode.Trim();
            TotalValue = Math.Round(totalValue, 2);
            Weight = Math.Round(weight, 2);
            IssuedAt = issuedAt;
            DispatchedAt = dispatchedAt;
            ReceiverName = receiverName.Trim();
            ReceiverCNPJ = receiverCNPJ.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            CarrierName = carrierName.Trim();
            CarrierCNPJ = carrierCNPJ.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            Company = company;
            UFIssuerCode = ufIsserCode.Trim();
            VerifyingDigit = verifyingDigit.Trim();

            DestinationCEP = destinationCEP.Trim().Replace("-", "");
            DestinationAddress = destinationAddress.Trim();
            DestinationNumber = destinationNumber.Trim();
            DestinationComplement = destinationComplement.Trim();
            DestinationDistrict = destinationDistrict.Trim();
            DestinationUF = destinationUF.Trim();
            DestinationCity = destinationCity.Trim();

            IssuerCEP = issuerCEP.Trim().Replace("-", "");
            IssuerAddress = issuerAddress.Trim();
            IssuerNumber = issuerNumber.Trim();
            IssuerComplement = issuerComplement.Trim();
            IssuerDistrict = issuerDistrict.Trim();
            IssuerUF = issuerUF.Trim();
            IssuerCity = issuerCity.Trim();

            Occurrences = new List<OccurrenceOrder>();
        }

        public Order(){}
    }
}
