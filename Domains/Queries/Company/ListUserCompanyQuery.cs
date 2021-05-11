using Commom.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Queries.Company
{
    public class ListUserCompanyQuery : IQuery
    {
        public Guid CompanyId { get; set; }
        public void Validate(){}
    }
}
