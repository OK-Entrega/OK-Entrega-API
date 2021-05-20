using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Company
{
    public class AddShipperCommand : Notifiable<Notification>, ICommand
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        public AddShipperCommand(string email, Guid companyId)
        {
            Email = email.Trim().ToLower();
            CompanyId = companyId;
        }
        public void Validate()
        {
            AddNotifications(new Contract<AddShipperCommand>()
            .Requires()
            .IsEmail(Email, "Email", "Email inválido!")
            );
        }
    }
}
