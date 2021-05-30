using Commom.Commands;
using Commom.Enum;
using Commom.Services;
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
    public class RemoveShipperHandler : IRequestHandler<RemoveShipperRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IShipperRepository _shipperRepository;

        public RemoveShipperHandler(ICompanyRepository companyRepository, IShipperRepository shipperRepository)
        {
            _companyRepository = companyRepository;
            _shipperRepository = shipperRepository;
        }

        public Task<GenericCommandResult> Handle(RemoveShipperRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if(company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa empresa não existe!", null));

                if (!company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId && s.ShipperRole == EnShipperRole.Creator))
                    return Task.FromResult(new GenericCommandResult(401, "Você não tem permissão para expulsar ninguém dessa empresa!", null));

                if (!company.CompanyHasShippers.Any(s => s.ShipperId == request.ShipperId))
                    return Task.FromResult(new GenericCommandResult(400, "Esse embarcador não pertence a essa empresa!", null));

                var shipper = _shipperRepository.Search(request.ShipperId);
                var shipperCompany = company.CompanyHasShippers.FirstOrDefault(s => s.ShipperId == request.ShipperId);

                company.CompanyHasShippers.Remove(shipperCompany);

                _companyRepository.Update(company);

                MessageServices.SendEmail(shipper.Email, $"Removido da empresa {company.Name}!", $"<p style='color: black; font-weight: bold'>Olá, {shipper.User.Name}!<br> Infelizmente, decidiram remover você da empresa {company.Name}. Achamos que seria melhor avisar!</p>");

                return Task.FromResult(new GenericCommandResult(200, $"{shipper.User.Name} expulso com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
