using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.UserRequests
{
    public class ChangeUserRequest : CommandRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public ChangeUserRequest(
            Guid userId,
            string name
        )
        {
            UserId = userId;
            Name = name.Trim();
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangeUserRequest>()
                .Requires()
                .IsTrue((Name.Length > 2 && Name.Length < 41), "Nome", "O nome deve ter entre 3 à 40 caracteres!")
            );
        }
    }
}
