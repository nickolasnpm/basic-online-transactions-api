using System.Security.Cryptography;
using System.Text;

namespace BasicOnlineTransactions.Helpers
{
    public class SHA256Generator
    {
        public static byte[] ComputeHashString(string hashstring)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashstring));

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return Encoding.ASCII.GetBytes(stringBuilder.ToString());
            }
        }
    }
}
