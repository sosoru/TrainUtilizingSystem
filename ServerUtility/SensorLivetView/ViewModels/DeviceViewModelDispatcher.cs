using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

using SensorLibrary;
using SensorViewLibrary;

namespace SensorLivetView.ViewModels
{
    public class DeviceViewModelDispatcher<TDevice, TState, TVM>
        where TDevice : class, IDevice<TState>
        where TState : class, IDeviceState<IPacketDeviceData>
        where TVM : DeviceViewModel<TDevice>
    {
        private PacketDispatcherSingle<TDevice, TState> _dispat;
        public PacketDispatcherSingle<TDevice, TState> dispat
        {
            get
            {
                return this._dispat;
            }
            set
            {
                this._dispat = value;

                if (this.projected != null)
                    this.projected.Context = this._dispat.FoundDeviceList;
            }
        }

        private ObservableWrappingCollection<TDevice, TVM> _projected;
        public ObservableWrappingCollection<TDevice, TVM> projected
        {
            get
            {
                return this._projected;
            }
            set
            {
                this._projected = value;

                if (this.dispat != null)
                    this.projected.Context = this.dispat.FoundDeviceList;

                this.projected.Projection = (src) =>
                    {
                        var vm = typeof(TVM).GetConstructor(new Type [] { }).Invoke(null) as TVM;
                        vm.Model = src;
                        return vm;
                    };
                this.projected.InverseProjection = (proj) =>
                    {
                        return proj.Model;
                    };

            }
        }
    }
    public class ObservableWrappingCollectionOnDispat<TSrc, TProj>
        : ObservableWrappingCollection<TSrc, TProj>
        where TSrc : class
        where TProj : class
    {
        public Dispatcher Dispatcher { get; set; }

        private Func<TSrc, TProj> srcProj;
        public override Func<TSrc, TProj> Projection
        {
            get
            {
                return this.srcProj;
            }
            set
            {
                var wrapped = new Func<TSrc, TProj>((src) =>
                    {
                        if (this.Dispatcher != null)
                            return (TProj)this.Dispatcher.Invoke(srcProj, src);
                        else
                            return srcProj.Invoke(src);
                    });
                this.srcProj = value;
                base.Projection = wrapped;
            }
        }

        private Func<TProj, TSrc> srcInvProj;
        public override Func<TProj, TSrc> InverseProjection
        {
            get
            {
                return this.srcInvProj;
            }
            set
                {
                    var wrapped = new Func<TProj, TSrc>((proj)=>
                        {
                            if(this.Dispatcher != null)
                                return (TSrc)this.Dispatcher.Invoke(srcInvProj, proj);
                            else
                                return srcInvProj.Invoke(proj);
                        });
                    this.srcInvProj = value;
                    base.InverseProjection = wrapped;
                }
        }
    }
}
