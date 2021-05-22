using Flunt.Notifications;
using Flunt.Validations;

namespace Domains.Entities
{
    public class Local : Notifiable<Notification>
    {
        public string CEP { get; private set; }
        public string Adress { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string UF { get; private set; }
        public string City { get; private set; }

        public Local(
            string cep,
            string adress,
            string number,
            string complement,
            string district,
            string uf,
            string city
        )
        {
            cep = cep.Trim().Replace("-", "");
            adress = adress.Trim();
            number = number.Trim();
            complement = complement.Trim();
            district = district.Trim();
            uf = uf.Trim();
            city = city.Trim();

            AddNotifications(new Contract<Local>()
                .Requires()
                .IsTrue(cep.Length == 8, "CEP", "O CEP deve conter 8 dígitos!")
                .IsTrue((adress.Length > 9 && adress.Length < 101), "Endereço", "O endereço deve ter entre 10 à 100 caracteres!")
                .IsTrue((district.Length > 2 && district.Length < 41), "Bairro", "O bairro deve ter entre 3 à 40 caracteres!")
                .IsTrue(uf.Length == 2, "UF", "A UF deve conter 2 caracteres!")
                .IsTrue((city.Length > 3 && city.Length < 71), "Cidade", "A cidade deve ter entre 4 à 70 caracteres!")
            );

            if (IsValid)
            {
                CEP = cep;
                Adress = adress;
                Number = number;
                Complement = complement;
                District = district;
                UF = uf;
                City = city;
            }
        }
    }
}
