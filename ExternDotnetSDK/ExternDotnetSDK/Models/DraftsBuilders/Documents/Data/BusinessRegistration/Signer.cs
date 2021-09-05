using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
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