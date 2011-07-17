using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorViewModels
{
    public class MotherBoardViewModel
        : DeviceViewModel<MotherBoard>
    {
        public MotherBoardViewModel(MotherBoard device)
            : base(device) { }

        public IEnumerable<object> PortMappingEnumerable
        {
            get
            {
                if (CurrentState is MotherBoardData)
                {
                    var list = (this.CurrentState as MotherBoardData).ModuleType.ToList();
                    return list.Select((item) => new { Port = list.IndexOf(item), ModuleType = (ModuleTypeEnum)item });
                }
                else
                    return new object[0];
            }
        }
    }
}
