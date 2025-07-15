using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssWarrantManagementDescription : FssSedoDescription
{
    /// <summary>
    /// Идентификатор доверенности, в отношении которой инициирована операция выпуска/отзыва
    /// </summary>
    public Guid? MainWarrantId { get; set; }

    /// <summary>
    /// Тип операции с доверенностью. Возможные значения - unknown, issuance, revocation
    /// </summary>
    public string WarrantManagementActionType { get; set; }

    /// <summary>
    /// Отпечаток сертификата отправителя
    /// </summary>
    public string SenderCertificateThumbprint { get; set; }

    /// <summary>
    /// ИНН организации, за которую сдается отчет
    /// </summary>
    public string PayerInn { get; set; }

    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion FormVersion { get; set; }
}