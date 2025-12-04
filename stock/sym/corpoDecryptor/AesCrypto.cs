using System.Security.Cryptography;
using System.Text;

namespace corpoDecryptor;

public class AesCrypto
{
    private static string key = "a3bd614b27864e3f854b971f9df1a802";
    private static byte[] iv = Convert.FromHexString("17382d434e595a0c22384e5a0c22384e");

    public string Decrypt(string source)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] cipherBytes = Convert.FromBase64String(source);

            using (MemoryStream ms = new MemoryStream(cipherBytes))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
