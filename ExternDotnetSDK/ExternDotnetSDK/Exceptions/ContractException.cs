using System;
using System.Runtime.Serialization;

namespace Kontur.Extern.Client.Exceptions
{
    [Serializable]
    public class ContractException : Exception
    {
        public ContractException(string message)
            : base(message)
        {
        }

        public ContractException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ContractException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}