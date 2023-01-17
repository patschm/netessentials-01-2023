using System.Security.Cryptography;
using System.Text;

namespace Confidentiality
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Asymmetrisch();
            Symmetrisch();
        }

        private static void Symmetrisch()
        {
            string msg = "Hello World";
            Aes alg = Aes.Create();
            byte[] key = alg.Key;

            byte[] cipher;
            using (MemoryStream str = new MemoryStream())
            {
                using CryptoStream cr = new CryptoStream(str, alg.CreateEncryptor(), CryptoStreamMode.Write);
                {
                    StreamWriter writer = new StreamWriter(cr);
                    {
                        writer.WriteLine(msg);                
                    }
                }    
                cipher = str.ToArray();  
            }


            // Ontvanger
            Aes alg2 = Aes.Create();
            alg2.Key = key;
            using(MemoryStream str = new MemoryStream(cipher))
            {
                using (CryptoStream cr = new CryptoStream(str, alg2.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader rdr = new StreamReader(cr))
                    {
                        Console.WriteLine(rdr.ReadToEnd());
                    }
                }
            }

        }

        private static void Asymmetrisch()
        {
            string msg = "Hello World";
            RSA ontvanger = new RSACryptoServiceProvider();
            var pub = ontvanger.ToXmlString(false);

            // Sender
            RSA sender = new RSACryptoServiceProvider();
            sender.FromXmlString(pub);
            byte[] cipher = sender.Encrypt(Encoding.UTF8.GetBytes(msg), RSAEncryptionPadding.Pkcs1);

            // Ontvanger
            byte[] data = ontvanger.Decrypt(cipher, RSAEncryptionPadding.Pkcs1);
            Console.WriteLine(Encoding.UTF8.GetString(data));

        }
    }
}