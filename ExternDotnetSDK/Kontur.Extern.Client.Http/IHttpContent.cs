using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Http
{
    public interface IHttpContent
    {
        long? Length { get; }
        
        Request Apply(Request request, IJsonSerializer serializer);
    }
}