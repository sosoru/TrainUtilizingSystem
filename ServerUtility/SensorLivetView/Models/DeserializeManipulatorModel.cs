﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using SensorLibrary;
using SensorLibrary.Devices;
using SensorLibrary.Manipulators;

namespace SensorLivetView.Models
{
    public class DeserializeManipulatorModel<TDevice, TState> : NotifyObject
        where TDevice : class, IDevice<TState>
        where TState : class, IDeviceState<IPacketDeviceData>
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

        public DeserializeManipulatorModel()
        {
            this.Deserializer = new DeserializeManipulatorModel<TDevice, TState>();
        }

        public DeserializeManipulatorModel<TDevice, TState> Deserializer { get; private set; }

    }
}
