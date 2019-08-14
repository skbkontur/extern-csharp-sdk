using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogTrace
    {
        bool IsTraceEnabled { get; set; }

        void Trace(object message);
        void Trace(string message);
        void Trace(Exception exc);
        void Trace(string message, Exception exc);

        [StringFormatMethod("format")]
        void Trace(string format, object arg0);

        [StringFormatMethod("format")]
        void Trace(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Trace(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Trace(string format, params object[] args);

        [StringFormatMethod("format")]
        void Trace(IFormatProvider provider, string format, params object[] args);
    }
}