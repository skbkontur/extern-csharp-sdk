using System;
using ExternDotnetSDK.Logging;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Common
{
    public class FakeLogError : ILogError
    {
        public bool IsErrorEnabled { get; set; }

        public void Error(object message)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(Exception exc)
        {
        }

        public void Error(string message, Exception exc)
        {
        }

        public void Error(string format, object arg0)
        {
        }

        public void Error(string format, object arg0, object arg1)
        {
        }

        public void Error(string format, object arg0, object arg1, object arg2)
        {
        }

        public void Error(string format, params object[] args)
        {
        }

        public void Error(IFormatProvider provider, string format, params object[] args)
        {
        }
    }
}