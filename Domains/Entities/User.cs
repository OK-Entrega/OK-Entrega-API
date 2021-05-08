using Commom.Entities;

namespace Domains.Entities
{
    public class User : Entity
    {
        public string Nome { get; private set; }
        public string Senha { get; private set; }

        public User(string nome, string senha)
        {
            Nome = nome;
            Senha = senha;
        }
    }
}
