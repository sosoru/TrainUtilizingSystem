using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogingConsole
{
    class Program
    {
        //        BOOL MoveWindow(
        //            HWND hWnd,      // ウィンドウのハンドル
        //            int X,          // 横方向の位置
        //            int Y,          // 縦方向の位置
        //            int nWidth,     // 幅
        //            int nHeight,    // 高さ
        //            BOOL bRepaint   // 再描画オプション
        //            );
        [DllImport("User32.dll")]
        static extern int MoveWindow(
            IntPtr hWnd,
            int x,
            int y,
            int nWidth,
            int nHeight,
            int bRepaint
            );

        static void Main(string[] args)
        {
            var pipename = args[0];
            var filter = args[1];
            var scr = args[2].Split(':').Select(int.Parse).ToArray();

            Console.Title = string.Format("{0} - {1}", "Logger", filter == "" ? "all" : filter);
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;
            try
            {
                using (var st = new NamedPipeClientStream(".", pipename, PipeDirection.In))
                using (var sr = new StreamReader(st))
                {
                    st.Connect();
                    Console.WriteLine("pipe connected");
                    MoveWindow(Process.GetCurrentProcess().MainWindowHandle, scr[0], scr[1], scr[2], scr[3], 1);
                    while (true)
                    {
                        string temp;
                        while ((temp = sr.ReadLine()) != null)
                        {
                            if (filter != "" && temp.Contains(filter))
                                Console.WriteLine(temp);
                        }
                        if (!st.IsConnected)
                        {
                            Console.WriteLine("pipe disconnected");
                            return;
                        }
                    }
                }
            }
            catch (IOException)
            {
            }

        }
    }
}
