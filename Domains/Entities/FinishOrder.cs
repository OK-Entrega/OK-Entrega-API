using Commom.Entities;
using Commom.Enum;
using System;
using System.Collections.Generic;

namespace Domains.Entities
{
    public class FinishOrder : Entity
    {
        public EnFinishType FinishType { get; private set; }
        public EnReasonDevolution? ReasonDevolution { get; private set; }
        public ICollection<string> UrlsEvidences { get; private set; }
        public string UrlVoucher { get; private set; }
        public Deliverer Deliverer { get; private set; }
        public Guid DelivererId { get; private set; }
        public Order Order { get; private set; }
        public Guid OrderId { get; private set; }

        public FinishOrder(
            EnReasonDevolution reasonDevolution,
            List<string> urlsEvidences,
            string urlVoucher,
            Guid delivererId,
            Guid orderId,
            EnFinishType finishType = EnFinishType.Success
        )
        {
            FinishType = finishType;
            ReasonDevolution = finishType switch
            {
                EnFinishType.Devolution => reasonDevolution,
                _ => null
            };
            UrlsEvidences = urlsEvidences;
            UrlVoucher = urlVoucher;
            DelivererId = delivererId;
            OrderId = orderId;
        }
    }
}
