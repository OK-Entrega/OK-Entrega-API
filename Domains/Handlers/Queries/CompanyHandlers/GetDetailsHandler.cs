using Commom.Queries;
using Commom.Services;
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
    public class GetDetailsHandler : IRequestHandler<GetDetailsRequest, GenericQueryResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetDetailsHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericQueryResult> Handle(GetDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if (company == null)
                    return Task.FromResult(new GenericQueryResult(404, "Nenhuma empresa encontrada!", null));

                var shipperRole = company.CompanyHasShippers.FirstOrDefault(c => c.Shipper.UserId == request.UserId).ShipperRole;

                var result = new GetDetailsResponse(company.Name, company.CNPJ, company.UrlLogo, company.Segment, shipperRole);

                return Task.FromResult(new GenericQueryResult(200, null, result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericQueryResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
