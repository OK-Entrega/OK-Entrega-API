using System;

namespace Domains.Queries.Responses.CompanyResponses
{
    public class GetShippersResponse
    {
        public Guid Id { get; set; }
        public string ShipperRole { get; set; }
        public string Name { get; set; }
        public string JoinedAt { get; set; }

        public GetShippersResponse(
            Guid id,
            string shipperRole,
            string name,
            string joinedAt
        )
        {
            Id = id;
            ShipperRole = shipperRole;
            Name = name;
            JoinedAt = joinedAt;
        }
    }
}
