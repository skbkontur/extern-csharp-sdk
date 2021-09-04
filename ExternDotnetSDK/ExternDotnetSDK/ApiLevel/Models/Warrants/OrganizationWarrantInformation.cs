#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    /// <summary>
    /// Информация о доверенности организации учетной записи Экcтерна
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationWarrantInformation
    {
        /// <summary>
        /// ИД организации учетной записи Экcтерна
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// ИНН организации учетной записи Экcтерна
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП организации учетной записи Экcтерна
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Название организации учетной записи Экcтерна
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Доверенность, если она есть, относящаяся к этой организации учетной записи Экcтерна
        /// </summary>
        public Warrant? Warrant { get; set; }
    }
}