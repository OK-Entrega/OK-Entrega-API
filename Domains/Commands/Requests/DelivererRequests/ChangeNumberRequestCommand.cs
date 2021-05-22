using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Domains.Commands.Requests.DelivererRequests
{
    public class ChangeNumberRequestCommand : Notifiable<Notification>, ICommand
    {
        public string Number { get; set; }

        public ChangeNumberRequestCommand(string email)
        {
            Number = Number.Trim().ToLower();
        }

        public void Validate()
        {
            AddNotifications(new Contract<ChangeNumberRequestCommand>()
              .IsTrue(Number.Length == 11, "Número de telefone celular", "Número de telefone celular inválido!")
            );
        }

        
    }
}
