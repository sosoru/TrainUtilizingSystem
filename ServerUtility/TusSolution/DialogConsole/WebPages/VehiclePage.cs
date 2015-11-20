using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
                                                                       IsHalt = v.IsHalted.ToString(),
                                                                       Halts = v.Halt.Select(h => h.HaltBlock.Name).ToArray(),
                                                                       Name = v.Name,
                                                                       ShownName = v.ShownName,
                                                                       RouteName = v.AssociatedRoute.RouteOrder.Name,
                                                                       Speed = v.Speed.ToString(),
                                                                       StopThreshold = v.StopThreshold.ToString(),
                                                                       Timeout = ""
                                                                   });
        }

        public override void ApplyReceivedJsonRequest()
        {
            IEnumerable<VehicleInfoReceived> objs;
            if (!this.ReceivedContents.TryDequeue(out objs)) return;

            foreach (var obj in objs)
            {
                var vh = this.Param.UsingLayout.Vehicles.FirstOrDefault(v => v.Name == obj.Name);
                if (vh == null) continue;

                if (obj.IsHalt != vh.IsHalted.ToString())
                {
                    bool res = false;
                    var vhhalt = vh.IsHalted;
                    bool objhalt;
                    if (bool.TryParse(obj.IsHalt, out objhalt))
                    {
                        if (objhalt && vh.CanHaltHere)
                        {
                            vh.HaltHere();
                            res = true;
                        }
                        else if (!objhalt && vh.CanLeaveHere)
                        {
                            vh.LeaveHere();
                            res = true;
                        }

                        if (res) Logger.WriteLineAsWebInfo("{0}({1}) is changed its halt state from {2} to {3}", vh.Name, vh.ShownName, vhhalt, objhalt);

                    }

                }
                if (obj.ShownName != vh.ShownName)
                {
                    Logger.WriteLineAsWebInfo("{0} is changed its shown name from {1} to {2}", vh.Name, vh.ShownName, obj.ShownName);
                    vh.ShownName = obj.ShownName;
                }
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
                if (obj.StopThreshold != null && float.Parse(obj.StopThreshold) != vh.StopThreshold)
                {
                    var changeto = float.Parse(obj.StopThreshold);
                    Logger.WriteLineAsWebInfo("{0} is changing stop threshold from {1} to {2}", vh.Name,
                                              vh.StopThreshold, changeto);

                    vh.StopThreshold = changeto;
                }
                if (obj.RouteName != null && obj.RouteName != vh.AssociatedRoute.RouteOrder.Name)
                {
                    Logger.WriteLineAsWebInfo("{0} is changing route from {1} to {2}", vh.Name, vh.AssociatedRoute.RouteOrder.Name,
                                      obj.RouteName);
                    try
                    {
                        vh.ChangeRoute(obj.RouteName, vh.CurrentBlock.Name);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Logger.WriteLineAsWebInfo("{0} is failed changing route from {1} to {2} : {3}", vh.Name,
                                                  vh.AssociatedRoute.RouteOrder.Name, obj.RouteName, ex.Message);
                    }

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
