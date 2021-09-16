using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Warrants
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
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП организации учетной записи Экcтерна
        /// </summary>
        public Kpp Kpp { get; set; } = null!;

        /// <summary>
        /// Название организации учетной записи Экcтерна
        /// </summary>
        public string OrganizationName { get; set; } = null!;

        /// <summary>
        /// Доверенность, если она есть, относящаяся к этой организации учетной записи Экcтерна
        /// </summary>
        public Warrant? Warrant { get; set; }
    }
}