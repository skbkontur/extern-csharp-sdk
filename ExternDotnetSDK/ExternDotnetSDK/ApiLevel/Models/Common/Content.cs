using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Content
    {
        [UsedImplicitly]
        public Link Decrypted { get; set; }

        public Link Encrypted { get; set; }
    }
}