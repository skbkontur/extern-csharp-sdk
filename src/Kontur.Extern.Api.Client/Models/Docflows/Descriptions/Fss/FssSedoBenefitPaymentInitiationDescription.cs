using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoBenefitPaymentInitiationDescription: FssSedoDescription
    {
        /// <summary>
        /// Отпечаток сертификата отправителя
        /// </summary>
        public string? SenderCertificateThumbprint { get; set; }

        /// <summary>
        /// ИНН организации, за которую сдается отчет
        /// </summary>
        public string? PayerInn { get; set; }

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }
    }
}