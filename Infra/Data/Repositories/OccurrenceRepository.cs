using Domains.Entities;
using Domains.Repositories;
using Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Infra.Data.Repositories
{
    public class OccurrenceRepository : IOccurrenceRepository
    {
        private readonly DataContext _context;

        public OccurrenceRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<OccurrenceOrder> Read(Guid companyId)
        {
            return
                _context
                .Occurrences
                .Include(o => o.Order)
                .Include(o => o.Deliverer).ThenInclude(d => d.User)
                .Where(o => o.Order.CompanyId == companyId);
        }
    }
}
