using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Signer
    {
        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName PersonFullName { get; set; }
    }
}