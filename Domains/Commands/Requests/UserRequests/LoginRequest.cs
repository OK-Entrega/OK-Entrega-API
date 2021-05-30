using Commom.Commands;
using Commom.Services;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class LoginRequest : CommandRequest
    {
        public string CellphoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }

        public LoginRequest(
            string cellphoneNumber,
            string email,
            string password
        )
        {
            Email = email?.Trim().ToLower();
            CellphoneNumber = cellphoneNumber?.Trim();
            Password = password.Trim();

            if (Email != null && CellphoneNumber != null)
                Discriminator = null;
            else if (Email != null)
                Discriminator = "Shipper";
            else
                Discriminator = "Deliverer";
        }

        public override void Validate()
        {
            AddNotifications(new Contract<LoginRequest>()
                .Requires()
                .IsTrue((Password.Length > 7 && Password.Length < 21), "Senha", "A senha deve ter entre 8 à 20 caracteres!")
            );

            if (Discriminator == "Shipper")
                AddNotifications(new Contract<LoginRequest>()
                    .Requires()
                    .IsEmail(Email, "Email", "Email inválido!")
                    .IsTrue(PasswordServices.ShipperPasswordIsValid(Password), "Senha", "A senha deve ter letras maiúsculas, minúsculas e números!")
                );
            else if (Discriminator == "Deliverer")
                AddNotifications(new Contract<LoginRequest>()
                    .Requires()
                    .IsTrue(CellphoneNumber.Length == 11, "Número de telefone celular", "O número de telefone celular deve conter 11 dígitos!")
                );
        }
    }
}
