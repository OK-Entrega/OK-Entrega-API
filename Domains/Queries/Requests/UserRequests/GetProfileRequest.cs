using Commom.Queries;
using System;
using System.Text.Json.Serialization;

namespace Domains.Queries.Requests.UserRequests
{
    public class GetProfileRequest : QueryRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }
    }
}
