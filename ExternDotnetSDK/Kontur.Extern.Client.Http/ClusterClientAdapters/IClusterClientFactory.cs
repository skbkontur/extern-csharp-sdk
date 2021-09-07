using Vostok.Clusterclient.Core;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    public interface IClusterClientFactory
    {
        IClusterClient Create(ILog log);
    }
}