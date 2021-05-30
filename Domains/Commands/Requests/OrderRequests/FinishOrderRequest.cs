using Commom.Commands;
using System;

namespace Domains.Commands.Requests.OrderRequests
{
    public class FinishOrderRequest : CommandRequest
    {
        public Guid UserId { get; set; }
        public string AccessKey { get; set; }
        public string UrlsEvidences { get; set; }
        public string UrlVoucher { get; set; }
    }
}
