using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Commands.Requests.DelivererRequests
{
    public class DeleteDelivererCommand
    {
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Password { get; set; }

        public void Validate() { }
    }
}
