using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public class UrnException : Exception
    {
        public UrnException(string message)
            : base(message)
        {
        }

        public UrnException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}