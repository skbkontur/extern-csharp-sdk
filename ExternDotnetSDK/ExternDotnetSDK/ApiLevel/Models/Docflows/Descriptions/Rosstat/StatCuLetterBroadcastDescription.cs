
// ReSharper disable CommentTypo

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatCuLetterBroadcastDescription : DocflowDescription
    {
        /// <summary>
        /// Код ТОГС, откуда направлена рассылка
        /// </summary>
        public string Cu { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
    }
}