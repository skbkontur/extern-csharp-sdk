using System.Net;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp;

public class HttpClientTransportSettings
{
    public IWebProxy? Proxy { get; set; }
}