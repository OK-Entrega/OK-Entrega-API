using Commom.Commands;
using System;
using System.Collections.Generic;

namespace Domains.Commands.Requests.OrderRequests
{
    public class PrintOrdersRequest : CommandRequest
    {
        public ICollection<Guid> OrdersIds { get; set; }
    }
}
