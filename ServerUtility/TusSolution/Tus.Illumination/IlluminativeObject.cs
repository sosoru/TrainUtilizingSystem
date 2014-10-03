using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SensorLibrary.Devices.TusAvrDevices;
using Tus.Communication;

namespace Tus.Illumination
{
    [DataContract]
    public class IlluminativeObject
    {
        [DataMember]
        public string Name { get; set; }
        [IgnoreDataMember]
        public Luminary AssociatedLuminary { get; set; }

        [DataMember]
        public bool HalfLighting { get; set; }

        private IDisposable refreshingDisp;

        [IgnoreDataMember]
        private float _luminance;
        [DataMember]
        public float Luminance
        {
            get
            {
                return this._luminance;
            }
            set
            {
                if (0.0f <= value && value <= 1.0f)
                {
                    var associatedLuminary = this.AssociatedLuminary;
                    if (associatedLuminary != null)
                    {
                        var start = (int)(this._luminance * 100.0f);
                        var end = (int)(value * 100.0f);

                        var list = Enumerable.Range((start <= end) ? start : end, Math.Abs(end - start));
                        if (start > end)
                            list = list.Reverse();

                        if (this.refreshingDisp != null)
                            this.refreshingDisp.Dispose();

                        var ob = list.ToObservable()
                            .Delay(TimeSpan.FromMilliseconds(100))
                            .Subscribe(v =>
                                           {
                                               var leds = (IEnumerable<Led>)associatedLuminary.Leds;
                                               foreach (var led in leds)
                                               {
                                                   var duty = v / 100.0f;
                                                   if (this.HalfLighting && led.DeviceID.InternalAddr % 2 == 0)
                                                       duty = 0.01f;

                                                   led.CurrentState.Duty = duty;
                                               }
                                               PacketExtension.CreatePackedPacket(leds).Send(leds.First().ReceivingServer);
                                           });
                        this.refreshingDisp = ob;
                    }

                    this._luminance = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [DataMember]
        //todo: しばらくの間は名前で
        public string SwitchingBlocks { get { return this.Name; } set { this.Name = value; } }
    }
}
