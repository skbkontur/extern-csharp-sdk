using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoAbonentSubscriptionDescription : FssSedoDescription
    {
        /// <summary>
        /// Список СНИЛС
        /// </summary>
        public string[] SnilsList { get; set; }

        /// <summary>
        /// Название отправленной подписки
        /// </summary>
        public string FormType { get; set; }

        /// <summary>
        /// Тип отправленной подписки
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }
    }
}