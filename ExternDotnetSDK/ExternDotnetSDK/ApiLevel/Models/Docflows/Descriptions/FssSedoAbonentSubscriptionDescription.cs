
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
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