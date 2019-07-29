using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Common
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Content
    {
        [UsedImplicitly]
        public Link Decrypted { get; set; }

        public Link Encrypted { get; set; }
    }
}