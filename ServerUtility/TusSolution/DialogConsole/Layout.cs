using System.Collections.Generic;
using Tus.Factory;
using Tus.TransControl.Base;
using System.Linq;

namespace DialogConsole
{
    public class Layout
    {
        public Layout(RouteOrderListFactory availableRoutesOrderFactory, BlockSheet sheet)
        {
            AvailableRoutesOrderFactory = availableRoutesOrderFactory;
            Sheet = sheet;
        }

        public Layout()
        {
        }

        public virtual RouteOrderListFactory AvailableRoutesOrderFactory { get; set; }
        public virtual BlockSheet Sheet { get; set; }
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