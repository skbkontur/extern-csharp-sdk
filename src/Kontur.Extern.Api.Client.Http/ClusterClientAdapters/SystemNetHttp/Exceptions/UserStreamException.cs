using System;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Exceptions
{
    internal class UserStreamException : Exception
    {
        public UserStreamException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}