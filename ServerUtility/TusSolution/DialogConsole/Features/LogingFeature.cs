using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Tus.Diagnostics;

namespace DialogConsole.Features.Base
{
    [FeatureMetadata("l", "Manage Loging Windows", IsShown=false)]
    [Export(typeof(IFeature))]
    class LogingFeature
            : BaseFeature, IFeature
    {
        private const string LOGING_PIPE_NAME = "Tus.Diagnostics.Logging";
        private const int LOGING_PIPE_MAX_COUNT = 6;
        private List<Process> procList = new List<Process>();

        public void Execute()
        {
        }

        public void Init()
        {
            Task.Run(new Action(pipeManageLoop));

            var scr = Screen.PrimaryScreen.Bounds;
            scr.Height = scr.Height - 30;

            var xpos = scr.Width/2;
            var ypos = scr.Height/3;
            var width = scr.Width/2;
            var height = scr.Height/3;

            createChildProcess(LoggingType.DEVICE.ToString(), xpos, ypos * 0 , width, height);
            createChildProcess(LoggingType.TRANS.ToString(), xpos, ypos  * 1, width, height);
            createChildProcess(LoggingType.WEB.ToString(), xpos, ypos * 2, width, height);
        }

        private void createChildProcess(string filter, int x, int y, int width, int height)
        {
            var scr = string.Format("{0}:{1}:{2}:{3}", x, y, width, height);
            var info = new ProcessStartInfo();
            info.FileName = "LogingConsole.exe";
            info.Arguments = string.Format("{0} {1} {2}", LOGING_PIPE_NAME, filter, scr);
            
            var proc = Process.Start(info);
            procList.Add(proc);
        }

        private void pipeManageLoop()
        {
            var servthreads = new Task[LOGING_PIPE_MAX_COUNT];
            while (true)
            {
                for (int i = 0; i < servthreads.Length; ++i)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    var thread = servthreads[i];
                    if (thread == null || thread.IsCompleted || thread.IsFaulted)
                    {
                        thread = new Task(new Action(this.loggingPipeLoop));
                        thread.Start();
                        servthreads[i] = thread;
                        Console.WriteLine("starting new loging pipe thread... ({0})", i);
                    }
                }
            }
        }

        private void loggingPipeLoop()
        {
            var pipe = new NamedPipeServerStream(LOGING_PIPE_NAME, PipeDirection.Out, LOGING_PIPE_MAX_COUNT);
            pipe.WaitForConnection();
            Console.WriteLine("new logging client connected...");
            var listener = new TextWriterTraceListener(pipe);

            Trace.AutoFlush = true;
            Trace.Listeners.Add(listener);

            while (true)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                if (!pipe.IsConnected)
                {                    Trace.Listeners.Remove(listener);
                    pipe.Dispose();
                    Console.WriteLine("logging client disconnected...");
                    return;
                }
            }
        }

        ~LogingFeature()
        {
            foreach (var proc in procList)
            {
                if (!proc.HasExited) proc.Kill();
            }
        }
    }
}
