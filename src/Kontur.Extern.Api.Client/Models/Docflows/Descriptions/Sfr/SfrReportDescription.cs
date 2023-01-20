using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
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
        public SfrReportCorrectionType? CorrectionType { get; set; }
        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int? CorrectionNumber { get; set; }
        /// <summary>
        /// Дата и время начала периода
        /// </summary>
        public DateTime? PeriodBegin { get; set; }
        /// <summary>
        /// Дата и время конца периода
        /// </summary>
        public DateTime? PeriodEnd { get; set; }
        /// <summary>
        /// Дата и время начала периода корректировки
        /// </summary>
        public DateTime? CorrectionPeriodBegin { get; set; }
        /// <summary>
        /// Дата и время конца периода корректировки
        /// </summary>
        public DateTime? CorrectionPeriodEnd { get; set; }
    }
}