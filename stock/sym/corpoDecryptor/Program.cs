using corpoDecryptor;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the path of the file to encrypt:");
        string path = Console.ReadLine();
        string[] lines = System.IO.File.ReadAllLines(path);
        foreach (string line in lines)
        {
            BlowFishCrypto bfc = new BlowFishCrypto();
            string first = line.Split(" @ ")[0];
            string decrypted = bfc.Decrypt(first);
            Console.WriteLine(decrypted + " @ " + line.Split(" @ ")[1]);
        }
        
    }
}