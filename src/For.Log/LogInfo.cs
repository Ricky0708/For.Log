using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For.Log
{
    public class LogInfo
    {
        public DateTime LogTime { get; set; }  
        public StackTrace stackTrace { get; set; }
        public string envStackTrace { get; set; }

    }
}
