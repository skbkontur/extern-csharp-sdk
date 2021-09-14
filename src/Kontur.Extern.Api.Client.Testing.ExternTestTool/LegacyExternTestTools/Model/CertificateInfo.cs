using System.Text;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    [UsedImplicitly]
    public class CertificateInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateInfo" /> class.
        /// </summary>
        /// <param name="thumbprint">Отпечаток сертификата.</param>
        /// <param name="certificateDrivePath">Ссылка на сертификат.</param>
        public CertificateInfo(string thumbprint = default, string certificateDrivePath = default)
        {
            Thumbprint = thumbprint;
            CertificateDrivePath = certificateDrivePath;
        }

        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        /// <value>Отпечаток сертификата</value>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Ссылка на сертификат
        /// </summary>
        /// <value>Ссылка на сертификат</value>
        public string CertificateDrivePath { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CertificateInfo {\n");
            sb.Append("  Thumbprint: ").Append(Thumbprint).Append("\n");
            sb.Append("  CertificateDrivePath: ").Append(CertificateDrivePath).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}