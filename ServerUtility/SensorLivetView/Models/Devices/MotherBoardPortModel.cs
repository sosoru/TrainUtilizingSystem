﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using SensorLibrary;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices.PicUsbDevices;

namespace SensorLivetView.Models.Devices
{
    public class MotherBoardPortModel : NotifyObject
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

        public MotherBoardModel Parent { get; private set; }
        public int Address { get; private set; }
        public MotherBoardPortModel(MotherBoardModel parent, int addr)
        {
            this.Parent = parent;
            this.Address = addr;

            this.Parent.TargetDevice.PacketReceived +=
                (sender, e) =>
                {
                    var bef = e.beforestate as MotherBoardState;
                    var cur = e.state as MotherBoardState;

                    if (bef == null && cur == null)
                        return;
                    else if (bef == null || cur == null)
                        RaisePropertyChanged("");

                    if (bef [this.Address] != cur [this.Address])
                        RaisePropertyChanged(() => ModuleType);
                };
        }

        public ModuleTypeEnum ModuleType
        {
            get
            {
                return this.Parent.TargetDevice.CurrentState [this.Address];
            }
            set
            {
                this.Parent.TargetDevice.IsHold = true;
                try
                {
                    var state = this.Parent.TargetDevice.CurrentState;
                    state [this.Address] = value;

                    this.Parent.TargetDevice.SendPacket(state);
                }
                finally
                {
                    this.Parent.TargetDevice.IsHold = false;
                }
            }
        }
    }
}
