using System;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss.Enums;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

public class FssWarrantManagementResultDescription : FssSedoResultDescription
{
    /// <summary>
    /// Идентификатор доверенности, в отношении которой инициирована операция выпуска/отзыва
    /// </summary>
    public Guid? MainWarrantId { get; set; }

    /// <summary>
    /// ИНН организации, за которую сдается отчет
    /// </summary>
    public string PayerInn { get; set; }

    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion FormVersion { get; set; }

    /// <summary>
    /// Тип операции с доверенностью
    /// </summary>
    public FssWarrantManagementActionType WarrantManagementActionType { get; set; }
}