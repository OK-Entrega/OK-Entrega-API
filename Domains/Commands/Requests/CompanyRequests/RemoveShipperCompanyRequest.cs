using Commom.Commands;
using System;

namespace Domains.Commands.Company
{
    public class RemoveShipperCompanyRequest : ICommand
    {
        public Guid CompanyId { get; set; }
        public Guid ShipperCompanyId { get; set; }
        public void Validate() {}
    }
}
