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


        public BaseLogger(LoggerProperty LoggerProperty)
        {
            _loggerProperty = LoggerProperty;
        }

        private object lockObject = new object();

        public virtual void WriteLogAtEndRequest(string log)
        {

        }

        public void Fatal(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Fatal) > 0)
            {
                WriteLog(() => WriteFatalAsync(log, GetLogInfo()));
            }
        }

        public void Error(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Error) > 0)
            {
                WriteLog(() => WriteErrorAsync(log, GetLogInfo()));
            }
        }

        public void Warn(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Warn) > 0)
            {
                WriteLog(() => WriteWarnAsync(log, GetLogInfo()));
            }
        }
        public void Info(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Info) > 0)
            {
                WriteLog(() => WriteInfoAsync(log, GetLogInfo()));
            }
        }
        public void Debug(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Debug) > 0)
            {
                WriteLog(() => WriteDebugAsync(log, GetLogInfo()));
            }
        }
        public void Trace(string log)
        {
            if ((_loggerProperty.Level & LogLevel.Trace) > 0)
            {
                WriteLog(() => WriteTraceAsync(log, GetLogInfo()));
            }
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
        private void WriteLog(Action write)
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

        protected abstract void WriteFatalAsync(string log, LogInfo logInfo);
        protected abstract void WriteErrorAsync(string log, LogInfo logInfo);
        protected abstract void WriteWarnAsync(string log, LogInfo logInfo);
        protected abstract void WriteInfoAsync(string log, LogInfo logInfo);
        protected abstract void WriteDebugAsync(string log, LogInfo logInfo);
        protected abstract void WriteTraceAsync(string log, LogInfo logInfo);
    }
}
