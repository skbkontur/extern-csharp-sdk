using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FormVersion
    {
        /// <summary>
        /// КНД
        /// </summary>
        public Knd Knd { get; set; } = null!;
        
        /// <summary>
        /// ОКУД
        /// </summary>
        public Okud Okud { get; set; } = null!;
        
        /// <summary>
        /// Уникальный идентификатор формы в Экстерне
        /// </summary>
        public string Version { get; set; } = null!;
        
        /// <summary>
        /// Полное название формы
        /// </summary>
        public string FormFullname { get; set; } = null!;
        
        /// <summary>
        /// Краткое название формы
        /// </summary>
        public string FormShortname { get; set; } = null!;
    }
}