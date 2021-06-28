// ReSharper disable ArrangeTypeMemberModifiers

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    // todo: split it into different types -- ContextTypes and HeaderNames
    public static class SenderConstants
    {
        public const string MediaType = "application/json";
        public const string AuthSidHeader = "auth.sid";
        public const string ApiKeyHeader = "X-Kontur-Apikey";
        public const string TimeoutHeader = "Timeout";
    }
}