using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using KeApiOpenSdk.Clients.Common.RequestMessages;
using KeApiOpenSdk.Clients.Common.ResponseMessages;
using Vostok.Clusterclient.Core.Model;

namespace ClusterClientIntegration.Common
{
    public class ClusterResultWrapper : IResponseMessage
    {
        private readonly ClusterResult result;

        public ClusterResultWrapper(ClusterResult result) => this.result = result;

        public HttpContent Content =>
            result.Response.HasStream
                ? (HttpContent)new System.Net.Http.StreamContent(result.Response.Stream)
                : result.Response.HasContent
                    ? new ByteArrayContent(result.Response.Content.ToArray())
                    : new StringContent(string.Empty);

        public Dictionary<string, string> Headers => result.Response.HasHeaders
            ? result.Response.Headers.ToDictionary(x => x.Name, x => x.Value)
            : new Dictionary<string, string>();

        public HttpStatusCode StatusCode => (HttpStatusCode)(int)result.Response.Code;
        public IRequestMessage Request => new RequestWrapper(result.Request);
        public string ReasonPhrase => $"{result.Status.ToString()} : {result.Response.Code.ToString()}";

        public IResponseMessage EnsureSuccessStatusCode()
        {
            result.Response.EnsureSuccessStatusCode();
            return this;
        }
    }
}