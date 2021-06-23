using System;
using System.Security.Cryptography.X509Certificates;

namespace Kontur.Extern.Client.Cryptography
{
    public static class BytesExtension
    {
        /// <summary>
        ///     Сравнение двух массивов байт
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static int Compare(this byte[] d1, byte[] d2)
        {
            if (d1 == d2) return 0;
            if (d1 == null) return -1;
            if (d2 == null) return 1;
            var data1Length = d1.Length;
            var data2Length = d2.Length;
            for (var i = 0; i < Math.Min(data1Length, data2Length); ++i)
            {
                var comparison = d1[i].CompareTo(d2[i]);
                if (comparison != 0) return comparison;
            }

            return data1Length.CompareTo(data2Length);
        }

        /// <summary>
        /// Получение сертификата в кодировке Base64
        /// </summary>
        /// <param name="cert">сертификат</param>
        /// <returns>Сертификат в base64</returns>
        public static string GetBase64(this X509Certificate2 cert)
        {
            return Convert.ToBase64String(cert.Export(X509ContentType.Cert));
        }
    }
}