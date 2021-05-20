using Commom.Commands;
using Flunt.Validations;

namespace Domains.Commands.Requests.DelivererRequests
{
    public class LoginDelivererRequest : CommandRequest
    {
        public string CellphoneNumber { get; set; }
        public string Password { get; set; }

        public LoginDelivererRequest(
            string cellphoneNumber,
            string password
        )
        {
            CellphoneNumber = cellphoneNumber.Trim();
            if (CellphoneNumber.Length != 11)
                CellphoneNumber = "1";
            Password = password.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<LoginDelivererRequest>()
                .Requires()
                .IsTrue((Password.Length > 5) && (Password.Length < 21), "Senha", "A senha deve ter de 6 a 20 caracteres!")
                .IsTrue(CellphoneNumber.Length == 11, "Número de telefone celular", "Número de telefone celular inválido!")
            );
        }
    }
}
