using System.Security.Cryptography;
using System.Text;

namespace rvepp.web.frontend.Common
{
    public static class StringExtensions
    {
        public static string ToSha256(this string str) => BitConverter.ToString(SHA256.HashData(Encoding.ASCII.GetBytes(str))).Replace("-", "").ToUpper();
    }
}