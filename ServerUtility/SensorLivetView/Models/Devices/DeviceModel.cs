using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reactive.Linq;

using Livet;
using SensorLibrary;
using SensorLibrary.Devices;

namespace SensorLivetView.Models.Devices
{
    public class DeviceModel<TDev>
        : NotifyObject, IDeviceModel<TDev>
        where TDev : class,  IDevice<IDeviceState<IPacketDeviceData>>
    {
        /*
         * NotifyObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        /*
         * リッチクライアントはステートフルであるため、通常のイベントの使用はメモリリークの原因になりやすくなっています。
         * Modelからイベントを発行する場合はNotificatorを使用してください。
         *
         * Notificatorはイベント代替手段です。コードスニペット lnev でCLRイベントと同時に定義できます。
         *
         * Model同士でNotificatorを使用した通知を行う場合はNotificatorHelper、
         * ViewModelへNotificatorを使用した通知を行う場合はViewModelHelperを使用して受信側の登録をしてください。
         */

        public DeviceModel()
        {
            this.WaitingList = new List<Func<IDeviceState<IPacketDeviceData>, bool>>();
        }

        private IDisposable sunbscribing;

        private TDev targetDevice;
        public TDev TargetDevice
        {
            get
            {
                return this.targetDevice;
            }
            set
            {
                if (this.targetDevice == null)
                    if (sunbscribing != null)
                        this.sunbscribing.Dispose();

                this.targetDevice = value;

                if (targetDevice != null)
                    this.sunbscribing = this.targetDevice.GetNextObservable
                            .Subscribe((e) =>
                            {
                                if (this.TargetDevice == null || this.PacketReceivedProcess == null)
                                    return;

                                bool breaker = false;
                                foreach (var q in this.WaitingList.ToArray())
                                {
                                    var res = q(e.EventArgs.state);

                                    if (!res) // waiting complete
                                    {
                                        this.WaitingList.Remove(q);
                                    }
                                    else
                                        breaker = true;

                                }

                                if (!breaker)
                                    this.PacketReceivedProcess(this.TargetDevice, e.EventArgs);
                            });

            }
        }

        internal List<Func<IDeviceState<IPacketDeviceData>, bool>> WaitingList { get; set; }

        public DeviceID DevID
        {
            get
            {
                return this.TargetDevice.DeviceID;
            }
        }

        internal void ModifyState(Action act)
        {
            this.ModifyState(act, d => false);
        }

        internal void ModifyState(Action act, Func<IDeviceState<IPacketDeviceData>, bool> waitingPredicate)
        {
            if (this.TargetDevice == null || this.TargetDevice.CurrentState == null)
                return;

            if (this.TargetDevice.IsHold)
                return;

            this.TargetDevice.IsHold = true;
            try
            {
                act();

                RegisterWaiting(waitingPredicate);
                this.TargetDevice.SendPacket(this.TargetDevice.CurrentState);
            }
            finally
            {
                this.TargetDevice.IsHold = false;
            }
        }

        internal void RegisterWaiting(Func<IDeviceState<IPacketDeviceData>, bool > predicate)
        {
            var d = DateTime.Now.AddSeconds(1);
            this.WaitingList.Add( s=> d <= DateTime.Now && predicate(s));
        }

        private PacketReceivedDelegate<IDeviceState<IPacketDeviceData>> packetReceivedProcess;
        protected PacketReceivedDelegate<IDeviceState<IPacketDeviceData>> PacketReceivedProcess
        {
            get
            {
                return this.packetReceivedProcess;
            }
            set
            {
                this.packetReceivedProcess = value;
            }

        }

        public bool IsStateReady
        {
            get { return this.TargetDevice.CurrentState != null; }
        }

        ~DeviceModel()
        {
            this.TargetDevice = null;
        }
    }
}
