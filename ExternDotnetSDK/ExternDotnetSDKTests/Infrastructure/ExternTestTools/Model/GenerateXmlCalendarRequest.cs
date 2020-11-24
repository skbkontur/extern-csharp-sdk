using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.Model
{
    /// <summary>
    /// Данные для генерации календаря отчетности.
    /// </summary>
    [DataContract]
    public class GenerateXmlCalendarRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateXmlCalendarRequest" /> class.
        /// </summary>
        [JsonConstructor]
        public GenerateXmlCalendarRequest()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GenerateXmlCalendarRequest {\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}