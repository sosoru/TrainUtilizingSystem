using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using SensorLibrary;
using System.Reactive.Linq;

namespace SensorLivetView.Models
{
    public abstract class TestTaskModel 
        : Model
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

        public IObservable<System.Reactive.Unit> Start()
        {
            var mt = new Action(internalStart);
            return Observable.FromAsyncPattern(mt.BeginInvoke, mt.EndInvoke)();
        }

        public bool IsTesting { get; protected set; }

        protected abstract void internalStart();
        
    }

    public class RangedValue
         : Model
    {

        public RangedValue()
            : base()
        { }

        public RangedValue(double min, double max, double interval)
            : this()
        {
            this.Min = min;
            this.Max = max;
            this.Interval = interval;
        }

        double _Min;

        public double Min
        {
            get
            { return _Min; }
            set
            {
                if (_Min == value)
                    return;
                _Min = value;
                RaisePropertyChanged("Min");
            }
        }


        double _Max;

        public double Max
        {
            get
            { return _Max; }
            set
            {
                if (_Max == value)
                    return;
                _Max = value;
                RaisePropertyChanged("Max");
            }
        }


        double _Interval;

        public double Interval
        {
            get
            { return _Interval; }
            set
            {
                if (_Interval == value)
                    return;
                _Interval = value;
                RaisePropertyChanged("Interval");
            }
        }

        public IEnumerable<double> ToEnumerable()
        {
            for (double i = this.Min; i < this.Max; i += this.Interval)
                yield return i;
        }
    }
}
