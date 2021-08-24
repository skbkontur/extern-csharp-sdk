using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public class Content
    {
        [UsedImplicitly]
        public Link Decrypted { get; set; }

        public Link Encrypted { get; set; }
    }
}