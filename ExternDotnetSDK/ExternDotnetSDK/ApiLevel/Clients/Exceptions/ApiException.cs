using System;
using System.Runtime.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Clients.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ApiException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}