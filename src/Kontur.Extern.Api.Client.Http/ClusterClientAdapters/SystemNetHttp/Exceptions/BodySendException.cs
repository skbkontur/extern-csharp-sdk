using System;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Exceptions
{
    internal class BodySendException : Exception
    {
        public BodySendException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
