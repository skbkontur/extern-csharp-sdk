using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GostCryptography.Base;

namespace Kontur.Extern.Api.Client.Cryptography
{
    public class WinApiCrypt : ICrypt
    {
        private static readonly bool isWin2000 = Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0;

        private static readonly IDictionary<string, string> HashAlgorithmsMap = new Dictionary<string, string>
        {
            {Api.OID_GOST_34_11_94_R3410EL, Api.OID_GOST_34_11_94},
            {Api.OID_GOST_34_11_12_256_R3410, Api.OID_GOST_34_11_12_256},
            {Api.OID_GOST_34_11_12_512_R3410, Api.OID_GOST_34_11_12_512}
        };

        public List<X509Certificate2> GetPersonalCertificates(bool onlyWithPrivateKey, bool useLocalSystemStorage = false)
        {
            var s = new X509Store("MY", useLocalSystemStorage ? StoreLocation.LocalMachine : StoreLocation.CurrentUser);
            s.Open(OpenFlags.ReadOnly);
            try
            {
                return s.Certificates.Cast<X509Certificate2>().Where(c => !onlyWithPrivateKey || c.HasPrivateKey).ToList();
            }
            finally
            {
                s.Close();
            }
        }

        public X509Certificate2 GetCertificateWithPrivateKey(string thumbprint, bool useLocalSystemStorage = false)
        {
            var cert = GetPersonalCertificates(true, useLocalSystemStorage)
                .FirstOrDefault(c => c.Thumbprint != null && c.Thumbprint.Equals(thumbprint, StringComparison.OrdinalIgnoreCase));
            if (cert == null)
                throw new Exception("Не найден сертификат с закрытым ключом и отпечатком " + thumbprint);
            return cert;
        }

        public List<byte[]> VerifySignature(byte[] content, byte[] signatures)
        {
            GCHandle contentHandle = GCHandle.Alloc(content, GCHandleType.Pinned);
            try
            {
                var verifyParameters = new Api.CRYPT_VERIFY_MESSAGE_PARA();
                verifyParameters.size = Marshal.SizeOf(verifyParameters);
                verifyParameters.encoding = Api.ENCODING;
                var signersCertificateContents = new List<byte[]>();
                for (Int32 signerIndex = 0;; ++signerIndex)
                {
                    IntPtr certificate;
                    if (!Api.CryptVerifyDetachedMessageSignature(ref verifyParameters, signerIndex, signatures, signatures.Length, 1, new[] {contentHandle.AddrOfPinnedObject()}, new[] {content.Length}, out certificate))
                    {
                        Int32 errorCode = Marshal.GetLastWin32Error();
                        if (errorCode == Api.CRYPT_E_NO_SIGNER) break;
                        if (errorCode == Api.NTE_BAD_SIGNATURE) throw new Exception("Неправильная подпись");
                        throw new Exception("Неправильная подпись", new Win32Exception(errorCode));
                    }

                    signersCertificateContents.Add(SerializeCertificateToBinaryData(certificate));
                    Api.CertFreeCertificateContext(certificate);
                }

                return signersCertificateContents;
            }
            finally
            {
                contentHandle.Free();
            }
        }

        public byte[] Sign(byte[] content, byte[] certificateContent)
        {
            var certificate = CertificateWithPrivateKeyFinder.GetCertificateWithPrivateKey(certificateContent);
            var certificatesHandle = GCHandle.Alloc(new[] {certificate}, GCHandleType.Pinned);

            try
            {
                return Sign(content, certificate, certificatesHandle);
            }
            finally
            {
                certificatesHandle.Free();
                Api.CertFreeCertificateContext(certificate);
            }
        }

        public byte[] RawSign(byte[] dataToSign, X509Certificate2 certificate)
        {
            var privateKeyAlgorithm = (GostAsymmetricAlgorithm)certificate.GetPrivateKeyAlgorithm();
            byte[] hash;

            using (var hashAlg = privateKeyAlgorithm.CreateHashAlgorithm())
                hash = hashAlg.ComputeHash(dataToSign);

            var signature = privateKeyAlgorithm.CreateSignature(hash);
            Array.Reverse(signature);
            return signature;
        }

