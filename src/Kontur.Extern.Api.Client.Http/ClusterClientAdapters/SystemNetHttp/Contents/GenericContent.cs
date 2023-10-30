using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Exceptions;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Contents
{
    internal abstract class GenericContent : HttpContent
    {
        public abstract long? Length { get; }

        public abstract Stream AsStream { get; }

        public abstract Task Copy(Stream target);

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            try
            {
                await Copy(stream).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (StreamAlreadyUsedException)
            {
                throw;
            }
            catch (UserStreamException)
            {
                throw;
            }
            catch (Exception error)
            {
                throw new BodySendException($"Failed to send body content of type '{GetType().Name}' with length {Length?.ToString() ?? "???"}.", error);
            }
        }

        protected override Task<Stream> CreateContentReadStreamAsync()
        {
            return Task.FromResult<Stream>(new UserStreamWrapper(AsStream));
        }

        protected override bool TryComputeLength(out long length)
        {
            var localLength = Length;

            length = localLength ?? 0L;

            return localLength.HasValue;
        }
    }
}
