using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace For.Log
{

    public static class MiddlewareProvider
    {
        public static void UseLoggerMiddleware<TLogger>(this Owin.IAppBuilder app, TLogger logger) where TLogger : BaseLogger
        {
            TLogger loggerInstance = logger;
            app.Use(typeof(LoggerMiddleware<TLogger>), loggerInstance);
        }
    }

    public class LoggerMiddleware<T> where T : BaseLogger
    {
        private AppFunc next;
        private BaseLogger _logger;
        public LoggerMiddleware(AppFunc next, BaseLogger logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            string loggerName = typeof(T).Name;
            //建立紀錄器
            T loggProvider = (T)_logger;

            //放入容器
            environment.Add(loggerName, loggProvider);

            await next.Invoke(environment);

            //移除logger
            if (environment.ContainsKey(loggerName))
            {
                environment.Remove(loggerName);
            }

        }
    }
}
