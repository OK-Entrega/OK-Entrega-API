using Commom.Entities;
using Commom.Enum;
using System;

namespace Domains.Entities
{
    public class OccurrenceOrder : Entity
    {
        public EnReasonOccurrence ReasonOccurrence { get; private set; }
        public Deliverer Deliverer { get; private set; }
        public Guid DelivererId { get; private set; }
        public Order Order { get; private set; }
        public Guid OrderId { get; private set; }

        public OccurrenceOrder(
            EnReasonOccurrence reasonOccurrence,
            Guid delivererId,
            Guid orderId
        )
        {
            ReasonOccurrence = reasonOccurrence;
            DelivererId = delivererId;
            OrderId = orderId;
        }
    }
}
