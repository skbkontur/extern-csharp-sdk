using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FssSedoAbonentSubscriptionResultDescription : FssSedoDescription
    {
        /// <summary>
        /// Название отправленной подписки
        /// </summary>
        public string FormType { get; set; }
    }
}