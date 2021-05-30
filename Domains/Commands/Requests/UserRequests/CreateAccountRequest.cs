using Commom.Commands;
using Commom.Services;
using Flunt.Validations;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class CreateAccountRequest : CommandRequest
    {
        public string Name { get; set; }
        public string CellphoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Discriminator { get; set; }

        public CreateAccountRequest(
            string name, 
            string cellphoneNumber, 
            string password, 
            string email
        )
        {
            Name = name.Trim();
            CellphoneNumber = cellphoneNumber?.Trim().Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); 
            Password = password.Trim();
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
            AddNotifications(new Contract<CreateAccountRequest>()
                .Requires()
                .IsTrue((Name.Length > 2 && Name.Length < 41), "Nome", "O nome deve ter entre 3 à 40 caracteres!")
                .IsTrue((Password.Length > 7 && Password.Length < 21), "Senha", "A senha deve ter entre 8 à 20 caracteres!")
            );

            if (Discriminator == "Shipper")
                AddNotifications(new Contract<CreateAccountRequest>()
                    .Requires()
                    .IsEmail(Email, "Email", "Email inválido!")
                    .IsTrue(PasswordServices.ShipperPasswordIsValid(Password), "Senha", "A senha deve ter letras maiúsculas, minúsculas e números!")
                );
            else if (Discriminator == "Deliverer")
                AddNotifications(new Contract<CreateAccountRequest>()
                    .Requires()
                    .IsTrue(CellphoneNumber.Length == 11, "Número de telefone celular", "O número de telefone celular deve conter 11 dígitos!")
                );
        }
    }
}
