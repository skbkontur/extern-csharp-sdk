using System;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class OvedConfirmationDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Дата начала отчетного периода, за который сдается отчет
        /// </summary>
        public DateTime? PeriodBegin { get; set; }
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается отчет
        /// </summary>
        public DateTime? PeriodEnd { get; set; }
        /// <summary>
        /// ИНН организации, за которую сдается отчет
        /// </summary>
        public string PayerInn { get; set; }
        /// <summary>
        /// Код отчетного периода, за который сдается отчет
        /// </summary>
        public string PeriodCode { get; set; }
        /// <summary>
        /// Идентификатор отчета, выданный порталом ФСС
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Отпечаток сертификата отправителя
        /// </summary>
        public string SenderCertificateThumbprint { get; set; }
    }
}