using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CheckError
    {
        public string Description { get; set; } = null!;
        public string Source { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Tags { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}