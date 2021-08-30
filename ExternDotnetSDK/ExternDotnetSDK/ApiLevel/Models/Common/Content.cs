using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Content
    {
        public Link Decrypted { get; set; }
        public Link Encrypted { get; set; }
    }
}