using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.HttpLevel
{
    public interface IHttpContent
    {
        Request Apply(Request request, IJsonSerializer serializer);
    }
}