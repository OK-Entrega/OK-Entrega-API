using Commom.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domains.Commands.Requests.OrderRequests
{
    public class CreateOrdersRequest : CommandRequest
    {
        public List<IFormFile> Files { get; set; }
        public Guid CompanyId { get; set; }
    }
}
