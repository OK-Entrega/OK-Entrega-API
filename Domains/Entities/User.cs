using Commom.Entities;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace Domains.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public Shipper Shipper { get; private set; }
        public Deliverer Deliverer { get; private set; }

        public User(
            string name, 
            string password
        )
        {
            name = name.Trim();
            password = password.Trim();

            var patternPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");

            AddNotifications(new Contract<User>()
                .Requires()
                .IsTrue((name.Length > 2 && name.Length < 41), "Nome", "O nome deve ter entre 3 à 40 caracteres!")
                .IsTrue((password.Length > 7 && password.Length < 21), "Senha", "A senha deve ter entre 8 à 20 caracteres!")
                .IsTrue(patternPassword.IsMatch(password), "Senha", "A senha deve conter letras maiúsculas, minúsculas e números!")
            );

            if (IsValid)
            {
                Name = name;
                Password = password;
                Active = false;
            }
        }
    }
}
