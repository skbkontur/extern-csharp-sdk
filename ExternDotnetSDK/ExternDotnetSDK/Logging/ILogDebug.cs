using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogDebug
    {
        bool IsDebugEnabled { get; set; }

        void Debug(object message);
        void Debug(string message);
        void Debug(Exception exc);
        void Debug(string message, Exception exc);

        [StringFormatMethod("format")]
        void Debug(string format, object arg0);

        [StringFormatMethod("format")]
        void Debug(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Debug(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Debug(string format, params object[] args);

        [StringFormatMethod("format")]
        void Debug(IFormatProvider provider, string format, params object[] args);
    }
}