using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogFatal
    {
        bool IsFatalEnabled { get; set; }

        void Fatal(object message);
        void Fatal(string message);
        void Fatal(Exception exc);
        void Fatal(string message, Exception exc);

        [StringFormatMethod("format")]
        void Fatal(string format, object arg0);

        [StringFormatMethod("format")]
        void Fatal(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Fatal(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Fatal(string format, params object[] args);

        [StringFormatMethod("format")]
        void Fatal(IFormatProvider provider, string format, params object[] args);
    }
}