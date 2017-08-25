using MDW.Tools;
using Microsoft.AspNet.Identity;

namespace MDW.Security
{
    public class WikiPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var result = string.Empty;

            result = Hash.SHA256(password);

            return result;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = PasswordVerificationResult.Failed;

            result = hashedPassword == Hash.SHA256(providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;

            return result;
        }
    }
}