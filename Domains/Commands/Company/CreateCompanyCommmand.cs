using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Company
{
    public class CreateCompanyCommmand : Notifiable<Notification>, ICommand
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public Guid UserId { get; set; }

        public CreateCompanyCommmand(string name, string cnpj)
        {
            Name = name.Trim();
            CNPJ = cnpj.Trim();
        }
        public void Validate()
        {
            AddNotifications(new Contract<CreateCompanyCommmand>()
                 .Requires()
                 .IsTrue((Name.Length > 2), "Name", "O nome da empresa deve ter ao menos 3 caracteres!")
                 .IsTrue((CNPJ.Length < 15), "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}
