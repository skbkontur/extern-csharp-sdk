using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class IonDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых направляется запрос
        /// </summary>
        public string PayerInn { get; set; } = null!;
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; } = null!;
        
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
        /// <summary>
        /// Тип запроса ИОН
        /// </summary>
        public string ServiceCode { get; set; } = null!;
    }
}