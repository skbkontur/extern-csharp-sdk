using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    [DataContract]
    public class Certificate
    {
        /// <param name="content">Публичная часть сертификата в формате Base64</param>
        public Certificate(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new InvalidDataException("content is a required property for Certificate and cannot be null");

            Content = content;
        }

        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        /// <value>Публичная часть сертификата</value>
        [DataMember(Name = "content", EmitDefaultValue = false)]
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