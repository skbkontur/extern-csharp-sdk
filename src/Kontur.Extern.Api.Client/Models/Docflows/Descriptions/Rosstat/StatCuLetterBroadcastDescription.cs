using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatCuLetterBroadcastDescription : DocflowDescription
    {
        /// <summary>
        /// Код ТОГС, откуда направлена рассылка
        /// </summary>
        public TogsCode Cu { get; set; } = null!;

        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; } = null!;
    }
}