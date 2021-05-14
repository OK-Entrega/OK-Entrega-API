using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Company
{
    public class AddShipperRequest : Notifiable<Notification>, ICommand
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public AddShipperRequest(string email, Guid companyId)
        {
            Email = email.Trim().ToLower();
            CompanyId = companyId;
        }
        public void Validate()
        {
            AddNotifications(new Contract<AddShipperRequest>()
            .Requires()
            .IsEmail(Email, "Email", "Email inválido!")
            );
        }
    }
}
