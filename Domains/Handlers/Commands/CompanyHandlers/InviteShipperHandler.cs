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
    public class InviteShipperHandler : IRequestHandler<InviteShipperRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IShipperRepository _shipperRepository;

        public InviteShipperHandler(ICompanyRepository companyRepository, IShipperRepository shipperRepository)
        {
            _companyRepository = companyRepository;
            _shipperRepository = shipperRepository;
        }

        public Task<GenericCommandResult> Handle(InviteShipperRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var company = _companyRepository.Search(request.CompanyId);

                if(company == null)
                    return Task.FromResult(new GenericCommandResult(400, "Essa empresa não existe!", null));

                if(_shipperRepository.Search(request.Email) == null)
                    return Task.FromResult(new GenericCommandResult(400, "Não existe ninguém cadastrado com esse email!", null));

                if (company.CompanyHasShippers.Any(s => s.Shipper.UserId == request.UserId && s.Shipper.Email == request.Email))
                    return Task.FromResult(new GenericCommandResult(400, "Esse email é seu!", null));

                if (company.CompanyHasShippers.Any(s => s.Shipper.Email == request.Email))
                    return Task.FromResult(new GenericCommandResult(400, "Já existe um embarcador com esse email nessa empresa!", null));

                var shipper = _shipperRepository.Search(request.Email);

                MessageServices.SendEmail(request.Email, $"Convite para entrar na {company.Name}!", $"<p style='color: black; font-weight: bold'>Olá, {shipper.User.Name}!<br> Você recebeu um convite para entrar na empresa {company.Name}! Clique no botão abaixo para aceitar.</p><br><a href='https://www.customvision.ai/projects/71e376d6-7942-450d-80d6-de9f8ac18b35#/manage'><button style='display: block; margin: auto; border-color: #2ecc71; background: #2ecc71; color: white; font-weight: bold; text-decoration: none; cursor: pointer; box-shadow: none'>Entrar</button></a>");

                return Task.FromResult(new GenericCommandResult(200, $"Um convite para {shipper.User.Name} foi enviado por email com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }
    }
}
