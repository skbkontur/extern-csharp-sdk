using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CuLetterBroadcastDescription : DocflowDescription
    {
        /// <summary>
        /// Код инспекции, откуда пришла рассылка 
        /// </summary>
        public string Cu { get; set; } = null!;
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; } = null!;
    }
}