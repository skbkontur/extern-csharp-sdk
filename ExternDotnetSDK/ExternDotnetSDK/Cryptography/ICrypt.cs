using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Kontur.Extern.Client.Cryptography
{
    public interface ICrypt
    {
        byte[] Sign(byte[] content, byte[] certificateContent);
        List<byte[]> VerifySignature(byte[] content, byte[] signatures);
        byte[] Decrypt(byte[] encryptedContent, bool userLocalSystemStorage = false);
        List<X509Certificate2> GetPersonalCertificates(bool onlyWithPrivateKey, bool useLocalSystemStorage = false);
        X509Certificate2 GetCertificateWithPrivateKey(string thumbprint, bool useLocalSystemStorage = false);
    }
}
