using Commom.Commands;
using Commom.Services;
using Flunt.Validations;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Domains.Commands.Requests.UserRequests
{
    public class ChangePasswordForgottenRequest : CommandRequest
    {
        public JwtSecurityToken Token { get; set; }
        public string Password { get; set; }

        public ChangePasswordForgottenRequest(
            string token,
            string password
        )
        {
            Token = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Password = password.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangePasswordForgottenRequest>()
                .Requires()
                .IsTrue((Password.Length > 7 && Password.Length < 21), "Senha", "A senha deve ter entre 8 à 20 caracteres!")
            );

            if(Token.Claims.FirstOrDefault(c => c.Type == "discriminator").Value == "Shipper")
                AddNotifications(new Contract<ChangePasswordForgottenRequest>()
                    .Requires()
                    .IsTrue(PasswordServices.ShipperPasswordIsValid(Password), "Senha", "A senha deve ter letras maiúsculas, minúsculas e números!")
                );
        }
    }
}
