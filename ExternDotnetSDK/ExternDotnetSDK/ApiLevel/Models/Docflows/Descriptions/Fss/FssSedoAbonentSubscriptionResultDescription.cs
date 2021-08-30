using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoAbonentSubscriptionResultDescription : FssSedoDescription
    {
        /// <summary>
        /// Название отправленной подписки
        /// </summary>
        public string FormType { get; set; }
    }
}