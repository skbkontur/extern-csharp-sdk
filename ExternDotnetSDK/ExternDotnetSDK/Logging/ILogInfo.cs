using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILogInfo
    {
        bool IsInfoEnabled { get; set; }

        void Info(object message);
        void Info(string message);
        void Info(Exception exc);
        void Info(string message, Exception exc);

        [StringFormatMethod("format")]
        void Info(string format, object arg0);

        [StringFormatMethod("format")]
        void Info(string format, object arg0, object arg1);

        [StringFormatMethod("format")]
        void Info(string format, object arg0, object arg1, object arg2);

        [StringFormatMethod("format")]
        void Info(string format, params object[] args);

        [StringFormatMethod("format")]
        void Info(IFormatProvider provider, string format, params object[] args);
    }
}