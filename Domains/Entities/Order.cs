using Commom.Entities;
using Commom.Enum;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class Order : Entity
    {
        //Dados da NFE
        public EnModelNFE ModelNFE { get; private set; }
        public int Series { get; private set; }
        public int Number { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public int AccessKey { get; private set; }

        //Dados do emissor
        public string IssuerUF { get; private set; }
        public string IssuerName { get; private set; }
        public string IssuerCompanyName { get; private set; }
        public int FromCNPJ { get; private set; }
        public EnIssueType IssueType { get; private set; }

        //Dados do remetente
        public string ReceiverName { get; private set; }
        public string ReceiverCompanyName { get; private set; }
        public int ToCNPJ { get; private set; }
        public string ReceiverEmail { get; private set; }

        //Impossibilidade de adicionar tributos...

        //Dados do transporte
        public EnFreightType FreightType { get; private set; }
        public string CarrierName { get; private set; }
        public EnFreightVehicleType FreightVehicleType { get; private set; }
        public decimal Volume { get; private set; }
        public decimal FreightPrice { get; private set; }

        //Informações de destino
        public Local Local { get; private set; }
        public EnPaymentType PaymentType { get; private set; }

        //Informações adicionais
        public decimal TotalPriceNFE { get; private set; }
        public int VerifyingDigit { get; private set; }
        public int AttemptsNumber { get; private set; }
        public List<string> UrlsEvidencesImages { get; private set; }
        public Company Company { get; private set; }

        public Order(
            string description,
            EnOrderType type,
            string nfe,
            string toCNPJ,
            int attemptsNumber,
            Local local,
            Company company
        )
        {
            Description = description;
            Type = type;
            NFE = nfe;
            ToCNPJ = toCNPJ;
            FinishedAt = null;
            AttemptsNumber = 0;
            UrlsEvidencesImages = new List<string>();
            Local = local;
            Company = company;
        }
    }
}
