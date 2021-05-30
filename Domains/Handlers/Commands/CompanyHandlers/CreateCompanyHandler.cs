using Commom.Commands;
using Commom.Enum;
using Domains.Commands.Requests.CompanyRequests;
using Domains.Entities;
using Domains.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Handlers.Commands.CompanyHandlers
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyRequest, GenericCommandResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public Task<GenericCommandResult> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (_companyRepository.Search(request.CNPJ) != null)
                    return Task.FromResult(new GenericCommandResult(400, "Já existe uma empresa cadastrada com esse CNPJ!", null));

                var user = _userRepository.Search(request.UserId);

                var code = GenerateCode();

                while (_companyRepository.SearchByCode(code) != null)
                    code = GenerateCode();

                var company = new Company(request.Name, request.CNPJ, code, request.Segment);
                var shipperCompany = new ShipperCompany(user.Shipper.Id, company.Id, EnShipperRole.Creator);
                company.CompanyHasShippers.Add(shipperCompany);


                _companyRepository.Create(company);

                return Task.FromResult(new GenericCommandResult(200, $"Empresa {company.Name} criada com sucesso!", null));
            }
            catch (Exception ex)
            {
                return Task.FromResult(new GenericCommandResult(500, ex.Message, ex.InnerException));
            }
        }

        private static string GenerateCode()
        {
            string caracters = "abcdefghijklmnopqrstuvwxyz";
            string code = "";

            Random random = new Random();

            for (int c = 0; c < 10; c++)
            {
                if (c == 3 || c == 7)
                    code += "-";
                code += caracters.Substring(random.Next(0, caracters.Length - 1), 1);
            }

            return code;
        }
    }
}
