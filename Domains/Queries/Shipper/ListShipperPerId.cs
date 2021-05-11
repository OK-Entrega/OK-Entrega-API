using Commom.Queries;
using System;

namespace Domains.Queries.Shipper
{
    public class ListShipperPerId : IQuery
    {
        public Guid ShipperId { get; set; }
        public void Validate()
        { }
    }
}
