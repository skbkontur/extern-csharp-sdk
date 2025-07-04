using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;

[PublicAPI]
public class FssSedoAdditionalVacationStatementNeedDocDescription: FssSedoDescription
{
    /// <summary>
    /// Версия формы документа
    /// </summary>
    public FormVersion FormVersion { get; set; }
    
    /// <summary>
    ///  Номер извещения 
    /// </summary>
    public string NoticeNumber { get; set; }
}