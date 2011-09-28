using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

using LibUsbDotNet;
using LibUsbDotNet.Main;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorViewLibrary.ViewModels;
using SensorLivetView.Models;

namespace SensorLivetView.ViewModels.Controls
{
    public class UsbDevicesViewModel : ModeledViewModel<UsbDevicesModel>
    {
        public UsbDevicesViewModel()
        {
        }

        #region properties

        public int VenderID
        {
            get
            {
                if (this.Model == null)
                    return 0;
                else
                    return this.Model.VenderId;
            }
            set
            {
                if (this.Model != null)
                    this.Model.VenderId = value;
            }                
        }

        public int ProductID
        {
            get
            {
                if (this.Model == null)
                    return 0;
                else
                    return this.Model.ProductId;
            }
            set
            {
                if (this.Model != null)
                    this.Model.ProductId = value;
            }
        }

        public IList<UsbRegistryViewModel> DeviceCandicates
        {
            get
            {
                if (this.Model == null)
                    return new UsbRegistryViewModel [] { };
                else
                {
                    return this.Model.Candicates.Select(reg => new UsbRegistryViewModel() { Model = reg })
                                                .ToList();
                }

            }
        }

        #region RefreshCommand
        DelegateCommand _RefreshCommand;

        public DelegateCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                    _RefreshCommand = new DelegateCommand(Refresh, CanRefresh);
                return _RefreshCommand;
            }
        }

        private bool CanRefresh()
        {
            return this.Model != null;
        }

        private void Refresh()
        {
            this.Model.Refresh();

            RaisePropertyChanged(()=> this.DeviceCandicates);
        }
        #endregion

        UsbRegistry _SelectedDeviceReg;
        public UsbRegistry SelectedDeviceReg
        {
            get
            { return _SelectedDeviceReg; }
            set
            {
                if (_SelectedDeviceReg == value)
                    return;
                _SelectedDeviceReg = value;
                RaisePropertyChanged("SelectedDeviceReg");
            }
        }

        #endregion
    }

    public class UsbRegistryConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                var res = (value as UsbRegistry).SymbolicName;
                return res;
            }
            catch (NullReferenceException) { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            
            try
            {
                var res = UsbDevice.AllDevices.Where((dev) => dev.SymbolicName == value as string).Single();
                return res;
            }
            catch (NullReferenceException) { }
            catch (InvalidOperationException) { }
            return null;
        }
    }
}
