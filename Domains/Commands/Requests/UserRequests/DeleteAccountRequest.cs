using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class DeleteAccountRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public DeleteAccountRequest(
            Guid userId,
            string password
        )
        {
            UserId = userId;
            Password = password.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<DeleteAccountRequest>()
                .Requires()
                .IsTrue((Password.Length > 7 && Password.Length < 21), "Senha", "A senha deve ter entre 8 à 20 caracteres!")
            );
        }
    }
}
