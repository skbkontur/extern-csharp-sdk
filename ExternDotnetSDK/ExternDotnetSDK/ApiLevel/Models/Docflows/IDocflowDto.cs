using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDocflowDto
    {
        /// <summary>
        /// Тип документооборота
        /// </summary>
        public Urn Type { get; }
        
        
        /// <summary>
        /// Описание документооборота
        /// </summary>
        public DocflowDescription Description { get; set; }
    }
}