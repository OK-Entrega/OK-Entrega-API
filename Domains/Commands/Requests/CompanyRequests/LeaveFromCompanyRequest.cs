using Commom.Commands;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class LeaveFromCompanyRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ShipperId { get; set; }

        public override void Validate(){}
    }
}
