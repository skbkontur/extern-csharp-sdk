using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoResultDescription: FssSedoDescription
    {
        /// <summary>
        /// Идентификатор исходящего документооборота, в ответ на который был сформирован ответ от ФСС
        /// </summary>
        public string? RelatedDocflowId { get; set; }
    }
}