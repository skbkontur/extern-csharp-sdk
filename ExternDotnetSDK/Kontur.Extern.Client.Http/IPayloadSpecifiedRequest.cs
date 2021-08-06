namespace Kontur.Extern.Client.Http
{
    public interface IPayloadSpecifiedRequest : IHttpRequest
    {
        IHttpRequest ContentRange(long from, long to, long? totalLength = null);
    }
}