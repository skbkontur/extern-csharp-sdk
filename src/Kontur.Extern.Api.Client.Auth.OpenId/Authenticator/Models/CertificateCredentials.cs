using System.Security.Cryptography.X509Certificates;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    public class CertificateCredentials
    {
        /// <summary>
        /// Публичный ключ пользователя
        /// </summary>
        public X509Certificate2 PublicKeyCertificate { get; set; }

        /// <summary>
        /// Пропускать валидацию сертификата пользователя
        /// </summary>
        public bool Free { get; set; }
    }
}