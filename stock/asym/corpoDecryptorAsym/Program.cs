// See https://aka.ms/new-console-template for more information
using Encryption.Blowfish;
using System.Security.Cryptography;
using corpoDecryptoAsym;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the path of the file to encrypt:");
        string path = Console.ReadLine();
        string[] lines = File.ReadAllLines(path);
        // for each line, 
        foreach (string line in lines)
        {
            // encrypt the line with RSA
            RSACrypto rsa = new RSACrypto();
            string first = line.Split(" @ ")[0];
            string decrypted = rsa.Decrypt(first);
            Console.WriteLine(decrypted + " @ " + line.Split(" @ ")[1]);
        }
        
    }
}