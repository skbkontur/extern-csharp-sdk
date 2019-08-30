using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Requests
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentDescriptionRequestDto
    {
        public Urn Type { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
    }
}