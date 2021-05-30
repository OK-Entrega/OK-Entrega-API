using Commom.Commands;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class RequestNewPasswordRequest : CommandRequest
    {
        public string CellphoneNumber { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }

        public RequestNewPasswordRequest(
            string cellphoneNumber,
            string email
        )
        {
            CellphoneNumber = cellphoneNumber?.Trim();
            Email = email?.Trim().ToLower();

            if (Email != null && CellphoneNumber != null)
                Discriminator = null;
            else if (Email != null)
                Discriminator = "Shipper";
            else
                Discriminator = "Deliverer";
        }

        public override void Validate()
        {
            if (Discriminator == "Shipper")
                AddNotifications(new Contract<RequestNewPasswordRequest>()
                    .Requires()
                    .IsEmail(Email, "Email", "Email inválido!")
                );

            else if (Discriminator == "Deliverer")
                AddNotifications(new Contract<RequestNewPasswordRequest>()
                    .Requires()
                    .IsTrue(CellphoneNumber.Length == 11, "Número de telefone celular", "O número de telefone celular deve conter 11 dígitos!")
                );
        }
    }
}
