using System;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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
        [JsonConverter(typeof (DateFormat))]
        public DateTime? DemandDate { get; set; }

        /// <summary>
        /// Срок отправки квитанции о приеме/отказе. Дата указывает, до какого числа нужно отправить квитанцию  
        /// </summary>
        [JsonConverter(typeof (DateFormat))]
        public DateTime? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Срок ответа на требование. Дата указывает, до какого числа нужно отправить ответ на требование
        /// </summary>
        [JsonConverter(typeof (DateFormat))]
        public DateTime? ReplyDeadlineDate { get; set; }

        /// <summary>
        /// Список ИНН, которые были перечислены в pdf-файле требования
        /// </summary>
        public string[] DemandInnList { get; set; }
    }
}