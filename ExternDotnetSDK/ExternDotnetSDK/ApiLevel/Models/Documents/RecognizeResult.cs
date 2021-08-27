using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    public class RecognizeResult
    {
        /// <summary>
        /// Номер требования
        /// </summary>
        public string DemandNumber { get; set; }

        /// <summary>
        /// КНД распознанного поручения
        /// </summary>
        public string DemandKnd { get; set; }

        /// <summary>
        /// Дата из файла требования
        /// </summary>
        public DateOnly? DemandDate { get; set; }

        /// <summary>
        /// Срок отправки квитанции о приеме/отказе. Дата указывает, до какого числа нужно отправить квитанцию  
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Срок ответа на требование. Дата указывает, до какого числа нужно отправить ответ на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }

        /// <summary>
        /// Список ИНН, которые были перечислены в pdf-файле требования
        /// </summary>
        public string[] DemandInnList { get; set; }
    }
}