using JetBrains.Annotations;

namespace ExternDotnetSDK.Common
{
    public class Content
    {
        [UsedImplicitly]
        public Link Decrypted { get; set; }

        public Link Encrypted { get; set; }
    }
}