using Commom.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Queries.Company
{
    public class ListCompanyDetailsQuery : IQuery
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public void Validate(){}
        public ListCompanyDetailsQuery(string name = null)
        {
            if (name != null)
            {
                Name = name.Trim().ToLower();
            }
        }
    }
}
