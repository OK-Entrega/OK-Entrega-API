using Commom.Commands;
using Flunt.Validations;
using System;
using System.Text.Json.Serialization;

namespace Domains.Commands.Requests.CompanyRequests
{
    public class InviteShipperRequest : CommandRequest
    {
        public string Email { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public InviteShipperRequest(
            string email,
            Guid userId,
            Guid companyId
        )
        {
            Email = email.Trim().ToLower();
            UserId = userId;
            CompanyId = companyId; 
        }

        public override void Validate()
        {
            AddNotifications(new Contract<ChangeCompanyRequest>()
                .Requires()
                .IsEmail(Email, "Email", "O email do embarcador é inválido!")
            );
        }
    }
}
