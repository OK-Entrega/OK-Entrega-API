using Domains.Entities;
using System;
using System.Linq;

namespace Domains.Repositories
{
    public interface IOccurrenceRepository
    {
        IQueryable<OccurrenceOrder> Read(Guid companyId);
    }
}
