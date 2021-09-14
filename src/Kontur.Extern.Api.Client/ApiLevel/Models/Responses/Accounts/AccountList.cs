using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Accounts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AccountList
    {
        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        public long Skip { get; set; }
        /// <summary>
        /// Количество полученных записей
        /// </summary>
        public long Take { get; set; }
        /// <summary>
        /// Общее число найденных учетных записей
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// Учетные записи
        /// </summary>
        public Account[] Accounts { get; set; }
        /// <summary>
        /// Ссылки для работы с учетными записями
        /// </summary>
        public Link[] Links { get; set; }
    }
}