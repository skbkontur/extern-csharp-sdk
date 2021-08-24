using System;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class FssSedoPvsoNotificationDescription : FssSedoDescription
    {
        /// <summary>
        /// Время окончания актуальности уведомления
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }
}