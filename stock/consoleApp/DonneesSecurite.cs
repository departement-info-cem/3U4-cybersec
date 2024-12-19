// https://crackstation.net/
// https://www.mscs.dal.ca/~selinger/md5collision/

using System;

namespace consoleApp
{
    class DonneesSecurite
    {
        public static string Encrypter(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                char t = (char)(c * 2);
                result += t;
            }
            return result;
        }

        public static string Decrypter(string input)
        {
            string result = "";
            foreach (char c in input)
            {
                char t = (char)(c / 2);
                result += t;
            }
            return result;
        }

        // fonctions permettant le hachage des mots de passe
        public static string HacherLeMotDePasse(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
        
        public static bool VerifierLeMotDePasse(string motDePasse, string hache)
        {
            // Use input string to calculate MD5 hash
            return hache == DonneesSecurite.HacherLeMotDePasse(motDePasse);
        }
        
    }
}
