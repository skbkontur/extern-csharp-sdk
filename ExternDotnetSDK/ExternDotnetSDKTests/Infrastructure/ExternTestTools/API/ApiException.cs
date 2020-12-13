using System;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.API
{
    public class ApiException : Exception
    {
        public ApiException()
        {
        }

        public ApiException(int errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public ApiException(int errorCode, string message, dynamic errorContent = null)
            : base(message)
        {
            ErrorCode = errorCode;
            ErrorContent = errorContent;
        }

        public int ErrorCode { get; set; }
        public dynamic ErrorContent { get; }
    }
}