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
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericCommandResult> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if (company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa empresa não existe!", null));

                if (!company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId && s.ShipperRole == EnShipperRole.Creator))
                    return Task.FromResult(new GenericCommandResult(401, "Você não tem permissão para deletar essa empresa!", null));

                _companyRepository.Delete(company);

                return Task.FromResult(new GenericCommandResult(200, $"{company.Name} deletada com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
