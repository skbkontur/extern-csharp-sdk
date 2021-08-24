using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    public class SignInitResult
    {
        public Link[] Links { get; set; }
        public Link[] DocumentsToSign { get; set; }
        public string TaskId { get; set; }
    }
}