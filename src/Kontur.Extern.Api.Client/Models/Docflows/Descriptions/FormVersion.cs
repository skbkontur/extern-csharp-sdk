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
        public Knd? Knd { get; set; }
        
        /// <summary>
        /// ОКУД
        /// </summary>
        public Okud? Okud { get; set; }
        
        /// <summary>
        /// Полное название формы
        /// </summary>
        public string? FormFullname { get; set; }
        
        /// <summary>
        /// Краткое название формы
        /// </summary>
        public string? FormShortname { get; set; }
    }
}