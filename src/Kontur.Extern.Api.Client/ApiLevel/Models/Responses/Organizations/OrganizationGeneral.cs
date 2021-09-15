using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationGeneral
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП организации
        /// </summary>
        public string Kpp { get; set; } = null!;

        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Признак головной организации
        /// </summary>
        public bool IsMainOrg { get; set; }
        
        /// <summary>
        /// Ссылки для работы с организациями
        /// </summary>
        public Link[] Links { get; set; } = null!;
    }
}