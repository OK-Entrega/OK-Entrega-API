using Commom.Commands;
using Commom.Enum;
using Domains.Commands.Requests.CompanyRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.CompanyHandlers
{
    public class ChangeCompanyHandler : IRequestHandler<ChangeCompanyRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public ChangeCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericCommandResult> Handle(ChangeCompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if (company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa empresa não existe!", null));

                if (!company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId && s.ShipperRole == EnShipperRole.Creator))
                    return Task.FromResult(new GenericCommandResult(401, "Você não tem permissão para alterar as informações dessa empresa!", null));

                company.ChangeCompany(request.Name, request.CNPJ, request.Segment);

                _companyRepository.Update(company);

                return Task.FromResult(new GenericCommandResult(200, $"Informações de {company.Name} atualizadas com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
