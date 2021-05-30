using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class RequestNewAccessRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }
        public string NewEmail { get; set; }
        public string NewCellphoneNumber { get; set; }

        public RequestNewAccessRequest(
            Guid userId,
            string newEmail,
            string newCellphoneNumber
        )
        {
            UserId = userId;
            NewEmail = newEmail?.Trim().ToLower();
            NewCellphoneNumber = newCellphoneNumber?.Trim().ToLower();

            if (NewEmail != null && NewCellphoneNumber != null)
                Discriminator = null;
        }

        public override void Validate()
        {
            if (Discriminator == "Shipper")
                AddNotifications(new Contract<RequestNewAccessRequest>()
                    .Requires()
                    .IsEmail(NewEmail, "Novo email", "Novo email inválido!")
                );

            else if (Discriminator == "Deliverer")
                AddNotifications(new Contract<RequestNewAccessRequest>()
                    .Requires()
                    .IsTrue(NewCellphoneNumber.Length == 11, "Novo número de telefone celular", "O novo número de telefone celular deve conter 11 dígitos!")
                );
        }
    }
}
