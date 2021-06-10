using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetMyCompaniesRequest : QueryRequest
    {
        public Guid UserId { get; set; }
    }
}
