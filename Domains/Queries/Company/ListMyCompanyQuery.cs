using Commom.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Queries.Company
{
    public class ListMyCompanyQuery : IQuery
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null;

        public ListMyCompanyQuery(string name = null)
        {
            if (name != null)
                Name = name.Trim().ToLower();
        }

        public void Validate(){ }
    }
}
