using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For.Log
{
    public abstract class BaseLogger
    {

        private LoggerProperty _loggerProperty;
        private object lockObject = new object();


        public BaseLogger(LoggerProperty LoggerProperty)
        {
            _loggerProperty = LoggerProperty;
        }


        public virtual void WriteLogAtEndRequest(string log)
        {

        }

        public void Fatal(string log)
        {
            WriteLog(LogLevel.Fatal, () => WriteFatalAsync(log, GetLogInfo()));
        }

        public void Error(string log)
        {
            WriteLog(LogLevel.Error, () => WriteErrorAsync(log, GetLogInfo()));
        }

        public void Warn(string log)
        {
            WriteLog(LogLevel.Warn, () => WriteWarnAsync(log, GetLogInfo()));
        }

        public void Info(string log)
        {
            WriteLog(LogLevel.Info, () => WriteInfoAsync(log, GetLogInfo()));
        }

        public void Debug(string log)
        {
            WriteLog(LogLevel.Debug, () => WriteDebugAsync(log, GetLogInfo()));
        }

        public void Trace(string log)
        {
            WriteLog(LogLevel.Trace, () => WriteTraceAsync(log, GetLogInfo()));
        }

        private LogInfo GetLogInfo()
        {
            return new LogInfo()
            {
                LogTime = DateTime.Now,
                envStackTrace = Environment.StackTrace,
                stackTrace = new System.Diagnostics.StackTrace()
            };
        }
        private void WriteLog(LogLevel level, Action write)
        {
            if ((_loggerProperty.Level & level) > 0)
            {
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    lock (lockObject)
                    {
                        write();
                    }
                });
                th.Start();
            }
        }

        protected abstract void WriteFatalAsync(string log, LogInfo logInfo);
        protected abstract void WriteErrorAsync(string log, LogInfo logInfo);
        protected abstract void WriteWarnAsync(string log, LogInfo logInfo);
        protected abstract void WriteInfoAsync(string log, LogInfo logInfo);
        protected abstract void WriteDebugAsync(string log, LogInfo logInfo);
        protected abstract void WriteTraceAsync(string log, LogInfo logInfo);
    }
}
