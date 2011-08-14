using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    using obsvType = IObserver<IDeviceState<IPacketDeviceData>>;

    public class PacketDispatcher
        : IObservable<IDeviceState<IPacketDeviceData>>
    {

        public virtual void Notify(IDeviceState<IPacketDeviceData> state)
        {
            foreach (var ob in this.observerList.ToList())
            {
                //if (!ob.IsAlive)
                //    this.observerList.Remove(ob);
                //else
                //    ob.Target.OnNext(state);
                //if (state.BasePacket.ModuleType == ModuleTypeEnum.MotherBoard)
                //{
                //    var mb = new MotherBoard(state.BasePacket.ID);
                //    if (!_mboards.Contains(mb))
                //    {
                //        mb.Observe(this);
                //        _mboards.Add(mb);
                //    }
                    
                //}
                //else
                //{
                    ob.OnNext(state);
                //}
            }
        }

        //private IList<WeakReference<MotherBoard>> _mboards = new List<WeakReference<MotherBoard>>();
        //public IList<WeakReference<MotherBoard>> MotherBoards
        //{
        //    get
        //    {
        //        foreach (var mb in _mboards.ToList())
        //        {
        //            if (!mb.IsAlive)
        //                _mboards.Remove(mb);
        //        }
        //        return new ReadOnlyCollection<WeakReference<MotherBoard>>(this._mboards);
        //    }
        //}

        //public virtual TDevice GetDevice<TDevice>(DeviceID id)
        //    where TDevice : class, IDevice<IDeviceState<IPacketDeviceData>>
        //{
        //    var ctor = typeof(TDevice).GetConstructor(new[] { typeof(DeviceID) });
        //    var inst = ctor.Invoke(new object[] { id });
        //    var dev = inst as IDevice<IDeviceState<IPacketDeviceData>>;

        //    dev.Observe(this);
        //    return dev as TDevice;
        //}


        #region Implemention of IObservable

        protected List<obsvType> observerList = new List<obsvType>();
        public IDisposable Subscribe(obsvType observer)
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
}
