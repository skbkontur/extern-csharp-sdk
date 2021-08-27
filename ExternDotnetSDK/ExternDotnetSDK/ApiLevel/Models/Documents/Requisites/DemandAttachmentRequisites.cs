using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites
{
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        public string DemandNumber { get; set; }
        public DateOnly? DemandDate { get; set; }
        public string[] DemandInnList { get; set; }
    }
}