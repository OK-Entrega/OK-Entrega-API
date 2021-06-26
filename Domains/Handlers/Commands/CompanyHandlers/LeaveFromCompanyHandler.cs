using Commom.Commands;
using Commom.Enum;
using Commom.Services;
using Domains.Commands.Requests.CompanyRequests;
using Domains.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.CompanyHandlers
{
    public class LeaveFromCompanyHandler : IRequestHandler<LeaveFromCompanyRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;

        public LeaveFromCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<GenericCommandResult> Handle(LeaveFromCompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                bool grant = false;
                string name = "";

                var company = _companyRepository.Search(request.CompanyId);

                if (company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa empresa não existe!", null));

                if (company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId && s.ShipperRole == EnShipperRole.Creator))
                {
                    if (company.CompanyHasShippers.Count > 1)
                    {
                        var newCreator = company.CompanyHasShippers.OrderBy(c => c.CreatedAt).FirstOrDefault();
                        newCreator.ChangeShipperRole(EnShipperRole.Creator);
                        grant = true;
                        name = newCreator.Shipper.User.Name;
                        //MessageServices.SendEmail(newCreator.Email, $"Promovido na empresa {company.Name}!", $"<p style='color: black; font-weight: bold'>Olá, {newCreator.User.Name}!<br> Parabéns! Você foi promovido para Criador da empresa {company.Name}</p>");
                    }
                    else
                    {
                        _companyRepository.Delete(company);
                        return Task.FromResult(new GenericCommandResult(200, "Como você era o único integrante da empresa e decidiu sair, apagamos ela de nossa base!", null));
                    }
                }

                var shipperCompany = company.CompanyHasShippers.FirstOrDefault(cs => cs.Shipper.UserId == request.UserId);
                company.CompanyHasShippers.Remove(shipperCompany);
                _companyRepository.Update(company);
                if(grant)
                    return Task.FromResult(new GenericCommandResult(200, $"Você saiu da empresa {company.Name} com sucesso e agora {name} tem o privilégio de criador dela!", null));
                return Task.FromResult(new GenericCommandResult(200, $"Você saiu da empresa {company.Name} com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
