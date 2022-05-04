using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace EverywhereNotes.Helpers
{
    public class PasswordHelper
    {
        /// <summary>
        /// At least eight characters, at least one uppercase letter, one lowercase letter one number and special character
        /// </summary>
        private const string _pattern = @"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$_!%^&-+()*=])(?=.*[0-9]).*$";
        private static readonly int _saltLen = 32;

        public static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] data = Encoding.Default.GetBytes(password + salt);
            using SHA256 mySHA256 = SHA256.Create();
            var result = mySHA256.ComputeHash(data);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        public static bool PasswordValidation(string password)
        {
            if (password is null)
                return false;
            return Regex.IsMatch(password, _pattern);
        }
    }
}
