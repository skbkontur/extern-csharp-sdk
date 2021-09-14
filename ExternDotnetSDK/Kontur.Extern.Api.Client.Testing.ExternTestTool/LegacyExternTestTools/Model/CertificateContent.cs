using System.IO;
using System.Text;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.LegacyExternTestTools.Model
{
    public class CertificateContent
    {
        /// <param name="content">Публичная часть сертификата в формате Base64</param>
        public CertificateContent(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new InvalidDataException("content is a required property for Certificate and cannot be null");

            Content = content;
        }

        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        /// <value>Публичная часть сертификата</value>
        public string Content { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Certificate {\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}