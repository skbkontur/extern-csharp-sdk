using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationBatch
    {
        /// <summary>
        /// Список найденных организаций
        /// </summary>
        public Organization[] Organizations { get; set; }
        
        /// <summary>
        /// Общее количество найденных организаций
        /// </summary>
        public long TotalCount { get; set; }
        
        /// <summary>
        /// Ссылки для работы со списком организаций
        /// </summary>
        public Link[] Links { get; set; }
        
        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        public long Skip { get; set; }
        
        /// <summary>
        /// Количество записей, которые вернулись в запросе
        /// </summary>
        public long Take { get; set; }
    }
}