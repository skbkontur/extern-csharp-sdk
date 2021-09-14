namespace Kontur.Extern.Api.Client.Http
{
    public interface IPayloadSpecifiedRequest : IHttpRequest
    {
        IHttpRequest ContentRange(long from, long to, long? totalLength = null);
    }
}