namespace Domains.Entities
{
    public class Local
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
