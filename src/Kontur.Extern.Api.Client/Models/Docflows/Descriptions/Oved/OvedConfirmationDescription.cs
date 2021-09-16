using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Oved
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OvedConfirmationDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public PfrRegNumber RegistrationNumber { get; set; } = null!;
        
        /// <summary>
        /// Дата начала отчетного периода, за который сдается отчет
        /// </summary>
        public DateOnly? PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается отчет
        /// </summary>
        public DateOnly? PeriodEnd { get; set; }
        
        /// <summary>
        /// ИНН организации, за которую сдается отчет
        /// </summary>
        public string PayerInn { get; set; } = null!;
        
        /// <summary>
        /// Код отчетного периода, за который сдается отчет
        /// </summary>
        public string PeriodCode { get; set; } = null!;
        
        /// <summary>
        /// Идентификатор отчета, выданный порталом ФСС
        /// </summary>
        public string RequestId { get; set; } = null!;
        
        /// <summary>
        /// Отпечаток сертификата отправителя
        /// </summary>
        public string SenderCertificateThumbprint { get; set; } = null!;
    }
}