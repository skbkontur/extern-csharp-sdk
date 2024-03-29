﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RecognizeResult
    {
        /// <summary>
        /// Номер требования
        /// </summary>
        public string DemandNumber { get; set; } = null!;

        /// <summary>
        /// КНД распознанного поручения
        /// </summary>
        public Knd DemandKnd { get; set; } = null!;
        
        /// <summary>
        /// Дата из файла требования
        /// </summary>
        public DateOnly? DemandDate { get; set; }
        
        /// <summary>
        /// Срок отправки квитанции о приеме\отказе. Дата указывает, до какого числа нужно отправить квитанцию  
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }
        
        /// <summary>
        /// Срок ответа на требование. Дата указывает, до какого числа нужно отправить ответ на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }
        
        /// <summary>
        /// Список ИНН, которые были перечислены в pdf-файле требования
        /// </summary>
        public string[] DemandInnList { get; set; } = null!;
    }
}