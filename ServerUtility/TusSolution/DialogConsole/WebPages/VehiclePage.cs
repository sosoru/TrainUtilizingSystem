using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DialogConsole.Features;
using Tus.Communication.Device.AvrComposed;
using Tus.Diagnostics;
using Tus.TransControl.Base;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("vehicle control", "vehicles")]
    public class VehiclePage : ConsolePageBase<IEnumerable<Vehicle>, IEnumerable<VehicleInfoReceived>>
    {
        protected override IEnumerable<Type> KnownTypesWhenSerialization
        {
            get
            {
                return new[]
                           {
                               typeof (ControlUnit), typeof (Switch), typeof (Motor), typeof (UsartSensor),
                               typeof (MemoryState)
                           };
            }
        }

        public override IEnumerable<Vehicle> CreateSendingContent()
        {
            IEnumerable<Vehicle> vehis = this.Param.UsingLayout.Vehicles.ToArray();
            int id;

            //if (int.TryParse(this.Query["id"], out id))
            //    vehis = vehis.Where(v => v.VehicleID == id);

            return vehis;
        }

        public IEnumerable<VehicleInfoReceived> CreateShortInfo()
        {
            return this.Param.UsingLayout.Vehicles.Select(v => new VehicleInfoReceived()
                                                                   {
                                                                       Accelation = v.Accelation.ToString(),
                                                                       CurrentBlockName = v.CurrentBlock.Name,
                                                                       Halts = v.Halt.Select(h => h.HaltBlock.Name).ToArray(),
                                                                       Name = v.Name,
                                                                       RouteName = v.AssociatedRoute.RouteOrder.Name,
                                                                       Speed = v.Speed.ToString(),
                                                                       Timeout = ""
                                                                   });
        }

        public override void ApplyReceivedJsonRequest()
        {
            IEnumerable<VehicleInfoReceived> objs;
            if (!this.ReceivedContents.TryDequeue(out objs)) return;

            foreach (var obj in objs)
            {
                var vh = this.Param.UsingLayout.Vehicles.First(v => v.Name == obj.Name);

                if (obj.Speed != null && float.Parse(obj.Speed) != vh.Speed)
                {
                    var changeto = float.Parse(obj.Speed);
                    Logger.WriteLineAsWebInfo("{0} is changing speed from {1} to {2}", vh.Name, vh.Speed, changeto);

                    vh.Speed = changeto;
                }
                if (obj.Accelation != null && float.Parse(obj.Accelation) != vh.Accelation)
                {
                    var changeto = float.Parse(obj.Accelation);
                    Logger.WriteLineAsWebInfo("{0} is changing accelation from {1} to {2}", vh.Name, vh.Accelation, changeto);

                    vh.Accelation = changeto;
                }

                if (obj.RouteName != null && obj.RouteName != vh.AssociatedRoute.RouteOrder.Name)
                {
                    Logger.WriteLineAsWebInfo("{0} is changing route from {1} to {2}", vh.Name, vh.AssociatedRoute.RouteOrder.Name,
                                      obj.RouteName);
                    vh.ChangeRoute(obj.RouteName, vh.CurrentBlock.Name);

                }
                if (obj.CurrentBlockName != null && obj.CurrentBlockName != vh.CurrentBlock.Name && vh.AssociatedRoute.RouteOrder.Units.Any(u => u.ControlBlock.Name == obj.CurrentBlockName))
                {
                    Logger.WriteLineAsWebInfo("{0} is changing position from {1} to {2}", vh.Name, vh.CurrentBlock.Name, obj.CurrentBlockName);
                    vh.ChangeRoute(vh.AssociatedRoute.RouteOrder.Name, obj.CurrentBlockName);
                }
                if (obj.Timeout != null)
                {

                }
                //if (obj.Halts != null)
                //{
                //    Logger.WriteLineAsWebInfo("{0} is changing halts set to {1}", vh.Name,
                //                      obj.Halts.Aggregate("", (ag, s) => ag + (s + ", ")));
                //    var halts = obj.Halts.Select(h => new Halt(vh.Sheet.GetBlock(h)));
                //    vh.Halt.Clear();
                //    foreach (var h in halts)
                //        vh.Halt.Add(h);
                //}


            }
        }
    }
}
