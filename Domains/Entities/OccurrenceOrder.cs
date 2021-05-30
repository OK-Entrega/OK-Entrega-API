using Commom.Entities;
using Commom.Enum;
using System;

namespace Domains.Entities
{
    public class OccurrenceOrder : Entity
    {
        public EnReasonOccurrence ReasonOccurrence { get; private set; }
        public Guid DelivererId { get; private set; }
        public Deliverer Deliverer { get; private set; }
        public decimal LatitudeDeliverer { get; private set; }
        public decimal LongitudeDeliverer { get; private set; }
        public string UrlsEvidences { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }

        public OccurrenceOrder(
            EnReasonOccurrence reasonOccurrence,
            Guid delivererId,
            decimal latitudeDeliverer,
            decimal longitudeDeliverer,
            Guid orderId,
            string urlsEvidences = null
        )
        {
            ReasonOccurrence = reasonOccurrence;
            DelivererId = delivererId;
            LatitudeDeliverer = latitudeDeliverer;
            LongitudeDeliverer = longitudeDeliverer;
            OrderId = orderId;
            UrlsEvidences = urlsEvidences;
        }
    }
}
