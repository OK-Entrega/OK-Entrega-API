using Commom.Commands;
using Domains.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Shipper
{
    public class AddShipperCommand : Notifiable<Notification>, ICommand
    {
        public string Email { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

        public AddShipperCommand(string email, User user)
        {
            Email = email;
            User = user;
        }

        public void Validate()
        {
            AddNotifications(new Contract<AddShipperCommand>()
           .IsNotNullOrEmpty(Email, "Email", "Informe o email do embarcador")
           );
        }
    }
}
