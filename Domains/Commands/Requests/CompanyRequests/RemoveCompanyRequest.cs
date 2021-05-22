using Commom.Commands;
using System;

namespace Domains.Commands.Requests.Company
{
    public class RemoveCompanyRequest : CommandRequest
    {
        public Guid CompanyId { get; set; }
        public override void Validate(){}
    }
}
