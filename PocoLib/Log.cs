using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocoLib
{
    /// <summary>
    /// Log nesnesi, Log tablosundaki kayda ait alanları içerir
    /// </summary>
    public class Log
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public int LogType { get; set; }
        public DateTime LogDate { get; set; }
        public string Details { get; set; }
    }

    public enum LogType
    {
        Error = 1,
        Warning = 2,
        Information = 3,
        SuccessAudit = 4,
        FailureAudit = 5
    }
}
