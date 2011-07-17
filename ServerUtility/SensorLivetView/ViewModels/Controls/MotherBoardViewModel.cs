using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLivetView.ViewModels.Controls
{
    public class MotherBoardViewModel
        : DeviceViewModel<MotherBoard>
    {
        public MotherBoardViewModel()
            : this(null) { }
        public MotherBoardViewModel(MotherBoard device)
            : base(device) { }

        public IEnumerable<object> PortMappingEnumerable
        {
            get
            {
                if (this.Model != null && CurrentState is MotherBoardState)
                {
                    for (int i = 0; i < (CurrentState as MotherBoardState).ModuleTypeLength; i++)
                    {
                        yield return new PortMappingProxy()
                        {
                            ModuleNo = i,
                            ViewModel = this,
                        };
                    }
                }
                else
                    yield return new object[0];
            }
        }

        protected override void ReceivedProcess(IDevice<IDeviceState<IPacketDeviceData>> dev, PacketReceiveEventArgs e)
        {
            var oldstate = e.beforestate as MotherBoardState;
            var curstate = e.state as MotherBoardState;

            if (oldstate == null || curstate == null || this.Model == null)
                return;

            if (!this.Model.IsHold && !oldstate.Data.ModuleType.SequenceEqual(curstate.Data.ModuleType))
                RaisePropertyChanged("");
        }

        private class PortMappingProxy
        {
            public int ModuleNo { get; set; }
            public MotherBoardViewModel ViewModel { get; set; }
            public ModuleTypeEnum ModuleType
            {
                get
                {
                    return this.ViewModel.Model.CurrentState[ModuleNo];
                }
                set
                {
                    this.ViewModel.Model.CurrentState[ModuleNo] = value;
                }
             }
        }
    }
}
