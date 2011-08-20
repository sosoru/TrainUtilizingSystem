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
                ob.OnNext(state);
            }
        }


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
