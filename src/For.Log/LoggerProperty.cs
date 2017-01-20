using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For.Log
{
    public class LoggerProperty
    {
        public LogLevel Level { get; set; }
    }
    public enum LogLevel
    {
        Fatal = 1,  //Highest level: important stuff down
        Error = 2,  //For example application crashes / exceptions.
        Warn = 4,   //Incorrect behavior but the application can continue
        Info = 8,   //Normal behavior like mail sent, user updated profile etc.
        Debug = 16,  //Executed queries, user authenticated, session expired
        Trace = 32   //Begin method X, end method X etc
    }
}
