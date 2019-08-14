using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogError
    {
        bool IsErrorEnabled { get; set; }

        void Error(object message);
        void Error(string message);
        void Error(Exception exc);
        void Error(string message, Exception exc);

        [StringFormatMethod("format")]
        void Error(string format, object arg0);

        [StringFormatMethod("format")]
        void Error(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Error(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Error(string format, params object[] args);

        [StringFormatMethod("format")]
        void Error(IFormatProvider provider, string format, params object[] args);
    }
}