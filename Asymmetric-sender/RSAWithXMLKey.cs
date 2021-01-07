using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;

namespace Asymmetric_sender
{
    public class RsaWithXmlKey 
    {
        public byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cipherbytes;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;                
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));

                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }

            return cipherbytes;
        }
    }
}