using System.Collections.Generic;
using SensorLibrary.Devices.TusAvrDevices;
using Tus.Factory;
using Tus.Illumination;
using Tus.TransControl.Base;
using System.Linq;

namespace DialogConsole
{
    public class Layout
    {
        public Layout(RouteOrderListFactory availableRoutesOrderFactory, BlockSheet sheet, IlluminativeSheet illumi)
        {
            AvailableRoutesOrderFactory = availableRoutesOrderFactory;
            Sheet = sheet;
            this.Illumination = illumi;
        }

        public Layout()
        {
        }

        public virtual RouteOrderListFactory AvailableRoutesOrderFactory { get; set; }
        public virtual BlockSheet Sheet { get; set; }
        public virtual IlluminativeSheet Illumination { get; set; }
        public virtual IList<Vehicle> Vehicles { get; set; }

        public void SendInquiryState()
        {
            SendInquiryState(Vehicles);
        }

        public void SendInquiryState(IList<Vehicle> vehicles)
        {
            var blocks = vehicles.SelectMany(v => v.AssociatedRoute.LockedBlocks).Distinct();
            this.Sheet.InquiryDevices(blocks);
        }

    }
}