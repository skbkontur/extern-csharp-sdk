using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Signer
    {
        private const string JsonPropName = "fio";

        public Signer(PersonFullName personFullName) => 
            PersonFullName = personFullName ?? throw Errors.RequiredJsonPropertyIsMissed(JsonPropName);

        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName(JsonPropName)]
        public PersonFullName PersonFullName { get; }
    }
}