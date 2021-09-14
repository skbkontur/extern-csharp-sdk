using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatCuLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Код ТОГС, откуда направлено письмо
        /// </summary>
        public string Cu { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота, к которому направлено письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        /// Код ОКПО
        /// </summary>
        public string Okpo { get; set; }
    }
}