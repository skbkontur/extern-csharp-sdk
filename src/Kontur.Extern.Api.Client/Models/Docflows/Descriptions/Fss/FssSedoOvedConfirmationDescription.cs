using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoOvedConfirmationDescription : FssSedoDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion? FormVersion { get; set; }
        
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
    public string? PayerInn { get; set; }
        
    /// <summary>
    /// Отпечаток сертификата отправителя
    /// </summary>
    public string? SenderCertificateThumbprint { get; set; }
        
    /// <summary>
    /// Идентификатор доверенности
    /// </summary>
    public Guid? WarrantId { get; set; }
}