using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationGeneral
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; }
        
        /// <summary>
        /// КПП организации
        /// </summary>
        public string Kpp { get; set; }
        
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Признак головной организации
        /// </summary>
        public bool IsMainOrg { get; set; }
        
        /// <summary>
        /// Ссылки для работы с организациями
        /// </summary>
        public Link[] Links { get; set; }
    }
}