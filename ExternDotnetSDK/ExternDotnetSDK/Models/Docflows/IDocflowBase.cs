using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows.Descriptions;

namespace Kontur.Extern.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDocflowBase
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