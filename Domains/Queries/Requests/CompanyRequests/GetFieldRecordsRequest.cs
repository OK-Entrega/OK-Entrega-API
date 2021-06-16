using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetFieldRecordsRequest : QueryRequest
    {
        public Guid CompanyId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string DelivererName { get; set; }
    }
}
