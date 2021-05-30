using System.Text.RegularExpressions;

namespace Commom.Services
{
    public static class PasswordServices
    {
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Validate(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public static bool ShipperPasswordIsValid(string password)
        {
            var patternPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]");

            return patternPassword.IsMatch(password);
        }
    }
}
