using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Http
{
    public interface IHttpContent
    {
        long? Length { get; }
        
        Request Apply(Request request, IJsonSerializer serializer);
    }
}