using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetDetailsRequest : QueryRequest
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
