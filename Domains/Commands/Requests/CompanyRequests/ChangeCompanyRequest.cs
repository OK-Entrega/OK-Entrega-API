using Commom.Commands;
using Commom.Enum;
using Flunt.Validations;
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

        public ChangeCompanyRequest(
            string name, 
            string cnpj,
            EnCompanySegment segment
        )
        {
            Name = name.Trim();
            CNPJ = cnpj.Trim().Replace("-", "").Replace(".", "").Replace("/", "");
            Segment = segment;
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
