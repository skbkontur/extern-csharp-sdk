using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Kontur.Extern.Client.Clients.Common.RequestMessages;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;
using Kontur.Extern.Client.Vostok.Vostok.ClusterClient.Core.Clients.Common.RequestMessages;
using Vostok.Clusterclient.Core.Model;
using StreamContent = System.Net.Http.StreamContent;

namespace Kontur.Extern.Client.Vostok.Vostok.ClusterClient.Core.Clients.Common.ResponseMessages
{
    public class ClusterResultWrapper : IResponseMessage
    {
        private readonly ClusterResult result;

        public ClusterResultWrapper(ClusterResult result) => this.result = result;

        public HttpContent Content => result.Response.HasStream
            ? (HttpContent)new StreamContent(result.Response.Stream)
            : new ByteArrayContent(result.Response.Content.ToArray());

        public Dictionary<string, string> Headers => result.Response.HasHeaders
            ? result.Response.Headers.ToDictionary(x => x.Name, x => x.Value)
            : new Dictionary<string, string>();

        public HttpStatusCode StatusCode => (HttpStatusCode)(int)result.Response.Code;
        public IRequestMessage Request => new RequestWrapper(result.Request);
        public string ReasonPhrase => $"{result.Status} : {result.Response.Code}";

        public IResponseMessage EnsureSuccessStatusCode()
        {
            result.Response.EnsureSuccessStatusCode();
            return this;
        }
    }
}