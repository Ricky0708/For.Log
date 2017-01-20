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

        public LoggerProperty LoggerProperty { get; set; }
        private object lockObject = new object();

        public virtual void WriteLogAtEndRequest(string log)
        {

        }

        public void Fatal(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Fatal) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => WriteFatalAsync(log, stackTrace, envStackTrace));
            }
        }
        public void Error(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Error) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => WriteErrorAsync(log, stackTrace, envStackTrace));
            }
        }
        public void Warn(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Warn) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => WriteWarnAsync(log, stackTrace, envStackTrace));
            }
        }
        public void Info(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Info) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => WriteInfoAsync(log, stackTrace, envStackTrace));
            }
        }
        public void Debug(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Debug) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => { WriteDebugAsync(log, stackTrace, envStackTrace); });
            }
        }
        public void Trace(string log)
        {
            if ((LoggerProperty.Level & LogLevel.Trace) > 0)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                var envStackTrace = Environment.StackTrace;
                WriteLog(() => WriteTraceAsync(log, stackTrace,envStackTrace));
            }
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

        protected abstract void WriteFatalAsync(string log, StackTrace stackTrace, string envStackTrace);
        protected abstract void WriteErrorAsync(string log, StackTrace stackTrace, string envStackTrace);
        protected abstract void WriteWarnAsync(string log, StackTrace stackTrace, string envStackTrace);
        protected abstract void WriteInfoAsync(string log, StackTrace stackTrace, string envStackTrace);
        protected abstract void WriteDebugAsync(string log, StackTrace stackTrace, string envStackTrace);
        protected abstract void WriteTraceAsync(string log, StackTrace stackTrace, string envStackTrace);
    }
}
