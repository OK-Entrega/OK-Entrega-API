using Commom.Commands;
using Commom.Enum;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class ChangeCompanyRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public EnCompanySegment Segment { get; set; }
        public IFormFile Logo { get; set; }
        public bool DeleteLogo { get; set; }

        public ChangeCompanyRequest() { }

        public override void Validate()
        {
            CNPJ = CNPJ.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            Name = Name.Trim();

            AddNotifications(new Contract<ChangeCompanyRequest>()
                .Requires()
                .IsTrue((Name.Length > 2 && Name.Length < 41), "Nome", "O nome da empresa deve ter entre 3 à 40 caracteres!")
                .IsTrue(CNPJ.Length == 14, "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}
