using Commom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Commands.Shipper
{
    public class RemoveShipperCommand : ICommand
    {
        public Guid ShipperId { get; set; }
        public void Validate(){}
    }
}
