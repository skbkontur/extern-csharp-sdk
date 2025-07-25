﻿using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoAdditionalVacationStatementDocsDescription: FssSedoDescription
{
    /// <summary>
    /// ИНН организации, за которую сдается отчет
    /// </summary>
    public string PayerInn { get; set; }

    /// <summary>
    /// Отпечаток сертификата отправителя
    /// </summary>
    public string SenderCertificateThumbprint { get; set; }

    /// <summary>
    /// Идентификатор доверенности
    /// </summary>
    public Guid? WarrantId { get; set; }

    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion FormVersion { get; set; }
        
    /// <summary>
    /// Идентификатор связанного документооборота 
    /// </summary>
    public Guid? RelatedDocflowId { get; set; }

    /// <summary>
    ///  Идентификатор документа в связанном документообороте
    /// </summary>
    public Guid? RelatedDocumentId { get; set; }
    
    /// <summary>
    ///  Номер извещения запроса дополнительных документов
    /// </summary>
    public string NoticeNumber { get; set; }
}