        protected static byte[] Sign(byte[] content, IntPtr certificate, GCHandle certificatesHandle)
        {
            var contentHandle = GCHandle.Alloc(content, GCHandleType.Pinned);

            try
            {
                var signParameters = new Api.CRYPT_SIGN_MESSAGE_PARA();
                signParameters.size = Marshal.SizeOf(signParameters);
                signParameters.encoding = Api.ENCODING;
                signParameters.signerCertificate = certificate;
                signParameters.hashAlgorithm.objectIdAnsiString = GetHashAlgorithm(certificate);
                signParameters.certificatesCount = 1;
                signParameters.certificates = certificatesHandle.AddrOfPinnedObject();

                var localSignParameters = signParameters;
                var signatureSize = 0;

                if (!Api.CryptSignMessage(ref localSignParameters, true, 1, new[] {contentHandle.AddrOfPinnedObject()}, new[] {content.Length}, IntPtr.Zero, ref signatureSize))
                    throw new Win32Exception();

                var bufferLength = signatureSize + 1024;

                while (true)
                {
                    var buffer = new byte[bufferLength];
                    var bytesWritten = bufferLength;
                    var signatureHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                    try
                    {
                        if (!Api.CryptSignMessage(ref signParameters, true, 1, new[] {contentHandle.AddrOfPinnedObject()}, new[] {content.Length}, signatureHandle.AddrOfPinnedObject(), ref bytesWritten))
                            throw new Win32Exception();

                        Array.Resize(ref buffer, bytesWritten);
                        return buffer;
                    }
                    catch (Exception exception)
                    {
                        var win32Exception = exception.InnerException as Win32Exception;
                        if (win32Exception != null && win32Exception.NativeErrorCode == 234)
                            bufferLength *= 2;
                        else
                            throw;
                    }
                    finally
                    {
                        signatureHandle.Free();
                    }
                }
            }
            finally
            {
                contentHandle.Free();
            }
        }

        private static string GetHashAlgorithm(IntPtr certificatePtr)
        {
            var certificate2 = new X509Certificate2(certificatePtr);
            var signatureAlgorithm = certificate2.SignatureAlgorithm.Value;

            return HashAlgorithmsMap.TryGetValue(signatureAlgorithm, out var hashAlgorithm)
                ? hashAlgorithm
                : Api.OID_GOST_34_11_94;
        }

        public byte[] Decrypt(byte[] encryptedContent, bool useLocalSystemStorage = false)
        {
            if (encryptedContent == null) throw new ArgumentNullException("encryptedContent");
            IntPtr storeHandle = OpenStore("my", Api.CERT_STORE_READONLY_FLAG | (useLocalSystemStorage ? Api.CERT_SYSTEM_STORE_LOCAL_MACHINE : Api.CERT_SYSTEM_STORE_CURRENT_USER));
            GCHandle pinnedStoreHandle = GCHandle.Alloc(storeHandle, GCHandleType.Pinned);
            try
            {
                var decryptParameters =
                    new Api.CRYPT_DECRYPT_MESSAGE_PARA
                    {
                        size = Marshal.SizeOf(typeof (Api.CRYPT_DECRYPT_MESSAGE_PARA)),
                        encoding = Api.ENCODING,
                        storesCount = 1,
                        stores = pinnedStoreHandle.AddrOfPinnedObject()
                    };
                if (isWin2000)
                    decryptParameters.size -= Marshal.SizeOf(decryptParameters.flags.GetType());
                int bufferLength = 0;
                GCHandle inBufferHandle = GCHandle.Alloc(encryptedContent, GCHandleType.Pinned);
                try
                {
                    if (!Api.CryptDecryptMessage(ref decryptParameters, inBufferHandle.AddrOfPinnedObject(), encryptedContent.Length, IntPtr.Zero, ref bufferLength, IntPtr.Zero))
                        throw new Win32Exception();
                    var buffer = new byte[bufferLength];
                    GCHandle outBufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    try
                    {
                        if (!Api.CryptDecryptMessage(ref decryptParameters, inBufferHandle.AddrOfPinnedObject(), encryptedContent.Length, outBufferHandle.AddrOfPinnedObject(), ref bufferLength, IntPtr.Zero))
                            throw new Win32Exception();
                        var finalBuffer = new byte[bufferLength];
                        Array.Copy(buffer, finalBuffer, bufferLength);
                        return finalBuffer;
                    }
                    finally
                    {
                        outBufferHandle.Free();
                    }
                }
                finally
                {
                    inBufferHandle.Free();
                }
            }
            finally
            {
                CloseStore(storeHandle);
                pinnedStoreHandle.Free();
            }
        }

        private byte[] SerializeCertificateToBinaryData(IntPtr certificate)
        {
            var certificateContext = (Api.CERT_CONTEXT) Marshal.PtrToStructure(certificate, typeof (Api.CERT_CONTEXT));
            var encodedCertificate = new byte[certificateContext.encodedCertificateSize];
            Marshal.Copy(certificateContext.encodedCertificate, encodedCertificate, 0, certificateContext.encodedCertificateSize);
            return encodedCertificate;
        }

        private IntPtr OpenStore(String name, int flags)
        {
            IntPtr storeHandle = Api.CertOpenStore(new IntPtr(Api.CERT_STORE_PROV_SYSTEM), 0, IntPtr.Zero, flags, Encoding.Unicode.GetBytes(name));
            if (storeHandle == IntPtr.Zero) throw new Win32Exception();
            return storeHandle;
        }

        private void CloseStore(IntPtr storeHandle)
        {
            Api.CertCloseStore(storeHandle, 0);
        }
    }
}