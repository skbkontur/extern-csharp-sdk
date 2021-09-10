using System;

namespace Kontur.Extern.Client.Testing.ExternTestTool.LegacyExternTestTools.API
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