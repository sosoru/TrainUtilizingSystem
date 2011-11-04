using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reactive.Linq;

using Livet;
using SensorLibrary;

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
                                if (this.TargetDevice != null && this.PacketReceivedProcess != null)
                                    this.PacketReceivedProcess(this.TargetDevice, e.EventArgs);
                            });

            }
        }

        public DeviceID DevID
        {
            get
            {
                return this.TargetDevice.DeviceID;
            }
        }
       

        internal void ModifyState(Action act)
        {
            if (this.TargetDevice == null || this.TargetDevice.CurrentState == null)
                return;

            this.TargetDevice.IsHold = true;
            try
            {
                act();

                this.TargetDevice.SendPacket(this.TargetDevice.CurrentState);
            }
            finally
            {
                this.TargetDevice.IsHold = false;
            }
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
