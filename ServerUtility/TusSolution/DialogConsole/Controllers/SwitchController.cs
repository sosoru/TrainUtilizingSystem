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
    public class SwitchController
        : DeviceController<Switch, SwitchState>
    {
        public SwitchController(Stream input, Stream output)
            : base(input, output) { }

        public SwitchState ConfPosition(SwitchState state)
        {
            var table = new Dictionary<string, PointStateEnum>{
                {"a", PointStateEnum.Any},
                {"s", PointStateEnum.Straight},
                {"c", PointStateEnum.Curve},
            };
            var msg = string.Format("Position : {0} {1}",
                Enum.GetName(typeof(PointStateEnum), state.Position),
                enumerate_keys(table.Keys)
                );

            return this.confEnum<PointStateEnum>(state,
                msg,
                table,
                replied =>
                {
                    state.Position = replied;
                    return state;
                }
            );
        }

        public SwitchState ConfDeadTime(SwitchState state)
        {
            var msg = string.Format("DeadTime : {0} (100-355)", state.DeadTime);

            return this.confValue(state, msg,
                replied =>
                {
                    state.DeadTime = (int)replied;
                    return state;
                });
        }

        public SwitchState ConfChangingTime(SwitchState state)
        {
            var msg = string.Format("ChangingTime : {0} (0-1005)", state.ChangingTime);

            return this.confValue(state, msg,
                replied =>
                {
                    state.ChangingTime = (int)replied;
                    return state;
                });
        }
    }
}
