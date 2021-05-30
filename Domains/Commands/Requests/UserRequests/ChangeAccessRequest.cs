using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class ChangeAccessRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }

        public ChangeAccessRequest(
            Guid userId,
            string code
        )
        {
            UserId = userId;
            Code = code.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangeAccessRequest>()
                .Requires()
                .IsTrue((Code.Length == 4), "Código de verificação", "O código de verificação deve conter 4 caracteres!")
            );
        }
    }
}
