
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class CuLetterBroadcastDescription : DocflowDescription
    {
        /// <summary>
        /// Код инспекции, откуда пришла рассылка 
        /// </summary>
        public string Cu { get; set; }
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
    }
}