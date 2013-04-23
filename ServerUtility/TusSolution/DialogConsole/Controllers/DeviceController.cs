using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tus.Communication;

namespace DialogConsole
{
    public class DeviceController<TDev, TState>
        : IDisposable
        where TDev : IDevice<TState>
        where TState : IDeviceState<IPacketDeviceData>
    {
        public TDev Device { get; private set; }
        protected StreamReader Input
        {
            get
            {
                return new StreamReader(this.instream);
            }
        }
        protected StreamWriter Output
        {
            get
            {
                return new StreamWriter(this.outstream);
            }
        }

        private Stream instream, outstream;

        public DeviceController(Stream input, Stream output)
        {
            this.instream = input;
            this.outstream = output;
        }

        public TState confState(TState state, string msg, Func<string, TState> succeeded)
        {
            Output.WriteLine(msg);
            Output.Flush();

            try
            {
                var dir = Input.ReadLine();
                if (dir != "")
                {
                    return succeeded(dir);
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.Message);
            }

            return state;
        }

        protected TState confEnum<TEnum>(TState state, string msg, IDictionary<string, TEnum> replytable, Func<TEnum, TState> apply)
        {
            return confState(state, msg,
                             reply =>
                                 {
                                     TEnum result;

                                     if (replytable.TryGetValue(reply, out result))
                                         return apply(result);
                                     else
                                         return state;
                                 });
        }

        protected TState confValue(TState state, string msg, Func<float, TState> apply)
        {
            return confState(state, msg,
                             reply =>
                                 {
                                     float val;
                                     if (float.TryParse(reply, out val))
                                         return apply(val);
                                     else
                                         return state;
                                 }
                );
        }

        protected string enumerate_keys(IEnumerable<string> strs)
        {
            return string.Format("{0}", strs.Aggregate("", (ac, str) => ac + str + '/', ac => ac.Remove(ac.Length - 1)));
        }

        public void Dispose()
        {
            if (this.instream != null)
            {
                this.instream.Dispose();
                this.instream = null;
            }


            if (this.outstream != null)
            {
                this.outstream.Dispose();
                this.outstream = null;
            }

            GC.SuppressFinalize(this);
        }

    }
}