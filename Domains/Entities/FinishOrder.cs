using Commom.Entities;
using Commom.Enum;
using System;

namespace Domains.Entities
{
    public class FinishOrder : Entity
    {
        public string UrlsEvidences { get; private set; }
        public EnFinishType FinishType { get; private set; }
        public EnReasonDevolution? ReasonDevolution { get; private set; }
        public Guid DelivererId { get; private set; }
        public Deliverer Deliverer { get; private set; }
        public decimal LatitudeDeliverer { get; private set; }
        public decimal LongitudeDeliverer { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
        public Voucher Voucher { get; private set; }

        public FinishOrder(){}

        public FinishOrder(
            string urlsEvidences,
            EnReasonDevolution? reasonDevolution,
            Guid delivererId,
            decimal latitudeDeliverer,
            decimal longitudeDeliverer,
            DateTime finishedAt,
            Guid orderId,
            EnFinishType finishType = EnFinishType.Success,
            Voucher voucher = null
        )
        {
            FinishType = finishType;
            ReasonDevolution = finishType switch
            {
                EnFinishType.Devolution => reasonDevolution,
                _ => null
            };
            Voucher = finishType switch
            {
                EnFinishType.Success => voucher,
                _ => null
            };
            FinishedAt = finishedAt;
            LatitudeDeliverer = latitudeDeliverer;
            LongitudeDeliverer = longitudeDeliverer;
            UrlsEvidences = urlsEvidences;
            DelivererId = delivererId;
            OrderId = orderId;
        }
    }
}
