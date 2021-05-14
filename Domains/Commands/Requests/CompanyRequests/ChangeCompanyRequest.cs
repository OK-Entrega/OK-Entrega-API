using Commom.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Domains.Commands.Company
{
    public class ChangeCompanyRequest : Notifiable<Notification>, ICommand
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public ChangeCompanyRequest(string name, string cnpj)
        {
            Name = name.Trim();
            CNPJ = cnpj.Trim();
        }
        public void Validate()
        {
            AddNotifications(new Contract<ChangeCompanyRequest>()
                 .Requires()
                 .IsTrue((Name.Length > 2), "Name", "O nome da empresa deve ter ao menos 3 caracteres!")
                 .IsTrue((CNPJ.Length < 15), "CNPJ", "O CNPJ da empresa deve conter 14 caracteres!")
            );
        }
    }
}
