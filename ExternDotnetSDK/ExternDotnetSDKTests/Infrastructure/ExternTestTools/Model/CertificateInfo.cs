using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    [DataContract]
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
        [DataMember(Name = "thumbprint", EmitDefaultValue = false)]
        public string Thumbprint { get; set; }

        /// <summary>
        /// Ссылка на сертификат
        /// </summary>
        /// <value>Ссылка на сертификат</value>
        [DataMember(Name = "certificateDrivePath", EmitDefaultValue = false)]
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