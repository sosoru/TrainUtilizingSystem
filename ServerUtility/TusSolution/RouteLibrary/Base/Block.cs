using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary;
namespace RouteLibrary.Base
{
    public class CommandInfo
    {
        public RouteSegmentInfo Route { get; set; }
        public float Speed { get; set; }

    }

    public class DetectorState
    {
        public bool IsDetected { get; set; }
    }

    public class SensorDetector
        : IObservable<DetectorState>
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

        protected virtual void Notify()
        {
            var state = new DetectorState() { IsDetected = this.devices.Any(a => a.IsDetected) };

            this.observerList.ForEach(d => d.OnNext(state));
        }

        #region Implemention of IObservable

        protected List<IObserver<DetectorState>> observerList = new List<IObserver<DetectorState>>();
        public IDisposable Subscribe(IObserver<DetectorState> observer)
        {
            observerList.Add(observer);

            return new UnSubscriber(() => this.observerList.Remove(observer));
        }

        #region UnSubscriber class
        private class UnSubscriber
            : IDisposable
        {
            private Action disposingFunc;
            private bool disposed = false;
            public UnSubscriber(Action disposingFunc)
            {
                this.disposingFunc = disposingFunc;
            }

            public void Dispose()
            {
                if (!disposed)
                {
                    disposingFunc();
                    disposed = true;
                }
            }

            ~UnSubscriber()
            {
                this.Dispose();
            }
        }
        #endregion
        #endregion
    
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

            var effs = new List<IDeviceEffector>();
            if(info.Motor != null)
                effs.Add(new MotorEffector(info.Motor, sheet));

            if(info.Switch !=null)
                effs.Add(new SwitchEffector(info.Switch, sheet));

            this.Effectors = new ReadOnlyCollection<IDeviceEffector>(effs);
        }

        public void Effect(IEnumerable<CommandInfo> infos)
        {
            var list_infos = infos.ToList();

            this.Effectors.ToList().ForEach(e =>
            {
                list_infos.ForEach(i => e.ApplyCommand(i));
                e.ExecuteCommand();
            });

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


        public bool HasMotor
        {
            get
            {
                return this.info.Motor != null;
            }
        }
    }
}
