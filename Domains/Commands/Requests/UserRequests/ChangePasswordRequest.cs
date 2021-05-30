using Commom.Commands;
using Commom.Services;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class ChangePasswordRequest : CommandRequest
    {
        [JsonIgnore]
        public string Discriminator { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordRequest(
            Guid userId,
            string currentPassword,
            string newPassword
        )
        {
            UserId = userId;
            CurrentPassword = currentPassword.Trim();
            NewPassword = newPassword.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangePasswordRequest>()
                .Requires()
                .IsTrue((NewPassword.Length > 7 && NewPassword.Length < 21), "Nova senha", "A nova senha deve ter entre 8 à 20 caracteres!")
            );

            if (Discriminator == "Shipper")
                AddNotifications(new Contract<ChangePasswordForgottenRequest>()
                    .Requires()
                    .IsTrue(PasswordServices.ShipperPasswordIsValid(NewPassword), "Nova senha", "A nova senha deve ter letras maiúsculas, minúsculas e números!")
                );
        }
    }
}
