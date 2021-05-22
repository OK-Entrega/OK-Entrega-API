using Commom.Commands;
using System;

namespace Domains.Commands.Requests.Company
{
    public class RemoveShipperCompanyRequest : CommandRequest
    {
        public Guid CompanyId { get; set; }
        public Guid ShipperCompanyId { get; set; }
        public override void Validate() {}
    }
}
