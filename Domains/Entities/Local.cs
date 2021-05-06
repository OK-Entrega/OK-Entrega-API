using Commom.Entities;

namespace Domains.Entities
{
    public class Local : Entity
    {
        public string CEP { get; private set; }
        public string Adress { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }
        public string District { get; private set; }
        public string UF { get; private set; }
        public string City { get; private set; }

        public Local(
            string cep,
            string adress,
            int number,
            string complement,
            string district,
            string uf,
            string city
        )
        {

        }
    }
}
