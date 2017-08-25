using System.Security.Cryptography;
using System.Text;

namespace MDW.Tools
{
    public class Hash
    {
        public static string MD5(string value)
        {
            return Hash.MD5(Encoding.UTF8.GetBytes(value));
        }

        public static string MD5(byte[] value)
        {
            var result = new StringBuilder();

            using (var hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] buffer = hash.ComputeHash(value);

                foreach (byte i in buffer)
                    result.Append(i.ToString("X2"));
            }

            return result.ToString();
        }

        public static string SHA1(string value)
        {
            var result = new StringBuilder();

            using (var hash = SHA1Managed.Create())
            {
                var buffer = hash.ComputeHash(Encoding.ASCII.GetBytes(value));

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }

        public static string SHA1(byte[] value)
        {
            var result = new StringBuilder();

            using (var hash = SHA1Managed.Create())
            {
                var buffer = hash.ComputeHash(value);

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }

        public static string SHA256(string value)
        {
            var result = new StringBuilder();

            using (var hash = SHA256Managed.Create())
            {
                var buffer = hash.ComputeHash(Encoding.ASCII.GetBytes(value));

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }

        public static string SHA256(byte[] value)
        {
            var result = new StringBuilder();

            using (var hash = SHA256Managed.Create())
            {
                var buffer = hash.ComputeHash(value);

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }

        public static string SHA512(string value)
        {
            var result = new StringBuilder();

            using (var hash = SHA512Managed.Create())
            {
                var buffer = hash.ComputeHash(Encoding.ASCII.GetBytes(value));

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }

        public static string SHA512(byte[] value)
        {
            var result = new StringBuilder();

            using (var hash = SHA512Managed.Create())
            {
                var buffer = hash.ComputeHash(value);

                foreach (var i in buffer)
                    result.Append(i.ToString("x2"));
            }

            return result.ToString();
        }
    }
}