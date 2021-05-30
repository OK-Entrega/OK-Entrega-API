using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class JoinWithCodeRequest : CommandRequest
    {
        public string Code { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }

        public JoinWithCodeRequest(
            string code,
            Guid userId
        )
        {
            Code = code.Trim();
            UserId = userId;
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangeCompanyRequest>()
                .Requires()
                .IsTrue(Code.Length == 12, "Código da empresa", "O código da empresa deve conter 12 caracteres!")
            );
        }
    }
}
