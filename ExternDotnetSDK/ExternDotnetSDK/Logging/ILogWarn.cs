using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogWarn
    {
        bool IsWarnEnabled { get; set; }

        void Warn(object message);
        void Warn(string message);
        void Warn(Exception exc);
        void Warn(string message, Exception exc);

        [StringFormatMethod("format")]
        void Warn(string format, object arg0);

        [StringFormatMethod("format")]
        void Warn(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Warn(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Warn(string format, params object[] args);

        [StringFormatMethod("format")]
        void Warn(IFormatProvider provider, string format, params object[] args);
    }
}