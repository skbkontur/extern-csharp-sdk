using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocflowPage
    {
        /// <summary>
        /// Количество записей, которые были пропущены при считывании
        /// </summary>
        public long Skip { get; set; }
        
        /// <summary>
        /// Количество записей, которые вернулись в запросе
        /// </summary>
        public long Take { get; set; }
        
        /// <summary>
        /// Общее количество найденных записей
        /// </summary>
        public long TotalCount { get; set; }
        
        /// <summary>
        /// Краткая информация о документообороте
        /// </summary>
        public DocflowPageItem[] DocflowsPageItem { get; set; }
    }
}