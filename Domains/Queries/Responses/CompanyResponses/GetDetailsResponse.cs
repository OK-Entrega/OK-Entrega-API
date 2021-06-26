using Commom.Enum;

namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetDetailsResponse
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string UrlLogo { get; set; }
        public EnCompanySegment Segment { get; set; }
        public EnShipperRole MyRole { get; set; }

        public GetDetailsResponse(
            string name, 
            string cnpj,
            string urlLogo,
            EnCompanySegment segment,
            EnShipperRole myRole
        )
        {
            Name = name;
            CNPJ = cnpj;
            UrlLogo = urlLogo;
            Segment = segment;
            MyRole = myRole;
        }
    }
}
