using System.Text;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации календаря отчетности.
    /// </summary>
    public class GenerateXmlCalendarRequest
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateXmlCalendarRequest {\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}