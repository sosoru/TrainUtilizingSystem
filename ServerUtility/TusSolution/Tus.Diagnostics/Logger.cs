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

        public static void WriteLineWithFormat(LoggingType type, string str, params object[] args)
        {
            var content = string.Format(str, args);
            var all = string.Format("[{0} {1}] {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), type.ToString(),
                                    content);
            Trace.WriteLine(all);
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