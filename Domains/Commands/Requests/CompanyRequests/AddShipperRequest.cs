using Commom.Commands;
using Flunt.Validations;
using System;

namespace Domains.Commands.Requests.Company
{
    public class AddShipperRequest : CommandRequest
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public AddShipperRequest(string email, Guid companyId)
        {
            Email = email.Trim().ToLower();
            CompanyId = companyId;
        }
        public override void Validate()
        {
            AddNotifications(new Contract<AddShipperRequest>()
                .Requires()
                .IsEmail(Email, "Email", "Email inválido!")
            );
        }
    }
}
