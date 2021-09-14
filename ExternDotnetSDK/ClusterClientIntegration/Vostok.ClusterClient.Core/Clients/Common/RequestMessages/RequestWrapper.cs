using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Common.RequestMessages;
using Vostok.Clusterclient.Core.Model;
using StreamContent = System.Net.Http.StreamContent;

namespace Kontur.Extern.Api.Client.Vostok.Vostok.ClusterClient.Core.Clients.Common.RequestMessages
{
    public class RequestWrapper : IRequestMessage
    {
        private readonly Request request;

        public RequestWrapper(Request request) => this.request = request;

        public Dictionary<string, string> Headers =>
            request.Headers?.ToDictionary(x => x.Name, x => x.Value) ?? new Dictionary<string, string>();

        public HttpContent Content =>
            request.Content != null
                ? new ByteArrayContent(request.Content.ToArray())
                : request.StreamContent != null
                    ? (HttpContent)new StreamContent(request.StreamContent.Stream)
                    : request.CompositeContent != null
                        ? new ByteArrayContent(request.CompositeContent.Parts.SelectMany(x => x.ToArray()).ToArray())
                        : new StringContent(string.Empty);

        public HttpMethod Method => new HttpMethod(request.Method.ToUpperInvariant());

        public Uri Uri => request.Url;
    }
}