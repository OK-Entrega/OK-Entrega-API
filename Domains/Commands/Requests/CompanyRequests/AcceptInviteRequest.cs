using Commom.Commands;
using System;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class AcceptInviteRequest : CommandRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ShipperId { get; set; }

        public override void Validate(){}
    }
}
