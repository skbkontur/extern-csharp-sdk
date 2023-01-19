using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Sfr
{
    [PublicAPI]
    public class SfrReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        /// <summary>
        /// Код УПФР для отправки отчета в СФР
        /// </summary>
        public string Recipient { get; set; } = null!;
        /// <summary>
        /// Регистрационный номер страхователя
        /// </summary>
        public PfrRegNumber RegistrationNumber { get; set; } = null!;
        /// <summary>
        /// ИНН организации, за которую сдается отчет
        /// </summary>
        public string PayerInn { get; set; } = null!;
        /// <summary>
        /// Код УПФР для отправки отчета в СФР
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        /// <summary>
        /// Отпечаток сертификата отправителя
        /// </summary>
        public string SenderCertificateThumbprint { get; set; } = null!;
        /// <summary>
        /// Информация о разделах
        /// </summary>
        public SfrSectionInfo[] SectionInfos { get; set; } = null!;
    }

    /// <summary>
    /// Информация о разделе отчета в СФР
    /// </summary>
    [PublicAPI]
    public class SfrSectionInfo
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        /// <summary>
        /// Тип корректировки
        /// </summary>
        public Urn CorrectionType { get; set; } = null!;
        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int? CorrectionNumber { get; set; }
        /// <summary>
        /// Дата и время начала периода. Формат ISO8601 – YYYY-MM-DDThh:mm:ss[.SSSSSS].
        /// </summary>
        public DateTime? PeriodBegin { get; set; }
        /// <summary>
        /// Дата и время конца периода. Формат ISO8601 – YYYY-MM-DDThh:mm:ss[.SSSSSS].
        /// </summary>
        public DateTime? PeriodEnd { get; set; }
        /// <summary>
        /// Дата и время начала периода корректировки. Формат ISO8601 – YYYY-MM-DDThh:mm:ss[.SSSSSS].
        /// </summary>
        public DateTime? CorrectionPeriodBegin { get; set; }
        /// <summary>
        /// Дата и время конца периода корректировки. Формат ISO8601 – YYYY-MM-DDThh:mm:ss[.SSSSSS].
        /// </summary>
        public DateTime? CorrectionPeriodEnd { get; set; }
    }
}