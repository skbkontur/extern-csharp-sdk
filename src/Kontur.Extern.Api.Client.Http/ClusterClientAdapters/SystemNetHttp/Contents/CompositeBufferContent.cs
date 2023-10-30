using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents
{
    internal class CompositeBufferContent : GenericContent
    {
        private readonly CompositeContent content;
        private readonly CancellationToken cancellationToken;

        public CompositeBufferContent(CompositeContent content, CancellationToken cancellationToken)
        {
            this.content = content;
            this.cancellationToken = cancellationToken;

            Headers.ContentLength = content.Length;
        }

        public override long? Length =>
            content.Length;

        public override Stream AsStream =>
            new CompositeStream(content.Parts.Select(part => part.ToMemoryStream()), content.Length);

        public override async Task Copy(Stream target)
        {
            foreach (var part in content.Parts)
                await new BufferContent(part, cancellationToken, false).Copy(target).ConfigureAwait(false);
        }
    }
}