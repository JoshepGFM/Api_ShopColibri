using System.Security.Cryptography;
using System.Text;

namespace API_ShopColibri.Tools
{
    public class Crypto
    {
        string LlavePersonalizada = "ApiColibri2323645j23353g2n324hg4";

        public string DesEncriptarPassword(string Pass)
        {
            String R = string.Empty;

            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    Byte[] byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(LlavePersonalizada));

                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;

                    Byte[] data = Convert.FromBase64String(Pass);

                    R = Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));

                }
            }

            return R;
        }
    }
}
