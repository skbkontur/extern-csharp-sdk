using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class FssSedoDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
    }
}