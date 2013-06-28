using System;
using System.IO;

namespace TestProject.DialogController
{
    public class DialogControllerTestHelper
    {
        public void PrepareStateTest(string cmd, Action<Stream, Stream> callTest)
        {
            using (var output = new MemoryStream())
            using (var input = new MemoryStream())
            using (var sr_input = new StreamReader(input))
            using (var sw_output = new StreamWriter(output))
            {
                sw_output.AutoFlush = true;
                sw_output.WriteLine(cmd);
                output.Seek(0, SeekOrigin.Begin);

                callTest(output, input);

                input.Seek(0, SeekOrigin.Begin);
                Console.WriteLine(sr_input.ReadToEnd());
            }

        }
    }
}