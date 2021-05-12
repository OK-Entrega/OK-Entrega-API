using Commom.Commands;
using Flunt.Validations;
using System;

namespace Domains.Commands.Requests.ShipperRequests
{
    public class CreateShipperRequest : CommandRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateShipperRequest(
            string name,
            string email,
            string password
        )
        {
            Name = name.Trim();
            Email = email.Trim().ToLower();
            Password = password.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<CreateShipperRequest>()
                .Requires()
                .IsTrue((Name.Length > 2) && (Name.Length < 41), "Nome", "O nome deve ter de 3 a 40 caracteres!")
                .IsEmail(Email, "Email", "Email inválido!")
                .IsTrue((Password.Length > 5) && (Password.Length < 21), "Senha", "A senha deve ter de 6 a 20 caracteres!")
            );
        }
    }
}
