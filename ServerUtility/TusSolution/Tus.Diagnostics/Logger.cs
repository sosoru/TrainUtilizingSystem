using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tus.Diagnostics
{
    public enum LoggingType
    {
        DEVICE,
        TRANS,
        WEB,
    }

    public static class Logger
    {
        static Logger()
        {
        }

        private static readonly Dictionary<LoggingType, DateTime> lastWritten
            = new Dictionary<LoggingType, DateTime>{
                {LoggingType.DEVICE, DateTime.MinValue},
                {LoggingType.TRANS, DateTime.MinValue},
                {LoggingType.WEB, DateTime.MinValue}
            };
        private static readonly object lockWriting = new object();

        public static void WriteLineWithFormat(LoggingType type, string str, params object[] args)
        {
            lock (lockWriting)
            {
                var content = string.Format(str, args);
                var header = string.Format("[{0} {1}] ", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), type.ToString());

                var now = DateTime.Now;
                if ((now - lastWritten[type]).TotalSeconds >= 60.0)
                {
                    var border = new string('-', 15);
                    Trace.WriteLine(header + border);
                }
                Trace.WriteLine(header + content);
                lastWritten[type] = now;
            }
        }

        public static void WriteLineAsDeviceInfo(string msg, params object[] args)
        {
            WriteLineWithFormat(LoggingType.DEVICE, msg, args);
        }
        public static void WriteLineAsTransInfo(string msg, params object[] args)
        {
            WriteLineWithFormat(LoggingType.TRANS, msg, args);
        }
        public static void WriteLineAsWebInfo(string msg, params object[] args)
        {
            WriteLineWithFormat(LoggingType.WEB, msg, args);
        }
    }
}