using Commom.Commands;
using Commom.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domains.Commands.Requests.OrderRequests
{
    public class FinishOrderRequest : CommandRequest
    {
        public EnFinishType FinishType { get; set; }
        public DateTime FinishedAt { get; set; }
        public EnReasonDevolution? ReasonDevolution { get; set; }
        public Guid UserId { get; set; }
        public string AccessKey { get; set; }
        public ICollection<IFormFile> Evidences { get; set; }
        public IFormFile Voucher { get; set; }
        public decimal LatitudeDeliverer { get; set; }
        public decimal LongitudeDeliverer { get; set; }
    }
}
