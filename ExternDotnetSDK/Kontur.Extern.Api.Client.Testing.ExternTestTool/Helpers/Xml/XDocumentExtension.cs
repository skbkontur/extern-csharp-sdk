using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Helpers.Xml
{
    internal static class XDocumentExtension
    {
        public static string ToStringWithDeclaration(this XDocument document, Encoding encoding)
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriterWithEncoding(builder, encoding))
            {
                document.Save(writer);
            }
            return builder.ToString();
        }

        private class StringWriterWithEncoding : StringWriter
        {
            public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
                : base(sb) => Encoding = encoding;

            public override Encoding Encoding { get; }
        }

    }
}