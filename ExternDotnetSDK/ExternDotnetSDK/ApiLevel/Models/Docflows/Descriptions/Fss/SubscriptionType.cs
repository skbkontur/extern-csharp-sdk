using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Fss
{
    /// <summary>
    /// Тип подписки
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum SubscriptionType
    {
        Unknown,

        /// <summary>
        /// Подписка
        /// </summary>
        Subscribe,

        /// <summary>
        /// Отписка
        /// </summary>
        Unsubscribe
    }
}