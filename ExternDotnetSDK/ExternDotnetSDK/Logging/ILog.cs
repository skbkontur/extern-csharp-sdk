using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Logging
{
    public interface ILog
    {
        void Info(object message);
        void Info(string message);
        void Info(Exception exc);
        void Info(string message, Exception exc);
        [StringFormatMethod("format")] void Info(string format, params object[] args);
        [StringFormatMethod("format")] void Info(IFormatProvider provider, string format, params object[] args);

        void Warn(object message);
		void Warn(string message);
        void Warn(Exception exc);
        void Warn(string message, Exception exc);
		[StringFormatMethod("format")] void Warn(string format, params object[] args);
		[StringFormatMethod("format")] void Warn(IFormatProvider provider, string format, params object[] args);

		void Debug(object message);
		void Debug(string message);
        void Debug(Exception exc);
        void Debug(string message, Exception exc);
		[StringFormatMethod("format")] void Debug(string format, params object[] args);
		[StringFormatMethod("format")] void Debug(IFormatProvider provider, string format, params object[] args);

		void Error(object message);
		void Error(string message);
        void Error(Exception exc);
        void Error(string message, Exception exc);
		[StringFormatMethod("format")] void Error(string format, params object[] args);
		[StringFormatMethod("format")] void Error(IFormatProvider provider, string format, params object[] args);

		void Fatal(object message);
		void Fatal(string message);
        void Fatal(Exception exc);
        void Fatal(string message, Exception exc);
		[StringFormatMethod("format")] void Fatal(string format, params object[] args);
		[StringFormatMethod("format")] void Fatal(IFormatProvider provider, string format, params object[] args);

		bool IsInfoEnabled { get; set; }
		bool IsWarnEnabled { get; set; }
		bool IsDebugEnabled { get; set; }
		bool IsErrorEnabled { get; set; }
		bool IsFatalEnabled { get; set; }
    }
}