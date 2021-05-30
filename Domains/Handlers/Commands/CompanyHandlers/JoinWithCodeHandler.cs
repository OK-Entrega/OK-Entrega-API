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
    public class JoinWithCodeHandler : IRequestHandler<JoinWithCodeRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public JoinWithCodeHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(JoinWithCodeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.SearchByCode(request.Code);

                if (company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Não existe nenhuma empresa com o código informado!", null));

                if (company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId))
                    return Task.FromResult(new GenericCommandResult(400, "Você já está nesta empresa!", null));

                var user = _userRepository.Search(request.UserId);

                var shipperCompany = new ShipperCompany(user.Shipper.Id, company.Id, EnShipperRole.Normal);

                _companyRepository.CreateShipperCompany(shipperCompany);

                return Task.FromResult(new GenericCommandResult(200, $"Seja bem vindo à {company.Name}!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
