#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Warrants
{
    /// <summary>
    /// Информация о частном лице или индивидуальном предпринимателе из доверенности
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantIndividual
    {
        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName? PersonFullName { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string? Inn { get; set; }

        /// <summary>
        /// Удостоверение личности
        /// </summary>
        public IdentityCard? Document { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly? BirthDate { get; set; }

        /// <summary>
        /// ОГРНИП
        /// </summary>
        public string? Ogrnip { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        public string? Citizenship { get; set; }

        /// <summary>
        /// Место жительства
        /// </summary>
        public string? Address { get; set; }
    }
}