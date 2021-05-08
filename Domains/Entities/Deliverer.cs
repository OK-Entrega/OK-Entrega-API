namespace Domains.Entities
{
    public class Deliverer : User
    {
        public string Celular { get; private set; }

        public Deliverer(
            string nome,
            string senha, 
            string celular
        ) : base(
            nome,
            senha
        )
        {
            Celular = celular;
        }
    }
}
