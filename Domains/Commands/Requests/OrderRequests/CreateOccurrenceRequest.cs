using Commom.Commands;
using Commom.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.OrderRequests
{
    public class CreateOccurrenceRequest : CommandRequest
    {
        public EnReasonOccurrence ReasonOccurrence { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public decimal LatitudeDeliverer { get; set; }
        public decimal LongitudeDeliverer { get; set; }
        public string AccessKey { get; set; }
        public ICollection<IFormFile> Evidences { get; set; }
    }
}
