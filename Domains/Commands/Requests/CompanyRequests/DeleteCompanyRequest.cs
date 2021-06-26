using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class DeleteCompanyRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string CNPJ { get; set; }

        public override void Validate()
        {
            CNPJ = CNPJ.Trim().Replace("-", "").Replace(".", "").Replace("/", "");

            AddNotifications(new Contract<DeleteCompanyRequest>()
                .Requires()
                .IsTrue(CNPJ.Length == 14, "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}
