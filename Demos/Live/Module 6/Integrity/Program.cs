using System.Security.Cryptography;
using System.Text;

namespace Integrity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Hashing();
            //Symmetrisch();
            Asymmetrisch();
        }

        private static void Asymmetrisch()
        {
            string msg = "Hello World";

            DSA dsa = new DSACryptoServiceProvider();
            string pub = dsa.ToXmlString(false);
            string priv = dsa.ToXmlString(true);   

            var alg = SHA1.Create();
            byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(msg));
            byte[] signature = dsa.SignData(hash, hashAlgorithm: HashAlgorithmName.SHA1);
            Console.WriteLine(Convert.ToBase64String(signature));

            msg += ".";

            var alg2 = SHA1.Create();
            byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(msg));
            DSA dsa2 = new DSACryptoServiceProvider();
            dsa2.FromXmlString(pub);
            bool isOk = dsa2.VerifyData(hash2, signature, HashAlgorithmName.SHA1);
            Console.WriteLine(isOk);
        }

        private static void Symmetrisch()
        {
            string msg = "Hello World";
            var alg = new HMACSHA256();
            byte[] key = alg.Key;

            byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(msg));
            Console.WriteLine(Convert.ToBase64String(hash));
            
            //msg += "!";

            var alg2 = new HMACSHA256();
            alg2.Key= key;
            byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(msg));
            Console.WriteLine(Convert.ToBase64String(hash2));
        }

        private static void Hashing()
        {
            string msg = "Hello World";
            var alg = SHA256.Create();
            byte[] hash = alg.ComputeHash(Encoding.UTF8.GetBytes(msg));
            Console.WriteLine(Convert.ToBase64String(hash));

            msg += ".";
            var alg2 = SHA256.Create();
            byte[] hash2 = alg2.ComputeHash(Encoding.UTF8.GetBytes(msg));
            Console.WriteLine(Convert.ToBase64String(hash2));
        }
    }
}