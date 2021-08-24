using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FssSedoPvsoNotificationDescription : FssSedoDescription
    {
        /// <summary>
        /// Время окончания актуальности уведомления
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}