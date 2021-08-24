using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class Certificate
    {
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}