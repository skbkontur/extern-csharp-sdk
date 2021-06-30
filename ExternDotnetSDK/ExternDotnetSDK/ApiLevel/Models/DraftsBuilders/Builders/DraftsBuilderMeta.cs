using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderMeta
    {
        [Required]
        [DataMember]
        public Sender Sender { get; set; }

        [Required]
        [DataMember]
        public AccountInfo Payer { get; set; }

        [Required]
        [DataMember]
        public RecipientInfo Recipient { get; set; }

        [Required]
        [DataMember]
        public Urn BuilderType { get; set; }

        [Required]
        [DataMember]
        public DraftsBuilderData BuilderData { get; set; }
    }
}