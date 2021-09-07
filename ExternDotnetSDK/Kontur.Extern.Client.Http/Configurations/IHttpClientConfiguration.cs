using Vostok.Clusterclient.Core;

namespace Kontur.Extern.Client.Http.Configurations
{
    public interface IHttpClientConfiguration
    {
        void Apply(IClusterClientConfiguration config);
    }
}