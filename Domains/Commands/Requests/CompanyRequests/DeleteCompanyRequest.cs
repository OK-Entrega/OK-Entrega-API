﻿using Commom.Commands;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class DeleteCompanyRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public override void Validate(){}
    }
}
