using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApplicantInfo
    {
        public ApplicantInfo(Inn inn, PersonFullName personFullName, string? email)
        {
            Inn = inn;
            PersonFullName = personFullName ?? throw Errors.JsonDoesNotContainProperty(nameof(personFullName));
            Email = email;
        }

        /// <summary>
        /// ИНН
        /// </summary>
        public Inn Inn { get; }

        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName PersonFullName { get; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string? Email { get; }
    }
}