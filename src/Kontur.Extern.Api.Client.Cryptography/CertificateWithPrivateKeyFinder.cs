using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Cryptography
{
    internal static class CertificateWithPrivateKeyFinder
    {
        /// <summary>
        /// Поиск сертификата с закрытым ключом по содержимому
        /// </summary>
        /// <param name="certificateContent">Содержимое сертификата</param>
        /// <returns>Указатель на сертификат</returns>
        internal static IntPtr GetCertificateWithPrivateKey(byte[] certificateContent)
        {
            IntPtr signerCertificate = Api.CertCreateCertificateContext(Api.ENCODING, certificateContent, certificateContent.Length);
            return GetCertificateWithPrivateKeyInternal(signerCertificate);
        }

        /// <summary>
        /// Получение списка хранилищ по-умлочанию
        /// </summary>
        /// <returns></returns>
        private static List<IntPtr> GetDefaultStores()
        {
            var stores = new List<IntPtr>();
            try
            {
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_CURRENT_USER, "my"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_CURRENT_USER, "root"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_CURRENT_USER, "ca"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_CURRENT_USER, "addressbook"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_LOCAL_MACHINE, "my"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_LOCAL_MACHINE, "root"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_LOCAL_MACHINE, "ca"));
                stores.Add(GetStoreHandle(Api.CERT_SYSTEM_STORE_LOCAL_MACHINE, "addressbook"));
                return stores;
            }
            catch
            {
                foreach (IntPtr store in stores)
                    Api.CertCloseStore(store, 0);
                throw;
            }
        }

        private static IntPtr GetCertificateWithPrivateKeyInternal(IntPtr initialCertificate)
        {
            if (HasPrivateKey(initialCertificate)) return initialCertificate;
            IntPtr certificateWithPrivateKey = FindCertificateWithPrivateKey(initialCertificate);
            if (certificateWithPrivateKey == IntPtr.Zero)
                throw new Exception("Сертификат с закрытым ключом не найден");
            return certificateWithPrivateKey;
        }

        /// <summary>
        /// Проверка на наличие закрытого ключа у сертификата
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        private static bool HasPrivateKey(IntPtr certificate)
        {
            int bufferSize = 0;
            if (Api.CertGetCertificateContextProperty(certificate, Api.CERT_KEY_PROV_INFO_PROP_ID, IntPtr.Zero, ref bufferSize)) return true;
            int errorCode = Marshal.GetLastWin32Error();
            if (errorCode == Api.CRYPT_E_NOT_FOUND) return false;
            throw new Win32Exception(errorCode);
        }

        /// <summary>
        /// Поиск сертификата, совпрадающего с исходным в известных хранилищах
        /// </summary>
        /// <param name="initialCertificate">Исходный сертификат</param>
        /// <returns>Сертификат с закрытым ключом. IntPtr.Zero, если не найден</returns>
        private static IntPtr FindCertificateWithPrivateKey(IntPtr initialCertificate)
        {
            List<IntPtr> stores = GetDefaultStores();
            try
            {
                foreach (IntPtr store in stores)
                {
                    foreach (IntPtr storedCertificate in GetCertificatesFromStore(store))
                    {
                        if (GetHash(storedCertificate).Compare(GetHash(initialCertificate)) == 0 && HasPrivateKey(storedCertificate))
                            return storedCertificate;
                        else
                            Api.CertFreeCertificateContext(storedCertificate);
                    }
                }
            }
            finally
            {
                foreach (IntPtr storeHandle in stores)
                    Api.CertCloseStore(storeHandle, 0);
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// Получение указателя на хранилище сертификатов
        /// </summary>
        /// <param name="store"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static IntPtr GetStoreHandle(int store, String name)
        {
            int flags = store | Api.CERT_STORE_READONLY_FLAG;
            IntPtr storeHandle = Api.CertOpenStore(new IntPtr(Api.CERT_STORE_PROV_SYSTEM), 0, IntPtr.Zero, flags, Encoding.Unicode.GetBytes(name));
            if (storeHandle == IntPtr.Zero)
                throw new Win32Exception();
            return storeHandle;
        }
        /// <summary>
        /// Возвращает список сертификатов из хранилища
        /// </summary>
        /// <param name="storeHandle"></param>
        /// <returns></returns>
        private static List<IntPtr> GetCertificatesFromStore(IntPtr storeHandle)
        {
            var certificates = new List<IntPtr>();
            IntPtr certificate = IntPtr.Zero;
            try
            {
                while (true)
                {
                    certificate = Api.CertEnumCertificatesInStore(storeHandle, certificate);
                    if (certificate == IntPtr.Zero)
                    {
                        Int32 errorCode = Marshal.GetLastWin32Error();
                        if (errorCode == Api.CRYPT_E_NOT_FOUND || errorCode == Api.ERROR_NO_MORE_FILES)
                            break;
                        throw new Win32Exception(errorCode);
                    }
                    IntPtr certificateContext = Api.CertDuplicateCertificateContext(certificate);
                    certificates.Add(certificateContext);
                }
            }
            catch
            {
                if (certificate != IntPtr.Zero)
                    Api.CertFreeCertificateContext(certificate);
                throw;
            }
            return certificates;
        }

        /// <summary>
        /// Хэш для сертификата
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        private static byte[] GetHash(IntPtr certificate)
        {
            int bufferLength = 0;
            if (!Api.CertGetCertificateContextProperty(certificate, Api.CERT_HASH_PROP_ID, IntPtr.Zero, ref bufferLength)) throw new Win32Exception();
            var buffer = new byte[bufferLength];
            GCHandle bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                if (!Api.CertGetCertificateContextProperty(certificate, Api.CERT_HASH_PROP_ID, bufferHandle.AddrOfPinnedObject(), ref bufferLength)) throw new Win32Exception();
            }
            finally
            {
                bufferHandle.Free();
            }
            return buffer;
        }
    }
}
