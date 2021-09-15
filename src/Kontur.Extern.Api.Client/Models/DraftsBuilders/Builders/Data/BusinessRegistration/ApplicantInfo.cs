using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApplicantInfo
    {
        /// <summary>
        /// ИНН
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Inn { get; set; } = null!;

        /// <summary>
        /// ФИО
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        [JsonPropertyName("fio")]
        public PersonFullName PersonFullName { get; set; } = null!;

        /// <summary>
        /// E-mail
        /// </summary>
        public string? Email { get; set; }
    }
}