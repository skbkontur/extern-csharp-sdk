using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class IpInfo
    {
        /// <summary>
        /// ОГРНИП
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string OgrnIp { get; set; } = null!;
    }
}