
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
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