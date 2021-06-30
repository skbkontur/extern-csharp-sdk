using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class BusinessRegistrationDraftsBuilderFileData : DraftsBuilderFileData
    {
    }
}