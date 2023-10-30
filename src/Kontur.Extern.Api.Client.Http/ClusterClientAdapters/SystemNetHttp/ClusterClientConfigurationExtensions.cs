using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp;

public static class ClusterClientConfigurationExtensions
{
    public static void SetupHttpClientTransport(this IClusterClientConfiguration self, HttpClientTransportSettings settings)
        => self.Transport = new HttpClientTransport(settings, self.Log);
}