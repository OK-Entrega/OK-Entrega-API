using Commom.Commands;
using Commom.Enum;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class CreateCompanyRequest : CommandRequest
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public EnCompanySegment Segment { get; set; }
        public IFormFile Logo { get; set; }

        public CreateCompanyRequest(){}

        public CreateCompanyRequest(
            string name, 
            string cnpj,
            Guid userId,
            EnCompanySegment segment,
            IFormFile logo
        )
        {
            Name = name.Trim();
            CNPJ = cnpj.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            UserId = userId;
            Segment = segment;
            Logo = logo;
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangeCompanyRequest>()
                .Requires()
                .IsTrue((Name.Length > 2 && Name.Length < 41), "Nome", "O nome da empresa deve ter entre 3 à 40 caracteres!")
                .IsTrue(CNPJ.Length == 14, "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}
