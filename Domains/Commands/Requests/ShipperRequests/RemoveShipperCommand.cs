using Commom.Commands;
using System;

namespace Domains.Commands.Requests.ShipperRequests
{
    public class RemoveShipperRequest : CommandRequest
    {
        public Guid ShipperId { get; set; }
        public override void Validate(){}
    }
}
