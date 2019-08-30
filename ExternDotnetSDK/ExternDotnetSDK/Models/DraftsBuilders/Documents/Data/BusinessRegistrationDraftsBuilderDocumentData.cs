using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class BusinessRegistrationDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        public string SvdregCode { get; set; }
    }
}