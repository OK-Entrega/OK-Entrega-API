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
        public string Series { get; private set; }
        public string Number { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public string AccessKey { get; private set; }

        //Dados do emissor
        public Local IssueLocal { get; private set; }
        public string IssuerCompanyName { get; private set; }
        public string IssuerStateRegistration { get; private set; }
        public string FromCNPJ { get; private set; }
        public EnIssueType IssueType { get; private set; }

        //Dados do destinatário
        public string ReceiverCompanyName { get; private set; }
        public string ReceiverStateRegistration { get; private set; }
        public string ToCNPJ { get; private set; }
        public string ReceiverEmail { get; private set; }

        //Impossibilidade de adicionar tributos...

        //Dados do transporte
        public string CarrierName { get; private set; }

        //Informações de destino
        public Local Destination { get; private set; }

        //Informações adicionais
        public decimal TotalPriceNFE { get; private set; }
        public FinishOrder FinishOrder { get; private set; }
        public ICollection<OccurrenceOrder> Occurrences { get; private set; }
        public Company Company { get; private set; }
        public Guid CompanyId { get; private set; }

        public Order(
            
        )
        {
            Occurrences = new List<OccurrenceOrder>();
        }
    }
}
