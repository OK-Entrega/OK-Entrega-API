using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetDashboardDataRequest : QueryRequest
    {
        public Guid CompanyId { get; set; }
        public int? Month { get; set; }
        public int Year { get; set; }
    }
}
