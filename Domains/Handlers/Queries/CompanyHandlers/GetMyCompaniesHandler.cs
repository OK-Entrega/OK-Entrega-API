using Commom.Queries;
using Domains.Queries.Requests.CompanyRequests;
using Domains.Queries.Responses.CompanyResponses;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Queries.CompanyHandlers
{
    public class GetMyCompaniesHandler : IRequestHandler<GetMyCompaniesRequest, GenericQueryResult>
    {
        private ICompanyRepository _companyRepository { get; set; }

        public GetMyCompaniesHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericQueryResult> Handle(GetMyCompaniesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var companies = _companyRepository.Read(request.UserId);

                var result = companies.Select(c => new GetMyCompaniesResponse(
                    c.Id,
                    c.Name,
                    c.Segment,
                    c.UrlLogo
                ))
                .OrderBy(c => c.Name)
                .ToList();

                if (result == null || result?.Count < 1)
                    return Task.FromResult(new GenericQueryResult(404, null, null));

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
