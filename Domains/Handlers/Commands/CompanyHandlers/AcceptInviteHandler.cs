using Commom.Commands;
using Commom.Enum;
using Domains.Commands.Requests.CompanyRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.CompanyHandlers
{
    public class AcceptInviteHandler : IRequestHandler<AcceptInviteRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public AcceptInviteHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericCommandResult> Handle(AcceptInviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if(company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Esta empresa não existe!", null));

                if(company.CompanyHasShippers.Any(s => s.ShipperId == request.ShipperId))
                    return Task.FromResult(new GenericCommandResult(400, "Você já está nesta empresa!", null));

                var shipperCompany = new ShipperCompany(request.ShipperId, request.CompanyId, EnShipperRole.Normal);

                _companyRepository.CreateShipperCompany(shipperCompany);

                return Task.FromResult(new GenericCommandResult(200, $"Convite aceito com sucesso! Seja bem vindo à {company.Name}!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
