using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace For.Log
{
    public class LoggerProvider<T>
    {
        /// <summary>
        /// 取得紀錄器
        /// </summary>
        /// <returns></returns>
        public static T GetLogger()
        {
            HttpContext _context = HttpContext.Current;
            if (_context.GetOwinContext().Environment.ContainsKey(typeof(T).Name))
            {
                return (T)_context.GetOwinContext().Environment[typeof(T).Name];
            }
            else
            {
                return default(T);
            }
        }

    }

}
