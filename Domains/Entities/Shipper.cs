namespace Domains.Entities
{
    public class Shipper : User
    {
        public string Email { get; private set; }

        public Shipper(
            string nome, 
            string senha, 
            string email
        ) : base(
            nome, 
            senha
        )
        {
            Email = email;
        }
    }
}
