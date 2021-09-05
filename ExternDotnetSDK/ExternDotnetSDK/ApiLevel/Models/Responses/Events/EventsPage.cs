using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Events;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Events
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class EventsPage
    {
        /// <summary>
        /// Идентификатор первого события на странице ленты событий
        /// </summary>
        public string FirstEventId { get; set; }
        
        /// <summary>
        /// Идентификатор следующего события в ленте, с которого нужно продолжать чтение. Передать в параметре fromId
        /// </summary>
        public string NextEventId { get; set; }
        
        /// <summary>
        /// Число запрошенных событий в параметре take
        /// </summary>
        public int RequestedCount { get; set; }
        
        /// <summary>
        /// Количество событий, которые вернулись в запросе
        /// </summary>
        public int ReturnedCount { get; set; }
        
        /// <summary>
        /// События
        /// </summary>
        public ApiEvent[] ApiEvents { get; set; }
        
        /// <summary>
        /// Ссылки для работы со страницей ленты событий
        /// </summary>
        public Link[] Links { get; set; }
    }
}