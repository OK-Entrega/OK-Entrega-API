using Commom.Entities;
using Commom.Enum;
using System;

namespace Domains.Entities
{
    public class FinishOrder : Entity
    {
        public string UrlsEvidences { get; private set; }
        public string UrlVoucher { get; private set; }
        public EnFinishType FinishType { get; private set; }
        public EnReasonDevolution? ReasonDevolution { get; private set; }
        public Guid DelivererId { get; private set; }
        public Deliverer Deliverer { get; private set; }
        public decimal LatitudeDeliverer { get; private set; }
        public decimal LongitudeDeliverer { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public FinishOrder(){}

        public FinishOrder(
            EnReasonDevolution reasonDevolution,
            string urlsEvidences,
            string urlVoucher,
            Guid delivererId,
            decimal latitudeDeliverer,
            decimal longitudeDeliverer,
            Guid orderId,
            EnFinishType finishType = EnFinishType.Success
        )
        {
            urlsEvidences = urlsEvidences.Trim();
            urlVoucher = urlVoucher.Trim();

            FinishType = finishType;
            ReasonDevolution = finishType switch
            {
                EnFinishType.Devolution => reasonDevolution,
                _ => null
            };
            LatitudeDeliverer = latitudeDeliverer;
            LongitudeDeliverer = longitudeDeliverer;
            UrlsEvidences = urlsEvidences;
            UrlVoucher = urlVoucher;
            DelivererId = delivererId;
            OrderId = orderId;
        }
    }
}
