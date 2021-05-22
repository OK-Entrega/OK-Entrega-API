using Commom.Queries;
using Domains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries
{
    /*public class ListProfileQueryHandler : IHandlerQuery<ListProfileQuery>
    {
        private IDelivererRepository Repository { get; set; }

        public ListProfileQueryHandler(IDelivererRepository repository)
        {
            Repository = repository;
        }

        public IQueryResult Handle(ListProfileQuery query)
        {
            var deliverer = Repository.Search(query.UserId);

            var result = new ListProfileQueryResult(deliverer.Id, deliverer.Name, deliverer.CellphoneNumber, deliverer.Password);

            return new GenericQueryResult(true, "Seu perfil!", result);
        }
    }*/
}
