using System.Security.Cryptography;
using System.Text;

namespace JeanLouisEtFils;

public class AesCrypto
{
    private static string key = "a3bd614b27864e3f854b971f9df1a802";
    private static byte[] iv = Convert.FromHexString("17382d434e595a0c22384e5a0c22384e");

    public string Encrypt(string source)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(source);
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
