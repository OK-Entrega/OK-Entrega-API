using Commom.Commands;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Commands.Company
{
    public class RemoveCompanyRequest : Notifiable<Notification>, ICommand
    {
        public Guid CompanyId { get; set; }
        public void Validate(){}
    }
}
