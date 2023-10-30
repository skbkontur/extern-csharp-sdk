using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.BodyReading;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Helpers;
using Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Messages;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Transport;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp;

public class HttpClientTransport : ITransport
{
    private readonly ILog log;
    private HttpClient httpClient;
    private IBodyReader bodyReader;
    private TimeoutProvider timeoutProvider;

    public HttpClientTransport(HttpClientTransportSettings settings, ILog log)
    {
        this.log = log;
        httpClient = new HttpClient(
            new HttpClientHandler
            {
                Proxy = settings.Proxy
            });
        bodyReader = new BodyReader(size => new byte[size], _ => false, () => null, log);
        timeoutProvider = new TimeoutProvider(TimeSpan.FromMilliseconds(250), log);
    }

    public Task<Response> SendAsync(Request request, TimeSpan? connectionTimeout, TimeSpan timeout, CancellationToken cancellationToken)
        => timeoutProvider.SendWithTimeoutAsync((r, t) => SendAsync(r, connectionTimeout, t), request, timeout, cancellationToken);

    private async Task<Response> SendAsync(Request request, TimeSpan? connectionTimeout, CancellationToken cancellationToken)
    {
        var httpRequest = RequestMessageFactory.Create(request, cancellationToken);
        var response = await httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        var bodyReadResult = await bodyReader.ReadAsync(response, cancellationToken).ConfigureAwait(false);

        if (bodyReadResult.ErrorCode.HasValue)
            return new Response(bodyReadResult.ErrorCode.Value);

        return ResponseMessageConverter.Convert(response, bodyReadResult.Content, bodyReadResult.Stream);
    }

    public TransportCapabilities Capabilities
        => TransportCapabilities.RequestCompositeBody |
           TransportCapabilities.RequestStreaming |
           TransportCapabilities.ResponseStreaming;
}