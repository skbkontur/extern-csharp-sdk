using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Contents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UploadChunkResponse
    {
        public ContentResponse Content { get; set; }
        public bool IsCompleted { get; set; }
    }
}