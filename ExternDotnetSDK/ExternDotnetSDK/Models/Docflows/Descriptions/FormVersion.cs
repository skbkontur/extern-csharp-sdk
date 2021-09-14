using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FormVersion
    {
        /// <summary>
        /// КНД
        /// </summary>
        public string Knd { get; set; }
        
        /// <summary>
        /// ОКУД
        /// </summary>
        public string Okud { get; set; }
        
        /// <summary>
        /// Уникальный идентификатор формы в Экстерне
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Полное название формы
        /// </summary>
        public string FormFullname { get; set; }
        
        /// <summary>
        /// Краткое название формы
        /// </summary>
        public string FormShortname { get; set; }
    }
}