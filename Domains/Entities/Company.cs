using Commom.Entities;

namespace Domains.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string Segment { get; private set; }

        public Company(
            string name,
            string cnpj,
            string segment
        )
        {
            Name = name;
            CNPJ = cnpj;
            Segment = segment;
        }
    }
}
