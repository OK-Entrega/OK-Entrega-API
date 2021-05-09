using Commom.Entities;
using Commom.Enum;
using System;

namespace Domains.Entities
{
    public class ShipperCompany : Entity
    {
        public Shipper Shipper { get; private set; }
        public Guid ShipperId { get; private set; }
        public Company Company { get; private set; }
        public Guid CompanyId { get; private set; }
        public EnShipperRole ShipperRole { get; private set; }

        public ShipperCompany(
            Guid shipperId,
            Guid companyId,
            EnShipperRole shipperRole
        )
        {
            ShipperId = shipperId;
            CompanyId = companyId;
            ShipperRole = shipperRole;
        }
    }
}
