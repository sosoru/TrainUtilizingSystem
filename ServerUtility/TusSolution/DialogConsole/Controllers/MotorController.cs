using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole
{
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