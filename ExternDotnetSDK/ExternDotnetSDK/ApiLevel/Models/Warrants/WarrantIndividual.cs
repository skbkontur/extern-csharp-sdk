#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
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
        public Fio? Fio { get; set; }

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
        [CanBeNull]
        public DateTime? BirthDate { get; set; }

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