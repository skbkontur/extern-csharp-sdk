using System;

namespace Kontur.Extern.Client.Models.Documents
{
    public class RecognizeRequest
    {
        /// <summary>
        /// Идентификатор контента
        /// </summary>
        public Guid ContentId { get; set; }
    }
}