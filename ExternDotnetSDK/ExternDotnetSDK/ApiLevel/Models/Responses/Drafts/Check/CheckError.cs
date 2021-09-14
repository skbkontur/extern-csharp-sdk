using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CheckError
    {
        public string Description { get; set; }
        public string Source { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string Tags { get; set; }
        public string Id { get; set; }
    }
}