using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FssSedoAbonentSubscriptionDescription : FssSedoDescription
    {
        /// <summary>
        /// Список СНИЛС
        /// </summary>
        public string[] SnilsList { get; set; }

        /// <summary>
        /// Название отправленной подписки
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// Тип отправленной подписки
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }
    }
}