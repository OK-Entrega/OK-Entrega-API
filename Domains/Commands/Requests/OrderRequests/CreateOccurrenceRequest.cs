using Commom.Commands;
using Commom.Enum;
using System;

namespace Domains.Commands.Requests.OrderRequests
{
    public class CreateOccurrenceRequest : CommandRequest
    {
        public EnReasonOccurrence ReasonOccurrence { get; set; }
        public Guid UserId { get; set; }
        public decimal LatitudeDeliverer { get; set; }
        public decimal LongitudeDeliverer { get; set; }
        public string AccessKey { get; set; }
        public string UrlsEvidences { get; set; }
    }
}
