using Commom.Commands;
using Flunt.Validations;
using System;

namespace Domains.Commands.Requests.UserRequests
{
    public class CreateShipperRequest : CommandRequest
    {
        int n;

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellphoneNumber { get; set; } = null;

        public CreateShipperRequest(
            string name,
            string email,
            string password,
            string cellphoneNumber = null
        )
        {
            Name = name.Trim();
            Email = email.Trim().ToLower();
            Password = password.Trim();
            if(CellphoneNumber == null)
            {
                CellphoneNumber = cellphoneNumber.Trim();
                if (!Int32.TryParse(CellphoneNumber, out n))
                    CellphoneNumber = "1";
            }
        }

        public void Validar()
        {
            AddNotifications(new Contract<CreateShipperRequest>()
                .Requires()
                .IsTrue((Name.Length > 2) && (Name.Length < 41), "Nome", "O nome deve ter de 3 a 40 caracteres!")
                .IsEmail(Email, "Email", "Email inválido!")
                .IsTrue((Password.Length > 5) && (Password.Length < 21), "Senha", "A senha deve ter de 6 a 20 caracteres!")
                .IsFalse(CellphoneNumber.Length == 11, "Número de telefone celular", "Número de celular inválido!")
            );
        }
    }
}
