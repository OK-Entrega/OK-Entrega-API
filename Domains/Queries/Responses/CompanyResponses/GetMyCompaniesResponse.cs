using Commom.Enum;
using Commom.Services;
using System;

namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetMyCompaniesResponse 
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Segment { get; set; }
        public string UrlLogo { get; set; }

        public GetMyCompaniesResponse(
            Guid companyId,
            string name, 
            EnCompanySegment segment,
            string urlLogo
        )
        {
            CompanyId = companyId;
            Name = name;
            Segment = EnumServices.GetDescription(segment);
            UrlLogo = urlLogo;
        }
    }
}
