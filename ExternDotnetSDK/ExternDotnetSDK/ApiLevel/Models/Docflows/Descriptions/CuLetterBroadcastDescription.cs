using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CuLetterBroadcastDescription : DocflowDescription
    {
        /// <summary>
        /// Код инспекции, откуда пришла рассылка 
        /// </summary>
        public string Cu { get; set; }
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
    }
}