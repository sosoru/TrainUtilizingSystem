using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.Data;

namespace RouteLibrary.Base
{
    public class CommandInfo
    {
        public Route Route { get; set; }
        public float Speed { get; set; }

    }

    public class MotorEffector
        : IDeviceEffector
    {
        public BlockSheet Sheet { get; private set; }
        public MotorInfo Info { get; private set; }
        public Motor Device { get; private set; }

        public MotorEffector(MotorInfo info, BlockSheet sheet)
        {
            this.Info = info;

            this.Device = new Motor()
                {
                    DeviceID = info.Address,
                };

            this.Device.Observe(sheet.Dispatcher);
        }

        public void EffectByRoute(CommandInfo cmd)
        {
            var state = new MotorState();

            if (cmd.Route.Current == this.Info.RoutePositive)
            {
                state.Direction = MotorDirection.Positive;
            }
            else if (cmd.Route.Current == this.Info.RouteNegative)
            {
                state.Direction = MotorDirection.Negative;
            }

            state.Duty = cmd.Speed;
            this.Device.SendPacket(state);
        }
    }

    public class SwitchEffector
        : IDeviceEffector
    {
        public BlockSheet Sheet { get; private set; }
        public SwitchInfo Info { get; private set; }
        public Switch Device { get; private set; }

        public SwitchEffector(SwitchInfo info, BlockSheet sheet)
        {
            this.Sheet = sheet;
            this.Info = info;

            this.Device = new Switch() { DeviceID = info.Address };
            this.Device.Observe(sheet.Dispatcher);
        }

        public void EffectByRoute(CommandInfo cmd)
        {
            var state = new SwitchState();

            if (cmd.Route.Current == this.Info.DirStraight)
            {
                state.Position = PointStateEnum.Straight;
            }
            else if (cmd.Route.Current == this.Info.DirCurved)
            {
                state.Position = PointStateEnum.Curve;
            }
            else
            {
                state.Position = PointStateEnum.Any;
            }

            state.ChangingTime = 250;
            state.DeadTime = 50;

            this.Device.SendPacket(state);
        }
    }

    public class SensorDetector
    {
        public BlockSheet Sheet { get; private set; }
        public SensorInfo Info { get; private set; }
        private List<Sensor> devices { get; set; }
        public ReadOnlyCollection<Sensor> Devices { get { return new ReadOnlyCollection<Sensor>(this.devices); } }

        public SensorDetector(SensorInfo info, BlockSheet sheet)
        {
            this.Sheet = sheet;
            this.Info = info;

            var devs = info.Addresses.Select(id => new Sensor() { DeviceID = id }).ToList();
            devs.ForEach(d => d.Observe(sheet.Dispatcher));
            this.devices = devs;
        }


        public bool IsDetected
        {
            get
            {
                return this.devices.Any(s => s.IsDetected);
            }
        }
    }

    public class Block
    {
        private BlockInfo info;

        public string Name { get; private set; }
        public BlockSheet Sheet { get; private set; }

        public IList<IDeviceEffector> Effectors { get; private set; }

        public Block(BlockInfo info, BlockSheet sheet)
        {
            this.info = info;
            this.Name = info.Name;
            this.Sheet = sheet;

            var effs = new IDeviceEffector[] { new MotorEffector(info.Motor, sheet), new SwitchEffector(info.Switch, sheet) };
            this.Effectors = new ReadOnlyCollection<IDeviceEffector>(effs);
        }



        #region implementation of IEqualable
        public static bool operator ==(Block A, Block B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.Name == B.Name) && (A.Sheet.Name == B.Sheet.Name);
        }
        public static bool operator !=(Block A, Block B) { return !(A == B); }

        public bool Equals(Block other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Sheet.Name.GetHashCode();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            return this.Equals((Block)obj);
        }

        #endregion

    }
}
