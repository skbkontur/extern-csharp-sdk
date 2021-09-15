using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UlInfo
    {
        /// <summary>
        /// ОГРН
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Ogrn { get; set; } = null!;

        /// <summary>
        /// Название организации
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = null!;
    }
}