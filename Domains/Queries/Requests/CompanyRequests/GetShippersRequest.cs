using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetShippersRequest : QueryRequest
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Page { get; set; } = 1;

        public GetShippersRequest(){}

        public GetShippersRequest(int page = 1)
        {
            Page = page <= 0 ? 1 : page;
        }
    }
}
