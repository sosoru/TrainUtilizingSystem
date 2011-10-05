using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using SensorLivetView.Models;
using SensorLivetView.Models.Devices;
using SensorLibrary;
using System.Collections.ObjectModel;

namespace SensorLivetView.ViewModels.Controls
{
    public class MotherBoardViewModel
        : DeviceViewModel<MotherBoardModel>
    {
        public MotherBoardViewModel(MotherBoardModel model)
            : base(model)
        {
            ViewModelHelper.BindNotifyChanged(this.Model, this,
                (sender, e) =>
                {
                    RaisePropertyChanged(e.PropertyName);
                    if (e.PropertyName == "Ports")
                    {
                        RaisePropertyChanged("PortViewModels");
                    }
                });
        }

        public int BaseParentID
        {
            get
            {
                return (int)this.Model.BaseParentID;
            }
            set
            {
                //TODO : error handle
                if (this.Model.BaseParentID > byte.MaxValue)
                    throw new NotImplementedException();
                else
                    this.Model.BaseParentID = (byte)value;
            }
        }

        public IEnumerable<MotherBoardPortViewModel> PortViewModels
        {
            get
            {
                return this.Model.Ports.Select((m) => new MotherBoardPortViewModel(m));
            }
        }

    }
}
