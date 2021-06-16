using Commom.Queries;
using System;

namespace Domains.Queries.Requests.CompanyRequests
{
    public class GetGeoreferencingDataRequest : QueryRequest
    {
        public Guid CompanyId { get; set; }
        public DateTime? DateLessThen { get; set; }
        public DateTime? DateBiggerThen { get; set; }
        public string DelivererName { get; set; }
        public string Type { get; set; }
    }
}
