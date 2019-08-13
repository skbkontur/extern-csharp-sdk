using System;

namespace ExternDotnetSDK.Logging
{
    public class FakeLog : ILog
    {
        public void Info(object message){}
        public void Info(string message){}
        public void Info(Exception exc){}
        public void Info(string message, Exception exc){}
        public void Info(string format, params object[] args){}
        public void Info(IFormatProvider provider, string format, params object[] args){}
        public void Warn(object message){}
        public void Warn(string message){}
        public void Warn(Exception exc){}
        public void Warn(string message, Exception exc){}
        public void Warn(string format, params object[] args){}
        public void Warn(IFormatProvider provider, string format, params object[] args){}
        public void Debug(object message){}
        public void Debug(string message){}
        public void Debug(Exception exc){}
        public void Debug(string message, Exception exc){}
        public void Debug(string format, params object[] args){}
        public void Debug(IFormatProvider provider, string format, params object[] args){}
        public void Error(object message){}
        public void Error(string message){}
        public void Error(Exception exc){}
        public void Error(string message, Exception exc){}
        public void Error(string format, params object[] args){}
        public void Error(IFormatProvider provider, string format, params object[] args){}
        public void Fatal(object message){}
        public void Fatal(string message){}
        public void Fatal(Exception exc){}
        public void Fatal(string message, Exception exc){}
        public void Fatal(string format, params object[] args){}
        public void Fatal(IFormatProvider provider, string format, params object[] args){}

        public bool IsInfoEnabled { get; set; }
        public bool IsWarnEnabled { get; set; }
        public bool IsDebugEnabled { get; set; }
        public bool IsErrorEnabled { get; set; }
        public bool IsFatalEnabled { get; set; }
    }
}