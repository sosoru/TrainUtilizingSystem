using System.Collections.Generic;
using Tus.Factory;
using Tus.TransControl.Base;
using System.Linq;

namespace DialogConsole
{
    public class Layout
    {
        public Layout(RouteListFactory availableRoutesFactory, BlockSheet sheet)
        {
            AvailableRoutesFactory = availableRoutesFactory;
            Sheet = sheet;
        }

        public RouteListFactory AvailableRoutesFactory { get; set; }
        public BlockSheet Sheet { get; set; }
        public IList<Vehicle> Vehicles { get; set; }

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