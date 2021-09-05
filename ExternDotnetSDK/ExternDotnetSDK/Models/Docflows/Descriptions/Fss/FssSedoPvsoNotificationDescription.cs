using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoPvsoNotificationDescription : FssSedoDescription
    {
        /// <summary>
        /// Время окончания актуальности уведомления
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}