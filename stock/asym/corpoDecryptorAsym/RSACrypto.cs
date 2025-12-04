namespace corpoDecryptoAsym;

using System;
using System.Security.Cryptography;
using System.Text;

public class RSACrypto
{
    private RSAParameters _publicKey;
    private RSAParameters _privateKey;

    // ici on a la clé publique uniquement utilisée pour encrypter
    private string _publicKeyString =
        "BgIAAACkAABSU0ExAAgAAAEAAQBRhBfNvOpECIkG2+UOxF6x9teUgWcu3/JXghPlYExFy9XtveTSZwNcYqWWkbFFKFi1wWC46wfmLYbGcIh4H35uaGSmXYIZU2KgPpbtH1MV/iuhjrr9VKmq+AC0zXkTlQ7Uq0oYX5lN6/07SSLnpiVanfP50pxwjJlk0uioWOA3KgJJGiRLo4nG66cNTFGB1qWMQ9sCoHZ15maDqSeddHi6tmVW5Zu1olDnj7phqogKuajrUGV5N9sBs2ZcanbEe1FbmtpY8HcQ6DRhfdeVLCvzpewr5qSuIcA6xTSm4fNUp1vAYE6M0KlLKiJpyOWTqL7116fiOwqd5W84ozCx4hfF";

    public string _allKeys =
        "BwIAAACkAABSU0EyAAgAAAEAAQBRhBfNvOpECIkG2+UOxF6x9teUgWcu3/JXghPlYExFy9XtveTSZwNcYqWWkbFFKFi1wWC46wfmLYbGcIh4H35uaGSmXYIZU2KgPpbtH1MV/iuhjrr9VKmq+AC0zXkTlQ7Uq0oYX5lN6/07SSLnpiVanfP50pxwjJlk0uioWOA3KgJJGiRLo\n4nG66cNTFGB1qWMQ9sCoHZ15maDqSeddHi6tmVW5Zu1olDnj7phqogKuajrUGV5N9sBs2ZcanbEe1FbmtpY8HcQ6DRhfdeVLCvzpewr5qSuIcA6xTSm4fNUp1vAYE6M0KlLKiJpyOWTqL7116fiOwqd5W84ozCx4hfFk3fVgaF0DwE7zOzSUXs9KKOQ1EOQh6Ebytj3yYnZBhG3ABaufdIK6av4o1DsvZ3NO\nk9mVf7VbcrmG9c2LS/1suVNL/QjPAAZwcfF/3KKqJQG7PyZxrko6M9n9t6t29ExpbrhKW+4xyFVv6Hy222W4lVUOoNntum0RiM0jhFwB/4LuwHBXjfBhKGL/g0hhCduX9c5jKggyLq9ohOiwCdvFD6uY67A+ptjlQ9jIgIZ2aDWx27GNAi6Qm9KZKkLZbMkn8aP3BM125n3n+iio31T9foHlISc0Bi/AOLwE\nR5PYx2oeRzMgEfHD5sMlkd+aIMkyO0hC8QoxVX9b8eJGlyfxsF/IDvpO/Quq9X+uFfkEPVrjysSp8vG0PhZIrrGyyiG/LnvOT3HkEdVeUXPGuv44D6TjRANnqx6C01VWaRRHyGjX8iimbH8Zmlp6s5+JcbvWcE6Gemk46ytEkAG6jKpIpcM5zehYAhrIM/GISUyeRgaNwBLmVrnDJIaHzSOUATvDUIAkSDmz\nP1UsqxKK/Q9Q++LD+dD21mOwWlxMGJa2Fn9La9e9AD4oWaf8rnbQ7ggu3pFIdOmDdLcbfihoeEjnwh1dNSEQlFajAWdnZuqgswXNgIA8tGOqkDZVVyPnZTtaRHzF0okouzy5dL/bZS+yEGuGvdjYO4n+aCAv2iWaq5DCGY5uDmyn7bs4VKevj+kI4SAIZ/1yqdXbc6QC4Ss5BAPuhWdjdU/kRIYcYOSGFUBg\n3Yzwa100uOMB4bmrCJAxm5Zi0fx22HVFSj2Y4P21BAfhv2HTD6dGo7XispgCpOiqHLZd86oyLFUNJMg4qKIuaX4Krxsn10Df4i0NQD4RTmjvzuEucK8WtwsPGfvuzvih2TsNEeWkaqXUH9jaxZngI/KrY6KveaZ/i5qnDo8aV2FzGed5PB61cKSZmIVtt7DrA9DrQOtJ3VjAAcnv4Exv3Eb5YyCpM6wIboowKU+NzqwvdvPyyPZoXbAXPAEB3WC52nv56iNG6iVGF1zi1C99p3wb5EPuVOCcX30fmL4oK3wjz8Jkz5yc/vJtrC2Z0JNkaMzbr71YJxhviwx6dEtolI7/OEaUcN6oBV/PS1MY9bHsEOJ8G/rL1zsc7L+hIATgqgutNN3Qvow6dFYos+97SYip00h3bURw7XCv5CqfHFZkdCToo6TwVAXkc7qh8Q=";
    
    public RSACrypto()
    {
        byte[] publicKeyBytes = Convert.FromBase64String(_allKeys);
        using (var rsa2 = new RSACryptoServiceProvider())
        {
            rsa2.ImportCspBlob(publicKeyBytes);
            _publicKey =  rsa2.ExportParameters(false);
            _privateKey = rsa2.ExportParameters(true);
        }
    }

    // cette fonction n'est pas appelée en général mais c'est celle qui a permis de générer les clés présentes dans le code
    public void generateKeys()
    {
        // creer un objet RSA
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);

            string exportPublicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
            string exportPrivateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));
            Console.WriteLine("public key is  " + exportPublicKey);
            Console.WriteLine("private/public key is  " + exportPrivateKey);
            
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

    public string Decrypt(string encryptedData)
    {
        byte[] dataToDecrypt = Convert.FromBase64String(encryptedData);
        byte[] decryptedData;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(_privateKey);
            decryptedData = rsa.Decrypt(dataToDecrypt, false);
        }
        return Encoding.UTF8.GetString(decryptedData);
    }
}