using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoAbonentSubscriptionResultDescription : FssSedoDescription
    {
        /// <summary>
        /// Название отправленной подписки
        /// </summary>
        public string FormType { get; set; } = null!;
    }
}