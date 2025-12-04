namespace JeanLouisEtFilsAsym;

using System;
using System.Security.Cryptography;
using System.Text;

public class RSACrypto
{
    private RSAParameters _publicKey;

    private string _publicKeyString =
        "BgIAAACkAABSU0ExAAgAAAEAAQBRhBfNvOpECIkG2+UOxF6x9teUgWcu3/JXghPlYExFy9XtveTSZwNcYqWWkbFFKFi1wWC46wfmLYbGcIh4H35uaGSmXYIZU2KgPpbtH1MV/iuhjrr9VKmq+AC0zXkTlQ7Uq0oYX5lN6/07SSLnpiVanfP50pxwjJlk0uioWOA3KgJJGiRLo4nG66cNTFGB1qWMQ9sCoHZ15maDqSeddHi6tmVW5Zu1olDnj7phqogKuajrUGV5N9sBs2ZcanbEe1FbmtpY8HcQ6DRhfdeVLCvzpewr5qSuIcA6xTSm4fNUp1vAYE6M0KlLKiJpyOWTqL7116fiOwqd5W84ozCx4hfF";

    public RSACrypto()
    {
        byte[] publicKeyBytes = Convert.FromBase64String(_publicKeyString);
        using (var rsa2 = new RSACryptoServiceProvider())
        {
            rsa2.ImportCspBlob(publicKeyBytes);
            _publicKey =  rsa2.ExportParameters(false);
        }
    }
    
    public string Encrypt(string data)
    {
        byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
        byte[] encryptedData;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(_publicKey);
            encryptedData = rsa.Encrypt(dataToEncrypt, false);
        }
        return Convert.ToBase64String(encryptedData);
    }

}