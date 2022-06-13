using System.Text;

namespace Prose.Application.Extensions
{
    public static class StringExtensions
    {
        public static string CriptografarSha256(this string valor)
        {
            if (valor == null)
            {
                return null;
            }

            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(valor), 0, Encoding.UTF8.GetByteCount(valor));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();

        }
    }
}
