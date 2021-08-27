using System;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    public class DecryptDocumentResult
    {
        /// <summary>
        /// Идентификатор расшифрованного контента
        /// </summary>
        public Guid ContentId { get; set; }
    }
}