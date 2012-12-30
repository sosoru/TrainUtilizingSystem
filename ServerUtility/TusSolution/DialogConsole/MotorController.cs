using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

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

    public class MotorController
        : DeviceController<Motor, MotorState>
    {
        public MotorController(Stream input, Stream output)
            : base(input, output) { }

        public MotorState confCurrent(MotorState state)
        {
            var msg = string.Format("Current : {0} (0.0-5.0)", state.Current);

            return confValue(state, msg,
                replied =>
                {
                    state.Current = replied;
                    return state;
                });
        }

        public MotorState confDuty(MotorState state)
        {
            var msg = string.Format("Duty : {0} (0.0-1.0)", state.Duty);

            return confValue(state, msg,
                replied =>
                {
                    state.Duty = replied;
                    return state;
                });
        }

        public MotorState confDirection(MotorState state)
        {
            var table = new Dictionary<string, MotorDirection> { 
                {"stb", MotorDirection.Standby},
                { "pos", MotorDirection.Positive },
                { "neg", MotorDirection.Negative }
            };
            return confEnum<MotorDirection>(state,
                string.Format("Direction : {0} ({1})", Enum.GetName(typeof(MotorDirection), state.Direction), enumerate_keys(table.Keys)),
                 table,
                 replied =>
                 {
                     state.Direction = replied;
                     return state;
                 }
                 );

        }

        public MotorState confMode(MotorState state)
        {
            var table = new Dictionary<string, MotorControlMode> {
            { "duty", MotorControlMode.DutySpecifiedMode},
            {"curr", MotorControlMode.CurrentFeedBackMode},
        };
            return confEnum<MotorControlMode>(
                state,
                string.Format("ControllMode : {0}. ({1})",
                    Enum.GetName(typeof(MotorControlMode), state.ControlMode),
                    enumerate_keys(table.Keys)
                    ),
                table,
                replied =>
                {
                    state.ControlMode = replied;
                    return state;
                }
            );

        }

    }
}